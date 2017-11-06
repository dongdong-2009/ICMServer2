using GalaSoft.MvvmLight.Threading;
using ICMServer;
using ICMServer.Models;
using ICMServer.Net;
using ICMServer.Services.Data;
using ICMServer.WPF.Models;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ICMServer.Services.Net
{
    public class NotificationService
    {
        enum STATE
        {
            STOPPED,
            STARTED
        }

        private static volatile NotificationService _instance;
        private static object _syncRoot = new object();
        private STATE _state = STATE.STOPPED;
        private object _lock = new object();
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private CancellationToken _token;
        private ConcurrentBag<Task> _tasks = new ConcurrentBag<Task>();
        private ConcurrentQueue<NetworkTask> _networkTasks = new ConcurrentQueue<NetworkTask>();
        private Dictionary<string, TaskQueue> _tasksPerIP = new Dictionary<string, TaskQueue>();
        private DateTime _serviceStartTime;

        private NotificationService()
        {
            _token = _tokenSource.Token;
        }

        public static NotificationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new NotificationService();
                    }
                }

                return _instance;
            }
        }

        public bool Start()
        {
            lock (_lock)
            {
                DebugLog.TraceMessage("");
                if (_state == STATE.STOPPED)
                {
                    _state = STATE.STARTED;
                    _serviceStartTime = DateTime.Now;

                    _tasks.Add(Task.Run(() => { ConsumerMainTask(_token); }));
                    _tasks.Add(Task.Run(() => { PublishAnnouncementsTask(_token); }));
                    _tasks.Add(Task.Run(() => { UpgradeTask(_token); }));
                }
                return _state == STATE.STARTED;
            }
        }

        public void Reset()
        {
            NetworkTask task;
            while (_networkTasks.TryDequeue(out task)) { }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (_state == STATE.STARTED)
                {
                    try
                    {
                        Reset();
                        _tokenSource.Cancel();
                        Task.WaitAll(_tasks.ToArray());
                    }
                    catch (Exception) { }

                    _state = STATE.STOPPED;
                }
            }
        }

        private void Add(NetworkTask task)
        {
            lock (_lock)
            {
                if (_state == STATE.STARTED)
                {
                    _networkTasks.Enqueue(task);
                }
            }
        }

        // Producer Task
        // 主要任務是到資料庫查看有哪些公告訊息未發佈的，如果有未發佈的公告訊息，
        // 從資料庫找出那些公告訊息應發佈到哪些裝置，並排程網路任務 (NetworkTask)。
        private void PublishAnnouncementsTask(CancellationToken cancelToken)
        {
            try
            {
                int lastestPublishedAnnouncementID = 0;

                while (true)
                {
                    _token.ThrowIfCancellationRequested();

                    using (var db = new ICMDBContext())
                    {
                        try
                        {
                            var latestGeneratedAnnouncementID = db.Announcements.Select(a => a.id).DefaultIfEmpty(0).Max();
                            if (latestGeneratedAnnouncementID != lastestPublishedAnnouncementID)
                            {
                                var devices = (from device in db.Devices
                                               from ar in db.AnnouncementRooms
                                               where device.roomid.StartsWith(ar.RoomID)
                                                  && ar.AnnouncementID > lastestPublishedAnnouncementID
                                               select device).Distinct();
                                foreach (var device in devices)
                                {
                                    if (device.type == (int)DeviceType.Indoor_Phone
                                     || device.type == (int)DeviceType.Indoor_Phone_SD)
                                    {
                                        NetworkTask task = new NetworkTask
                                        {
                                            IP = device.ip,
                                            TaskFunc = async () =>
                                            {
                                                int newAnnouncementCount = await Task.Run(() =>
                                                {
                                                    int count = 0;
                                                    try
                                                    {
                                                        using (var _db = new ICMDBContext())
                                                        {
                                                            count = (from ar in _db.AnnouncementRooms
                                                                     where ar.RoomID == device.roomid.Substring(0, 14)
                                                                        && ar.HasRead == false
                                                                     select ar).Count();
                                                        }
                                                    }
                                                    catch (Exception) { }
                                                    return count;
                                                }).ConfigureAwait(false);
                                                await HttpClient.SendNewTextMsgCountAsync(device.ip, newAnnouncementCount).ConfigureAwait(false);
                                            }
                                        };
                                        this.Add(task);
                                        //DebugLog.TraceMessage(string.Format("IP: {0}", device.ip));
                                    }
                                }
                                lastestPublishedAnnouncementID = latestGeneratedAnnouncementID;
                            }
                        }
                        catch (Exception) { }
                    }
                    Thread.Sleep(100);
                }
            }
            catch (OperationCanceledException) { }
        }

        class DeviceEqualityComparer : IEqualityComparer<Device>
        {
            public bool Equals(Device x, Device y)
            {
                return (x.id == y.id);
            }

            public int GetHashCode(Device obj)
            {
                return obj.id;
            }
        }

        // ConcurrentQueue<Task> updateTaskQueueForUpgradeTask = new ConcurrentQueue<Task>();
        private void UpgradeTaskForEachDevice(IUpgradeTasksModel upgradeTaskDataModel, Device device, CancellationToken cancelToken)
        {
            // 刪除被使用者標記要刪除且非運行中的 Task
            //var tasksToBeRemoved = upgradeTaskDataModel.Data.Where(
            var tasksToBeRemoved = upgradeTaskDataModel.Select(
                t => t.DeletedByUser
                  && t.Status != UpgradeStatus.InProgress).ToList();
            if (tasksToBeRemoved != null)
                upgradeTaskDataModel.Delete(tasksToBeRemoved);

            //var updateTasksPerDevice = upgradeTaskDataModel.Data.Where(
            //    t => t.DeviceID == device.id && t.Status != UpgradeStatus.Removed
            var updateTasksPerDevice = upgradeTaskDataModel.Select(
                t => t.DeviceID == device.id && t.Status != UpgradeStatus.Removed,
                t => t.Device,
                t => t.UpgradeFile
                ).OrderByDescending(t => t.UpgradeFile.filetype);
            UpgradeTask candidateTask = null;
            bool stopTrace = false;

            try
            {
                foreach (var task in updateTasksPerDevice)
                {
                    if (candidateTask == null
                     || candidateTask.UpgradeFile.filetype != task.UpgradeFile.filetype)
                    {
                        //if (candidateTask != null && candidateTask.Status == UpgradeStatus.TBD)
                        //{
                        // put candidateTask to working queue
                        //PutUpgradeTaskInWorkingQueue(upgradeTaskDataModel, candidateTask);
                        //}
                        candidateTask = task;
                        stopTrace = false;
                    }
                    else if (!stopTrace)
                    {
                        // similar tasks:
                        // case 1.1: (x, T)         // remove
                        //           (1, T)         // pick
                        // case 1.2: (1, C or W)    
                        //           (1, T)         // remove
                        // case 1.3: (1, C or W)
                        //           (2, T)         // pick if 1 is C
                        // case 2.1: (x, T)         // remove
                        //           (x, T)         // remove
                        //           (1, T)         // pick
                        // case 2.2: (1, C or W)    
                        //           (x, T)         // remove
                        //           (1, T)         // remove
                        // case 2.3: (1, C or W)
                        //           (x, T)         // remove
                        //           (2, T)         // pick if 1 is C
                        if (task.Status == UpgradeStatus.TBD)
                        {
                            MarkUpgradeTaskRemoved(upgradeTaskDataModel, task);
                        }
                        else
                        {   // Complete or Working
                            if (candidateTask.UpgradeID == task.UpgradeID)
                            {
                                MarkUpgradeTaskRemoved(upgradeTaskDataModel, candidateTask);
                            }
                            else
                            {
                                if (task.Status != UpgradeStatus.Complete)
                                {
                                    candidateTask = task;
                                }
                            }
                            stopTrace = true;
                        }
                    }
                }
            } catch (Exception) { return; }

            //if (candidateTask != null && candidateTask.Status == UpgradeStatus.TBD)
            //{
            //    // put candidateTask to working queue
            //    //PutUpgradeTaskInWorkingQueue(upgradeTaskDataModel, candidateTask);
            //}
            if (device.online == 0)
                return;

            var taskInProgress = (from t in updateTasksPerDevice.AsParallel()
                                  where t.Status == UpgradeStatus.InProgress
                                  select t).FirstOrDefault();
            if (taskInProgress == null)
            {
                candidateTask = (from t in updateTasksPerDevice.AsParallel()
                                 where t.Status == UpgradeStatus.TBD
                                    && !t.DeletedByUser
                                 orderby t.ID
                                 select t).FirstOrDefault();
                if (candidateTask != null)
                    PutUpgradeTaskInWorkingQueue(upgradeTaskDataModel, candidateTask);
            }
        }

        private void UpgradeTask(CancellationToken cancelToken)
        {
            IUpgradeTasksModel upgradeTaskDataModel = ServiceLocator.Current.GetInstance<IUpgradeTasksModel>();
            var deviceComparer = new DeviceEqualityComparer();

            try
            {
                FtpServer.Instance.NewDataConnection += FtpServer_NewDataConnection;
                FtpServer.Instance.ClosedDataConnection += FtpServer_ClosedDataConnection;
                FtpServer.Instance.SentData += FtpServer_SentData;

                upgradeTaskDataModel.RefillDataAsync().Wait();
                //upgradeTaskDataModel.Data.Where(
                upgradeTaskDataModel.Select(
                    t => t.Status == UpgradeStatus.InProgress
                      && (t.LastStartTime == null
                       || t.LastStartTime < _serviceStartTime)).ToList()
                    .ForEach(task => MarkUpgradeTaskTBD(upgradeTaskDataModel, task));

                while (true)
                {
                    _token.ThrowIfCancellationRequested();

                    // 刪除被使用者標記要刪除且非運行中的 Task
                    //var tasksToBeRemoved = upgradeTaskDataModel.Data.Where(
                    var tasksToBeRemoved = upgradeTaskDataModel.Select(
                        t => t.DeletedByUser
                          && t.Status != UpgradeStatus.InProgress).ToList();
                    if (tasksToBeRemoved != null)
                        upgradeTaskDataModel.Delete(tasksToBeRemoved);

                    // 挑選出狀態為運行中但超過 20 秒未動作的 tasks，將其標記為 TBD
                    upgradeTaskDataModel.Select(
                        t => t.Status == UpgradeStatus.InProgress
                          && (t.LastUpdateTime == null
                          || (DateTime.Now - (DateTime)t.LastUpdateTime).TotalSeconds > 20)).ToList()
                        .ForEach(t => { MarkUpgradeTaskTBD(upgradeTaskDataModel, t); });

                    //var upgradeTasks = upgradeTaskDataModel.Data.Where(
                    //    t => t.Status != UpgradeStatus.Removed).ToList();
                    var upgradeTasks = upgradeTaskDataModel.Select(
                        t => t.Status != UpgradeStatus.Removed,
                        t => t.Device,
                        t => t.UpgradeFile).ToList();
                    if (upgradeTasks != null)
                    {
                        var devices = (from t in upgradeTasks
                                       where t.Device.online == 1
                                       select t.Device).Distinct(deviceComparer).ToList();

                        //List<Task> tasks = new List<Task>();

                        foreach (var device in devices)
                        {
                            _token.ThrowIfCancellationRequested();

                            //tasks.Add(Task.Run(() => UpgradeTaskForEachDevice(upgradeTaskDataModel, device, cancelToken)));
                            UpgradeTaskForEachDevice(upgradeTaskDataModel, device, cancelToken);
                        }
                        //Task.WaitAll(tasks.ToArray());
                    }
                    Thread.Sleep(100);
                }
            }
            catch (OperationCanceledException) { }
            finally
            {
                FtpServer.Instance.NewDataConnection -= FtpServer_NewDataConnection;
                FtpServer.Instance.ClosedDataConnection -= FtpServer_ClosedDataConnection;
                FtpServer.Instance.SentData -= FtpServer_SentData;
            }
        }



        // TODO: BUG: 這個 TaskQueue 可能會積太多 task 還沒做完，系統就關閉。造成關閉視窗時會有 RaceOnRCWCleanup 的
        // Exception 產生。
        TaskQueue _upgradeTaskQueue = new TaskQueue();

        private async void FtpServer_SentData(object sender, FtpSentDataEventArgs e)
        {
            IUpgradeTasksModel upgradeTaskDataModel = ServiceLocator.Current.GetInstance<IUpgradeTasksModel>();
            await _upgradeTaskQueue.Enqueue(() => Task.Run(() =>
            {
                //var task = upgradeTaskDataModel.Select(
                var task = upgradeTaskDataModel.Data.Where(
                    t => t.Status == UpgradeStatus.InProgress
                      && t.Device.ip == e.Remote.Address.ToString()
                    //t => t.Device
                    ).FirstOrDefault();
                if (task != null)
                {
                    task.LastUpdateTime = DateTime.Now;
                    task.SentDataBytes = e.SentBytes;
                    try
                    {
                        upgradeTaskDataModel.UpdateAsync(
                            task,
                            t => t.SentDataBytes,
                            t => t.LastUpdateTime).Wait();
                        //DebugLog.TraceMessage(string.Format("Sent({0}) Total({1}) - {2} %)", e.SentBytes, e.FileSize, e.SentBytes * 100 / e.FileSize));
                    }
                    catch (Exception) { }
                }
            }
            ));
        }

        private async void FtpServer_ClosedDataConnection(object sender, FtpDataConnectionEventArgs e)
        {
            IUpgradeTasksModel upgradeTaskDataModel = ServiceLocator.Current.GetInstance<IUpgradeTasksModel>();

            await _upgradeTaskQueue.Enqueue(() => Task.Run(() =>
            {
                try
                {
                    //var task = upgradeTaskDataModel.Select(
                    var task = upgradeTaskDataModel.Data.Where(
                        t => t.Status == UpgradeStatus.InProgress
                          && t.Device.ip == e.Remote.Address.ToString()
                        //t => t.Device,
                        //t => t.UpgradeFile
                        ).FirstOrDefault();
                    if (task != null)
                    {
                        if (task.UpgradeFile == null || task.UpgradeFile.FileSize == 0)
                        {
                            MarkUpgradeTaskRemoved(upgradeTaskDataModel, task);
                            return;
                        }

                        if (task.UpgradeFile.FileSize == task.SentDataBytes)
                            MarkUpgradeTaskComplete(upgradeTaskDataModel, task);
                        else
                            MarkUpgradeTaskTBD(upgradeTaskDataModel, task);
                    }
                }
                catch (Exception) { }
            }
            ));
        }

        private async void FtpServer_NewDataConnection(object sender, FtpDataConnectionEventArgs e)
        {
            IUpgradeTasksModel upgradeTaskDataModel = ServiceLocator.Current.GetInstance<IUpgradeTasksModel>();

            await _upgradeTaskQueue.Enqueue(() => Task.Run(() =>
            {
                try
                {
                    // TODO: check 存取 upgradeTaskDataModel.Data 是否需要 mutex 保護
                    var task = upgradeTaskDataModel.Data.Where(
                        t => t.Status == UpgradeStatus.InProgress
                          && t.Device.ip == e.Remote.Address.ToString()
                        //t => t.Device
                        ).FirstOrDefault();
                    if (task != null)
                        UpdateUpgradeTaskLastUpdateTime(upgradeTaskDataModel, task);
                }
                catch (Exception) { }
            }
            ));
        }

        private void MarkUpgradeTaskRemoved(
            IUpgradeTasksModel dataModel, UpgradeTask task)
        {
            task.Status = UpgradeStatus.Removed;
            try
            {
                //if (!task.DeletedByUser)
                    dataModel.UpdateAsync(task, t => t.Status).Wait();
                //else
                //    dataModel.Delete(task);
            }
            catch (Exception) { }
        }

        private void MarkUpgradeTaskComplete(
            IUpgradeTasksModel dataModel, UpgradeTask task)
        {
            task.Status = UpgradeStatus.Complete;
            task.LastUpdateTime = DateTime.Now;
            try
            {
                //if (!task.DeletedByUser)
                    dataModel.UpdateAsync(
                        task,
                        t => t.Status,
                        t => t.LastUpdateTime).Wait();
                //else
                //    dataModel.Delete(task);
            }
            catch (Exception) { }
        }

        private void MarkUpgradeTaskTBD(
            IUpgradeTasksModel dataModel, UpgradeTask task)
        {
            task.Status = UpgradeStatus.TBD;
            task.LastStartTime = task.LastUpdateTime = null;
            task.SentDataBytes = 0;
            try
            {
                //if (!task.DeletedByUser)
                    dataModel.UpdateAsync(
                        task, 
                        t => t.Status,
                        t => t.LastStartTime,
                        t => t.LastUpdateTime,
                        t => t.SentDataBytes).Wait();
                //else
                //    dataModel.Delete(task);
            }
            catch (Exception) { }
        }

        private void UpdateUpgradeTaskLastUpdateTime(
            IUpgradeTasksModel dataModel, UpgradeTask task)
        {
            task.LastUpdateTime = DateTime.Now;
            try
            {
                dataModel.UpdateAsync(
                    task, 
                    t => t.LastUpdateTime).Wait();
            }
            catch (Exception) { }
        }

        private void PutUpgradeTaskInWorkingQueue(
            IUpgradeTasksModel dataModel, UpgradeTask task)
        {
            task.Status = UpgradeStatus.InProgress;
            task.LastStartTime = task.LastUpdateTime = DateTime.Now;
            task.SentDataBytes = 0;
            try
            {
                dataModel.UpdateAsync(
                    task,
                    t => t.Status,
                    t => t.LastStartTime,
                    t => t.LastUpdateTime,
                    t => t.SentDataBytes).Wait();
            }
            catch (Exception) { }

            Func<string, string, string, CancellationToken, Task<bool>> SendNotificationFunc = null;
            switch (task.UpgradeFile.filetype)
            {
                case (int)UpgradeFileType.SoftwareUpgrade:
                    SendNotificationFunc = HttpClient.SendFirmwareUpgradeNotificationAsync;
                    break;

                case (int)UpgradeFileType.AddressBookUpgrade:
                    SendNotificationFunc = HttpClient.SendAddressBookUpgradeNotificationAsync;
                    break;

                case (int)UpgradeFileType.ScreenSaverUpgrade:
                    SendNotificationFunc = HttpClient.SendResourceUpgradeNotificationAsync;
                    break;

                case (int)UpgradeFileType.SecurityCardListUpgrade:
                    SendNotificationFunc = HttpClient.SendCardListUpgradeNotificationAsync;
                    break;
            }

            if (SendNotificationFunc != null)
            {
                this.Add(new NetworkTask
                {
                    IP = task.Device.ip,
                    TaskFunc = async () =>
                    {
                        if (!await SendNotificationFunc(task.Device.ip,
                            Config.Instance.LocalIP, task.UpgradeFile.filepath, default(CancellationToken)).ConfigureAwait(false))
                        {
                            MarkUpgradeTaskTBD(dataModel, task);
                            return;
                        }

                        UpdateUpgradeTaskLastUpdateTime(dataModel, task);
                    }
                });
            }
        }

        // Consumer Task
        private void ConsumerMainTask(CancellationToken cancelToken)
        {
            try
            {
                while (true)
                {
                    _token.ThrowIfCancellationRequested();

                    NetworkTask task;
                    while (_networkTasks.TryDequeue(out task))
                    {
                        if (!_tasksPerIP.ContainsKey(task.IP))
                        {
                            _tasksPerIP.Add(task.IP, new TaskQueue());
                        }
                        task.Task = _tasksPerIP[task.IP].Enqueue(task.TaskFunc);
                    }
                    Thread.Sleep(1);
                }
            }
            catch (OperationCanceledException) { }
        }
    }

    public class NetworkTask
    {
        public string IP { get; set; }
        public Func<Task> TaskFunc { get; set; }
        public Task Task { get; set; }
    }
}
