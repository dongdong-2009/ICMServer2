using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer.Services.Net
{
    public class VideoTalkService : IVideoTalkService
    {
        enum STATE
        {
            STOPPED,
            STARTED
        }

        private static volatile VideoTalkService _instance;
        private static object _syncRoot = new Object();
        private STATE _state = STATE.STOPPED;
        private object _lock = new object();
        private readonly IDeviceDataService _deviceDataService;
        private readonly IDataService<leaveword> _videoMessageDataService;
        // private System.Windows.Forms.Form _parentForm;

        private VideoTalkService(
            IDeviceDataService deviceDataService,
            IDataService<leaveword> videoMessageDataService)
        {
            this._deviceDataService = deviceDataService;
            this._videoMessageDataService = videoMessageDataService;
        }

        private VideoTalkService() : this(new DeviceDataService(), new VideoMessageDataService())
        {
        }

        public static VideoTalkService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new VideoTalkService();
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
                    try
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();

                        StringBuilder ip = new StringBuilder(Config.Instance.LocalIP);
                        StringBuilder AddrName = new StringBuilder("000000000000");
                        DebugLog.TraceMessage(string.Format("done: {0}", stopwatch.Elapsed));
                        NativeMethods.Dll_SetLocalIpAddress(ip, AddrName, (uint)Config.Instance.SIPCommunicationPort);  // 花了兩秒
                        DebugLog.TraceMessage(string.Format("done: {0}", stopwatch.Elapsed));
                        _CallBack = new pCallBackPro(DllCallBackFunc);
                        NativeMethods.dll_init(_CallBack);
                        DebugLog.TraceMessage(string.Format("done: {0}", stopwatch.Elapsed));
                        NativeMethods.Dll_initCamera();
                        DebugLog.TraceMessage(string.Format("done: {0}", stopwatch.Elapsed));
                        int ndevice = NativeMethods.GetCaptureDeviceCount();
                        DebugLog.TraceMessage(string.Format("done: {0}", stopwatch.Elapsed));
                        if (ndevice > 0)
                            NativeMethods.Dll_SetCameraDeviceId(0); // 花了 1 秒
                        DebugLog.TraceMessage(string.Format("done: {0}", stopwatch.Elapsed));
                        _state = STATE.STARTED;
                    }
                    catch (Exception) { }
                }

                return _state == STATE.STARTED;
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (_state == STATE.STARTED)
                {
                    _state = STATE.STOPPED;
                }
            }
        }

        #region ReceivedNewVideoMessageEvent
        public event EventHandler<EventArgs> ReceivedNewVideoMessageEvent;
        // 發出收到新的留影留言事件
        private void RaiseReceivedNewVideoMessageEvent()
        {
            if (ReceivedNewVideoMessageEvent != null)
                ReceivedNewVideoMessageEvent(this, EventArgs.Empty);
        }
        #endregion

        #region VideoMessageHasBeenReadEvent
        public event EventHandler<EventArgs> VideoMessageHasBeenReadEvent;
        private void RaiseVideoMessageHasBeenReadEvent(leaveword videoMessage)
        {
            if (VideoMessageHasBeenReadEvent != null)
                VideoMessageHasBeenReadEvent(this, EventArgs.Empty);
        }
        #endregion

        #region ReceivedIncomingCallEvent
        public event EventHandler<ReceivedIncommingCallEventArgs> ReceivedIncomingCallEvent;
        private void RaiseReceivedIncomingCallEvent(Device device)
        {
            if (ReceivedIncomingCallEvent != null)
                ReceivedIncomingCallEvent(this, new ReceivedIncommingCallEventArgs { ClientDevice = device });
        }
        #endregion

        #region AcceptedIncomingCallEvent
        public event EventHandler<EventArgs> AcceptedIncomingCallEvent;
        private void RaiseAcceptedIncomingCallEvent()
        {
            if (AcceptedIncomingCallEvent != null)
                AcceptedIncomingCallEvent(this, EventArgs.Empty);
        }
        #endregion

        #region ReceivedHangUpEvent
        public event EventHandler<EventArgs> ReceivedHangUpEvent;
        private void RaiseReceivedHangUpEvent()
        {
            if (ReceivedHangUpEvent != null)
                ReceivedHangUpEvent(this, EventArgs.Empty);
        }
        #endregion

        #region ReceivedOutgoingCallTimeoutEvent
        public event EventHandler<EventArgs> ReceivedOutgoingCallTimeoutEvent;
        private void RaiseReceivedOutgoingCallTimeoutEvent()
        {
            if (ReceivedOutgoingCallTimeoutEvent != null)
                ReceivedOutgoingCallTimeoutEvent(this, EventArgs.Empty);
        }
        #endregion

        private pCallBackPro _CallBack;

        private void OpenCameraByDeviceType(int? deviceType)
        {
            switch (deviceType)
            {
                case (int)DeviceType.Door_Camera:
                case (int)DeviceType.Lobby_Phone_Unit:
                case (int)DeviceType.Lobby_Phone_Building:
                case (int)DeviceType.Lobby_Phone_Area:
                case (int)DeviceType.Indoor_Phone:
                case (int)DeviceType.Administrator_Unit:
                case (int)DeviceType.Indoor_Phone_SD:
                    NativeMethods.Dll_OpenCamera();
                    break;
            }
        }

        private void DllCallBackFunc(ref sIteDataParam param)
        {
            //bool isVideoMessage = d.lwstatus;
            //CallBackValue.XValue = d.msgtype;
            DebugLog.TraceMessage(string.Format("msgType: {0}", param.msgtype));
            switch (param.msgtype)
            {
                case (int)VideoTalkOperation.emMEET_REQUEST:    // received a phone call request
                    try
                    {
                        string ip = param.ip;
                        var device = this._deviceDataService.Select((d) => d.ip == ip).FirstOrDefault();
                        if (device != null)
                        {
                            // TODO: 跳出來電提示
                            switch (device.type)
                            {
                                case (int)DeviceType.Door_Camera:
                                case (int)DeviceType.Lobby_Phone_Unit:
                                case (int)DeviceType.Lobby_Phone_Building:
                                case (int)DeviceType.Lobby_Phone_Area:
                                    if (Config.Instance.IsMulticastEnabled && !string.IsNullOrEmpty(device.group))
                                    {
                                        NativeMethods.Dll_ConfigMulticastIP(new StringBuilder(device.group));
                                    }
                                    break;
                            }

                            OpenCameraByDeviceType(device.type);

                            this.RaiseReceivedIncomingCallEvent(device);
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    break;

                case (int)VideoTalkOperation.emMEET_ACCEPT: // accept a phone call
                    this.RaiseAcceptedIncomingCallEvent();
                    break;

                case (int)VideoTalkOperation.emMEET_REFUSE:     // 拒接 (還不知道什麼時機會收到這訊息)
                case (int)VideoTalkOperation.emMEET_STOP:
                    try
                    {
                        NativeMethods.Dll_CloseCamera();
                        this.RaiseReceivedHangUpEvent();
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    break;

                case (int)VideoTalkOperation.emMEET_TIMEOVER:   // 對方超時未接/對方裝置未開機時
                    this.RaiseReceivedOutgoingCallTimeoutEvent();
                    break;

                case (int)VideoTalkOperation.emPHONERECORDS_TO:
                    try
                    {
                        string ip = param.ip;
                        var device = this._deviceDataService.Select((d) => d.ip == ip).FirstOrDefault();
                        if (device != null)
                        {
                            OpenCameraByDeviceType(device.type);
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    break;

                case (int)VideoTalkOperation.emLW_REQUEST:
                    {
                        string targetDeviceAddress = (string)param.lParam;
                        string sourceDeviceIP = (string)param.ip;
                        string filePath = (string)param.wParam;

                        leaveword videoMessage = new leaveword();
                        videoMessage.filenames = filePath;
                        videoMessage.dst_addr = targetDeviceAddress;
                        videoMessage.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        videoMessage.readflag = 0;
                        try
                        {
                            var srcDevice = this._deviceDataService.Select((d) => d.ip == sourceDeviceIP).FirstOrDefault();
                            if (srcDevice != null)
                            {
                                videoMessage.src_addr = srcDevice.roomid;
                                this._videoMessageDataService.Insert(videoMessage);
                                // TODO: move the job to a standalone service
                                SendVideoMessageCountChangedNotification(targetDeviceAddress);
                                RaiseReceivedNewVideoMessageEvent();
                            }
                            else
                            {
                                VideoMessageDataService.DeleteVideoMessageFiles(filePath);
                            }
                        }
                        catch (Exception) { }
                    }
                    break;

                case (int)VideoTalkOperation.emLW_DOWNLOAD_REQUEST:
                    try
                    {
                        int videoMessageId = int.Parse(param.lParam);
                        var videoMessage = this._videoMessageDataService.Select((msg) => msg.id == videoMessageId).FirstOrDefault();
                        if (videoMessage != null)
                        {
                            param.filepath = videoMessage.filenames;
                            if (videoMessage.readflag != 1)
                            {
                                videoMessage.readflag = 1;
                                this._videoMessageDataService.Update(videoMessage);
                                SendVideoMessageCountChangedNotification(videoMessage.dst_addr);
                                RaiseVideoMessageHasBeenReadEvent(videoMessage);
                            }
                        }
                    }
                    catch (Exception) { }
                    break;
            }
        }

        TaskQueue _sendVideoMessageCountTaskQueue = new TaskQueue();

        private void SendVideoMessageCountChangedNotification(string targetDeviceAddress)
        {
            Task t = _sendVideoMessageCountTaskQueue.Enqueue(
                () => Task.Run(() =>
                {
                    var NewVideoMessageCount = this._videoMessageDataService.Select(
                        (msg) => msg.dst_addr == targetDeviceAddress
                              && (!msg.readflag.HasValue || msg.readflag == 0)).Count();
                    var targetDevices = _deviceDataService.Select((d) => d.roomid.StartsWith(targetDeviceAddress));
                    List<Task> tasks = new List<Task>();
                    foreach (var device in targetDevices)
                    {
                        if (device.type.HasValue
                         && (device.type == (int)DeviceType.Indoor_Phone || device.type == (int)DeviceType.Indoor_Phone_SD))
                        {
                            tasks.Add(ICMServer.Net.HttpClient.SendNewVideoMsgCountAsync(device.ip, NewVideoMessageCount));
                        }
                    }
                    Task.WaitAll(tasks.ToArray());
                }));
        }

        public void SetVideoWindow(Control control)
        {
            if (control == null)
                return;

            HandleRef h = new HandleRef(control, control.Handle);
            NativeMethods.Dll_HWND(h.Handle);
        }

        //private string ClientIP { get; set; }
    }
}
