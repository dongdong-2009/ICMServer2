using ICMServer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICMServer
{
    // TODO: CPU 使用率過高，試著想更好的演算法
    public class Heartbeat
    {
        class DeviceStatus
        {
            internal Device DeviceEntity { get; private set; }
            internal DateTime LastPollingTime { get; set; }
            internal DateTime LastResponseTime { get; set; }

            internal int PollingCount { get; set; }
            internal int ResponseCount { get; set; }

            public DeviceStatus(Device Device)
            {
                DeviceEntity = Device;
                try
                {
                    IPAddress address = IPAddress.Parse(Device.ip);
                    IPEndPoint = new IPEndPoint(address, Port);
                }
                catch (Exception) { }
            }

            public void HasResponsed()
            {
                LastResponseTime = DateTime.Now;
                ResponseCount++;

                if (DeviceEntity.online != 1)
                {
                    using (var db = new ICMDBContext())
                    {
                        try
                        {
                            db.Devices.Attach(DeviceEntity);
                            DeviceEntity.online = 1;
                            db.SaveChanges();
                        }
                        catch (Exception) { }
                    }
                }
            }

            public IPEndPoint IPEndPoint { get; private set; }

            public bool Online
            {
                get { return DeviceEntity.online == 1; }
            }

            public void HasPolled()
            {
                if (LastPollingTime > LastResponseTime
                 && (LastPollingTime - LastResponseTime).TotalSeconds >= 3)
                {
                    if (DeviceEntity.online != 0)
                    {
                        Task.Run(() =>
                            {
                                using (var db = new ICMDBContext())
                                {
                                    try
                                    {
                                        db.Devices.Attach(DeviceEntity);
                                        DeviceEntity.online = 0;
                                        db.SaveChanges();
                                    }
                                    catch (Exception) { }
                                }
                            });
                    }
                }
                LastPollingTime = DateTime.Now;
                PollingCount++;
            }

            public override string ToString()
            {
                return string.Format("{0}:{1}-p({2},{3})-r({4},{5}), {6}s",
                    DeviceEntity.roomid, DeviceEntity.ip,
                    PollingCount, LastPollingTime.ToString("mmssffff"),
                    ResponseCount, LastResponseTime.ToString("mmssffff"),
                    (DateTime.Now - LastPollingTime).TotalSeconds);
            }
        }

        public const int Port = 49201;
        enum STATE
        {
            STOPPED,
            STARTED
        }

        private static volatile Heartbeat _instance;
        private static object _syncRoot = new Object();
        private STATE _state = STATE.STOPPED;
        private object _lock = new object();

        CancellationTokenSource _tokenSource = new CancellationTokenSource();
        CancellationToken _token;
        ConcurrentBag<Task> _tasks = new ConcurrentBag<Task>();
        UdpClient _udpClient = new UdpClient(Port);// Set udp port

        private Heartbeat()
        {
            _token = _tokenSource.Token;
        }

        public static Heartbeat Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new Heartbeat();
                    }
                }

                return _instance;
            }
        }

        public bool Start()
        {
            lock (_lock)
            {
                if (_state == STATE.STOPPED)
                {
                    _state = STATE.STARTED;

                    _tasks.Add(Task.Run(() => { MainTask(_token); }));
                }
                return _state == STATE.STARTED;
            }
        }

        private void StopChildTasks(ConcurrentBag<Task> tasks, CancellationTokenSource CancelToken)
        {
            try
            {
                CancelToken.Cancel();
                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception) { }
        }

        private CancellationTokenSource StartChildTasks(out ConcurrentBag<Task> tasks, List<DeviceStatus> DevicesStatus)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            tasks = new ConcurrentBag<Task>
            {
                Task.Run(() => { PollingDevices(DevicesStatus, token); }),
                Task.Run(async () => { await UdpServer(DevicesStatus, token); })
            };

            return tokenSource;
        }

        private void MainTask(CancellationToken CancelToken)
        {
            List<DeviceStatus> DeviceStatus = new List<DeviceStatus>();
            CancellationTokenSource tokenSourceForChild = StartChildTasks(out ConcurrentBag<Task> tasks, DeviceStatus);

            try
            {
                while (true)
                {
                    if (CancelToken.IsCancellationRequested == true)
                    {
                        StopChildTasks(tasks, tokenSourceForChild);
                        CancelToken.ThrowIfCancellationRequested();
                    }

                    using (var db = new ICMDBContext())
                    {
                        var DevicesInDB = (from d in db.Devices
                                           where d.type != 0
                                           select d).ToList();
                        if (DeviceStatus.Count() != DevicesInDB.Count())
                        {
                            StopChildTasks(tasks, tokenSourceForChild);
                            DeviceStatus = (from d in DevicesInDB
                                            select new DeviceStatus(d)).ToList();
                            tokenSourceForChild = StartChildTasks(out tasks, DeviceStatus);
                        }

                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception) { }
        }

        private void PollingDevices(List<DeviceStatus> DevicesStatus, CancellationToken CancelToken)
        {
            if (DevicesStatus.Count == 0)
                return;

            try
            {
                int i = 0;
                byte[] sendData = Encoding.ASCII.GetBytes("*");

                while (true)
                {
                    CancelToken.ThrowIfCancellationRequested();

                    var Devices = (from d in DevicesStatus
                                   where ((DateTime.Now - d.LastPollingTime).TotalSeconds >= 10)
                                      || ((d.Online == false && (DateTime.Now - d.LastPollingTime).TotalSeconds >= 1))
                                   select d);

                    foreach (var Device in Devices)
                    {
                        //Console.WriteLine(Device.ToString());
                        CancelToken.ThrowIfCancellationRequested();

                        _udpClient.Send(sendData, sendData.Length, Device.IPEndPoint);

                        Device.HasPolled();

                        if (++i % 30 == 0)
                            Thread.Sleep(10);
                    }

                    Thread.Sleep(10);
                }
            }
            catch (Exception) { }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (_state == STATE.STARTED)
                {
                    _state = STATE.STOPPED;

                    try
                    {
                        _tokenSource.Cancel();
                        Task.WaitAll(_tasks.ToArray());
                    }
                    catch (Exception) { }
                    //finally
                    //{
                    //    _tokenSource.Dispose();
                    //}
                }
            }
        }

        private async Task UdpServer(List<DeviceStatus> DevicesStatus, CancellationToken CancelToken)
        {
            if (DevicesStatus.Count == 0)
                return;

            try
            {
                while (true) // 無窮迴圈，不断接收信息並顯示到螢幕上。
                {
                    CancelToken.ThrowIfCancellationRequested();

                    try
                    {
                        var result = await _udpClient.ReceiveAsync().WithCancellation(CancelToken); // 接收對方傳來的封包。

                        string data = Encoding.ASCII.GetString(result.Buffer);
                        IPEndPoint remote = result.RemoteEndPoint;

                        if (data == "*")
                        {
                            await Task.Run(() =>
                            {
                                var Device = (from d in DevicesStatus
                                              where d.DeviceEntity.ip == remote.Address.ToString()
                                              select d).FirstOrDefault();
                                if (Device != null)
                                {
                                    Device.HasResponsed();
                                }
                            });
                        }
                        // 将该封包以 UTF8 的格式解碼為字串，並顯示到螢幕上。
                        //Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
        }
    }
}
