using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.Services.Net;
using ICMServer.WPF.Messages;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class FormVideoTalkViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IDeviceDataService _dataService;
        private object m_SyncRoot = new object();

        private TimeSpan RingTimeout = TimeSpan.FromSeconds(15);    // 未接通響鈴狀態最長允許時間值
        private readonly TimeoutAction _RingTimeoutAction;
        //private System.Threading.Timer RingTimeoutTimer = null;
        //private object SyncRootForRingTimeroutTimer = new object();

        private TimeSpan TalkTimeout = TimeSpan.FromSeconds(60);    // 已接通對話狀態最長允許時間值
        private readonly TimeoutAction _TalkTimeoutAction;

        public FormVideoTalkViewModel(
            IDeviceDataService _dataService,
            IDialogService dialogService)
        {
            this._dialogService = dialogService;
            this._dataService = _dataService;
            VideoTalkService.Instance.ReceivedIncomingCallEvent += _videoTalkService_ReceivedIncomingCallEvent;
            VideoTalkService.Instance.AcceptedIncomingCallEvent += _videoTalkService_AcceptedIncomingCallEvent;
            VideoTalkService.Instance.ReceivedNewVideoMessageEvent += _videoTalkService_ReceivedNewVideoMessageEvent;
            VideoTalkService.Instance.VideoMessageHasBeenReadEvent += _videoTalkService_VideoMessageHasBeenReadEvent;
            VideoTalkService.Instance.ReceivedHangUpEvent += _videoTalkService_ReceivedHangUpEvent;
            VideoTalkService.Instance.ReceivedOutgoingCallTimeoutEvent += _videoTalkService_ReceivedOutgoingCallTimeoutEvent;
            //this._videoTalkService = videoTalkService;
            this._RingTimeoutAction = new TimeoutAction(() =>
            {
                CallState callState;

                lock (m_SyncRoot)
                {
                    callState = this.CallState;
                }
                switch (callState)
                {
                    case CallState.ReceivedIncomingCall:
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            this.HangUpCommand.Execute(null);
                        });
                        break;

                    case CallState.Calling:
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            this.HangUpCommand.Execute(null);
                            Messenger.Default.Send(new ReceivedOutgoingCallTimeoutEvent());
                        });
                        break;
                }
            });
            this._TalkTimeoutAction = new TimeoutAction(() =>
            {
                CallState callState;

                lock (m_SyncRoot)
                {
                    callState = this.CallState;
                }

                switch (callState)
                {
                    case CallState.AcceptedIncomingCall:
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            this.HangUpCommand.Execute(null);
                        });
                        break;

                    case CallState.WatchingLiveVideo:
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            this.StopWatchLiveVideoCommand.Execute(null);
                        });
                        break;
                }
            });
        }

        private void _videoTalkService_VideoMessageHasBeenReadEvent(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => Messenger.Default.Send(new VideoMessageHasBeenReadEvent()));
        }

        class TimeoutAction
        {
            private System.Threading.Timer _TimeoutTimer = null;
            private object _SyncRoot = new object();
            private Action _Action;

            public TimeoutAction(Action action)
            {
                this._Action = action;
            }

            public void Start(TimeSpan timeout)
            {
                lock (_SyncRoot)
                {
                    if (_TimeoutTimer == null)
                        _TimeoutTimer = new System.Threading.Timer(new TimerCallback(delegate
                        {
                            if (this._Action != null)
                                this._Action.Invoke();

                            lock (_SyncRoot)
                            {
                                this._TimeoutTimer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1));
                            }

                        }));

                    this._TimeoutTimer.Change(timeout, TimeSpan.FromMilliseconds(-1));
                }
            }

            public void Stop()
            {
                lock (_SyncRoot)
                {
                    if (this._TimeoutTimer == null)
                        return;

                    this._TimeoutTimer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1));
                }
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="timeout">in milliseconds</param>
        //void StartRingTimeoutTimer(TimeSpan timeout)
        //{
        //    lock (SyncRootForRingTimeroutTimer)
        //    {
        //        if (RingTimeoutTimer == null)
        //            RingTimeoutTimer = new System.Threading.Timer(new TimerCallback(delegate
        //            {
        //                CallState callState;

        //                lock (m_SyncRoot)
        //                {
        //                    callState = this.CallState;
        //                }
        //                switch (callState)
        //                {
        //                    case CallState.ReceivedIncomingCall:
        //                        Application.Current.Dispatcher.Invoke(() =>
        //                        {
        //                            this.HangUpCommand.Execute(null);
        //                        });
        //                        break;

        //                    case CallState.Calling:
        //                        Application.Current.Dispatcher.Invoke(() =>
        //                        {
        //                            this.HangUpCommand.Execute(null);
        //                            Messenger.Default.Send(new ReceivedOutgoingCallTimeoutEvent());
        //                        });
        //                        break;
        //                }

        //                lock (SyncRootForRingTimeroutTimer)
        //                {
        //                    this.RingTimeoutTimer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1));
        //                }

        //            }));

        //        this.RingTimeoutTimer.Change(timeout, TimeSpan.FromMilliseconds(-1));
        //    }

        //}

        //void StopRingTimountTimer()
        //{
        //    lock (SyncRootForRingTimeroutTimer)
        //    {
        //        if (this.RingTimeoutTimer == null)
        //            return;

        //        this.RingTimeoutTimer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1));
        //    }
        //}

        private void _videoTalkService_ReceivedOutgoingCallTimeoutEvent(object sender, EventArgs e)
        {
            CallState callState;

            lock (m_SyncRoot)
            {
                callState = this.CallState;
            }
            switch (callState)
            {
                case CallState.Calling:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.HangUpCommand.Execute(null);
                        Messenger.Default.Send(new ReceivedOutgoingCallTimeoutEvent());
                    });
                    break;
            }
        }

        private void _videoTalkService_ReceivedHangUpEvent(object sender, EventArgs e)
        {
            lock (m_SyncRoot)
            {
                this.CallState = CallState.Idle;
                this.ClientDevice = null;
            }
            _RingTimeoutAction.Stop();
            _TalkTimeoutAction.Stop();
            //StopRingTimountTimer();
            Application.Current.Dispatcher.Invoke(() => Messenger.Default.Send(new ReceivedHangUpEvent()));
        }

        private void _videoTalkService_AcceptedIncomingCallEvent(object sender, EventArgs e)
        {
            lock (m_SyncRoot)
            {
                this.CallState = CallState.AcceptedIncomingCall;
            }
            _TalkTimeoutAction.Start(TalkTimeout);
            Application.Current.Dispatcher.Invoke(() => Messenger.Default.Send(new AcceptedIncomingCallEvent()));
        }

        private void _videoTalkService_ReceivedNewVideoMessageEvent(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => Messenger.Default.Send(new ReceivedNewVideoMessageEvent()));
        }

        private void _videoTalkService_ReceivedIncomingCallEvent(object sender, ReceivedIncommingCallEventArgs e)
        {
            lock (m_SyncRoot)
            {
                this.ClientDevice = e.ClientDevice;
                this.CallState = CallState.ReceivedIncomingCall;
            }

            _RingTimeoutAction.Start(RingTimeout);
            //StartRingTimeoutTimer(RingTimeout);
            Application.Current.Dispatcher.Invoke(() => Messenger.Default.Send(new ReceivedIncomingCallEvent()));
        }

        private void CallDevice(Device device)
        {
            if (device == null)
            {
                this._dialogService.ShowMessageBox(CulturesHelper.GetTextValue("TheDeviceDoesNotExist"));
                return;
            }

            lock (m_SyncRoot)
            {
                this.ClientDevice = device;
                this.CallState = CallState.Calling;
            }
            _RingTimeoutAction.Start(RingTimeout);
            //StartRingTimeoutTimer(RingTimeout);
            Application.Current.Dispatcher.Invoke(() => Messenger.Default.Send(new CallingEvent()));
            NativeMethods.Dll_DataOut((int)VideoTalkOperation.emMEET_REQUEST,
                                      new StringBuilder(device.ip),
                                      new StringBuilder(device.roomid.Replace("-", "")));
        }

        #region InputDigitNumberCommand
        private RelayCommand<string> _InputDigitNumberCommand;
        public RelayCommand<string> InputDigitNumberCommand
        {
            get
            {
                return _InputDigitNumberCommand ?? (_InputDigitNumberCommand = new RelayCommand<string>((number) =>
                {
                    if (DeviceAddress.Length < 17)
                    {
                        DeviceAddress += (DeviceAddress.Length % 3 == 2)
                                       ? ("-" + number)
                                       : number;
                    }
                },
                (number) =>
                {
                    bool valid = false;
                    lock (m_SyncRoot)
                    {
                        valid = this.CallState == CallState.Idle && (DeviceAddress.Length < 17);
                    }
                    return valid;
                }));
            }
        }
        #endregion

        #region ClearInputCommand
        private RelayCommand _ClearInputCommand;
        public RelayCommand ClearInputCommand
        {
            get
            {
                return _ClearInputCommand ?? (_ClearInputCommand = new RelayCommand(() => { DeviceAddress = ""; },
                    () =>
                    {
                        bool valid = false;
                        lock (m_SyncRoot)
                        {
                            valid = this.CallState == CallState.Idle;
                        }
                        return valid;
                    }));
            }
        }
        #endregion

        #region PickOnlineDeviceCommand
        private RelayCommand _PickOnlineDeviceCommand;
        public RelayCommand PickOnlineDeviceCommand
        {
            get
            {
                return _PickOnlineDeviceCommand ?? (_PickOnlineDeviceCommand = new RelayCommand(() =>
                {
                    _dialogService.ShowSelectOnlineDeviceDialog(this.DeviceType, (deviceAddress) => { this.DeviceAddress = deviceAddress; });
                },
                () =>
                {
                    bool valid = false;
                    lock (m_SyncRoot)
                    {
                        valid = this.CallState == CallState.Idle;
                    }
                    return valid;
                }));
            }
        }
        #endregion

        #region SwitchBusyModeCommand
        private ICommand _SwitchBusyModeCommand;
        public ICommand SwitchBusyModeCommand
        {
            get
            {
                return _SwitchBusyModeCommand ?? (_SwitchBusyModeCommand = AsyncCommand.Create(() =>
                {
                    NativeMethods.Dll_SetCurrentModeBusy(!NativeMethods.Dll_GetCurrentModeIsBusy());
                    this.RaisePropertyChanged(() => this.BusyMode);
                    return null;
                }));
            }
        }
        #endregion

        #region CallOutCommand
        private IAsyncCommand _CallOutCommand;
        public IAsyncCommand CallOutCommand
        {
            get
            {
                return _CallOutCommand ?? (_CallOutCommand = AsyncCommand.Create(() =>
                {
                    lock (m_SyncRoot)
                    {
                        if (this.CallState != CallState.Idle)
                            return null;
                    }

                    if (string.IsNullOrEmpty(this.DeviceAddress))
                    {
                        this._dialogService.ShowMessageBox(CulturesHelper.GetTextValue("PleaseEnterTheAddresOfTheCallingDevice"));
                        return null;
                    }

                    Device device = _dataService.Select((d) => d.roomid == this.DeviceAddress).FirstOrDefault();
                    CallDevice(device);
                    return null;
                },
                () =>
                {
                    bool valid = false;
                    lock (m_SyncRoot)
                    {
                        valid = this.CallState == CallState.Idle
                             && !string.IsNullOrEmpty(this.DeviceAddress)
                             && this.DeviceAddress.Length == 17;
                    }
                    return valid;
                }));
            }
        }
        #endregion

        #region CallOutByIPCommand
        private IAsyncCommand _CallOutByIPCommand;
        public IAsyncCommand CallOutByIPCommand
        {
            get
            {
                return _CallOutByIPCommand ?? (_CallOutByIPCommand = AsyncCommand.Create(() =>
                {
                    lock (m_SyncRoot)
                    {
                        if (this.CallState != CallState.Idle)
                            return null;
                    }

                    this._dialogService.ShowInputIPAddressDialog("", (ip) =>
                    {
                        Device device = _dataService.Select((d) => d.ip == ip).FirstOrDefault();
                        CallDevice(device);
                    });
                    return null;
                },
                () =>
                {
                    bool valid = false;
                    lock (m_SyncRoot)
                    {
                        valid = this.CallState == CallState.Idle;
                    }
                    return valid;
                }));
            }
        }
        #endregion

        #region AcceptIncomingCallCommand
        private IAsyncCommand _AcceptIncomingCallCommand;
        public IAsyncCommand AcceptIncomingCallCommand
        {
            get
            {
                return _AcceptIncomingCallCommand ?? (_AcceptIncomingCallCommand = AsyncCommand.Create(() =>
                {
                    string ip;
                    string deviceAddress;
                    lock (m_SyncRoot)
                    {
                        if (this.CallState != CallState.ReceivedIncomingCall || this.ClientDevice == null)
                            return null;

                        ip = this.ClientDevice.ip;
                        deviceAddress = this.ClientDevice.roomid;
                    }

                    NativeMethods.Dll_DataOut((int)VideoTalkOperation.emMEET_ACCEPT,
                                               new StringBuilder(ip),
                                               new StringBuilder(deviceAddress));
                    return null;
                },
                () =>
                {
                    bool valid = false;
                    lock (m_SyncRoot)
                    {
                        valid = (this.CallState == CallState.ReceivedIncomingCall && this.ClientDevice != null);
                    }
                    return valid;
                }));
            }
        }
        #endregion

        #region HangUpCommand
        private IAsyncCommand _HangUpCommand;
        public IAsyncCommand HangUpCommand
        {
            get
            {
                return _HangUpCommand ?? (_HangUpCommand = AsyncCommand.Create(() =>
                {
                    string ip;
                    string deviceAddress;
                    lock (m_SyncRoot)
                    {
                        if (this.ClientDevice == null)
                            return null;

                        switch (this.CallState)
                        {
                            case CallState.ReceivedIncomingCall:
                            case CallState.AcceptedIncomingCall:
                            case CallState.Calling:
                                break;

                            default:
                                return null;
                        }

                        ip = this.ClientDevice.ip;
                        deviceAddress = this.ClientDevice.roomid;
                    }

                    NativeMethods.Dll_DataOut((int)VideoTalkOperation.emMEET_STOP,
                                               new StringBuilder(ip),
                                               new StringBuilder(deviceAddress));
                    return null;
                },
                () =>
                {
                    bool valid = false;
                    lock (m_SyncRoot)
                    {
                        if (this.ClientDevice != null)
                        {
                            switch (this.CallState)
                            {
                                case CallState.ReceivedIncomingCall:
                                case CallState.AcceptedIncomingCall:
                                case CallState.Calling:
                                    valid = true;
                                    break;
                            }
                        }
                    }
                    return valid;
                }));
            }
        }
        #endregion

        #region WatchLiveVideoCommand
        private IAsyncCommand _WatchLiveVideoCommand;
        public IAsyncCommand WatchLiveVideoCommand
        {

            get
            {
                return _WatchLiveVideoCommand ?? (_WatchLiveVideoCommand = AsyncCommand.Create(() =>
                {
                    lock (m_SyncRoot)
                    {
                        if (this.CallState != CallState.Idle)
                            return null;
                    }

                    this._dialogService.ShowSelectOnlineDeviceWithCameraDialog((device) =>
                    {
                        if (device == null)
                            return;

                        lock (m_SyncRoot)
                        {
                            this.ClientDevice = device;
                            this.CallState = CallState.WatchingLiveVideo;
                        }

                        //Application.Current.Dispatcher.Invoke(() => Messenger.Default.Send(new CallingEvent()));
                        this._TalkTimeoutAction.Start(TalkTimeout);
                        NativeMethods.Dll_DataOut((int)VideoTalkOperation.emWATCH_REQUEST,
                                                  new StringBuilder(device.ip),
                                                  new StringBuilder(device.roomid.Replace("-", "")));
                    });

                    return null;
                },
                () =>
                {
                    bool valid = false;
                    lock (m_SyncRoot)
                    {
                        valid = this.CallState == CallState.Idle;
                    }
                    return valid;
                }));
            }
        }
        #endregion

        #region StopWatchLiveVideoCommand
        private IAsyncCommand _StopWatchLiveVideoCommand;
        public IAsyncCommand StopWatchLiveVideoCommand
        {

            get
            {
                return _StopWatchLiveVideoCommand ?? (_StopWatchLiveVideoCommand = AsyncCommand.Create(() =>
                {
                    string ip;
                    string deviceAddress;

                    lock (m_SyncRoot)
                    {
                        if (this.CallState != CallState.WatchingLiveVideo)
                            return null;

                        ip = this.ClientDevice.ip;
                        deviceAddress = this.ClientDevice.roomid;
                    }

                    NativeMethods.Dll_DataOut((int)VideoTalkOperation.emWATCH_STOP,
                                               new StringBuilder(ip),
                                               new StringBuilder(deviceAddress));
                    return null;
                },
                () =>
                {
                    bool valid = false;
                    lock (m_SyncRoot)
                    {
                        valid = this.CallState == CallState.WatchingLiveVideo;
                    }
                    return valid;
                }));
            }
        }
        #endregion

        #region OpenDoorCommand
        private IAsyncCommand _OpenDoorCommand;
        public IAsyncCommand OpenDoorCommand
        {
            get
            {
                return _OpenDoorCommand ?? (_OpenDoorCommand = AsyncCommand.Create(() =>
                {
                    string ip;
                    string deviceAddress;
                    int? deviceType;
                    lock (m_SyncRoot)
                    {
                        if (this.ClientDevice == null)
                            return null;

                        ip = this.ClientDevice.ip;
                        deviceAddress = this.ClientDevice.roomid;
                        deviceType = this.ClientDevice.type;
                    }

                    if ((int)DeviceType.Lobby_Phone_Unit <= deviceType && deviceType <= (int)DeviceType.Lobby_Phone_Area)
                    {
                        var controlServer = this._dataService.Select(d => d.type == (int)DeviceType.Control_Server)
                                            .FirstOrDefault();
                        if (controlServer != null)
                            ICMServer.Net.HttpClient.SendOpenDoorAsync(ip, controlServer.roomid).RunSynchronously();
                    }
                    return null;
                },
                () =>
                {
                    bool valid = false;
                    lock (m_SyncRoot)
                    {
                        valid = (this.ClientDevice != null);
                    }
                    return valid;
                }));
            }
        }
        #endregion

        private string _DeviceAddress = "";
        public string DeviceAddress
        {
            get { return _DeviceAddress; }
            protected set { this.Set(ref _DeviceAddress, value); }
        }

        private Device _ClientDevice;
        public Device ClientDevice
        {
            get { return _ClientDevice; }
            set
            {
                if (this.Set(ref _ClientDevice, value))
                {
                    if (value != null)
                        this.DeviceAddress = value.roomid;
                    this.RaisePropertyChanged(() => this.ClientDeviceAddress);
                }
            }
        }

        public string ClientDeviceAddress
        {
            get { return _ClientDevice != null ? _ClientDevice.roomid : ""; }
        }

        private ICMServer.Models.DeviceType _DeviceType = ICMServer.Models.DeviceType.Indoor_Phone;
        public ICMServer.Models.DeviceType DeviceType
        {
            get { return _DeviceType; }
            set { this.Set(ref _DeviceType, value); }
        }

        private readonly ICMServer.Models.DeviceType[] _TalkableDeviceTypes =
        {
             ICMServer.Models.DeviceType.Lobby_Phone_Unit,
             ICMServer.Models.DeviceType.Lobby_Phone_Building,
             ICMServer.Models.DeviceType.Lobby_Phone_Area,
             ICMServer.Models.DeviceType.Indoor_Phone,
             ICMServer.Models.DeviceType.Administrator_Unit
        };

        public ICMServer.Models.DeviceType[] TalkableDeviceTypes
        {
            get { return _TalkableDeviceTypes; }
        }

        public bool BusyMode
        {
            get { return NativeMethods.Dll_GetCurrentModeIsBusy(); }
        }

        private CallState _CallState = CallState.Idle;
        public CallState CallState
        {
            get { return _CallState; }
            private set
            {
                if (this.Set(ref _CallState, value))
                {
                    this.AcceptIncomingCallCommand.RaiseCanExecuteChanged();
                    this.CallOutCommand.RaiseCanExecuteChanged();
                    this.ClearInputCommand.RaiseCanExecuteChanged();
                    this.InputDigitNumberCommand.RaiseCanExecuteChanged();
                    this.PickOnlineDeviceCommand.RaiseCanExecuteChanged();
                }
            }
        }
    }

    public enum CallState
    {
        Idle = 0,
        ReceivedIncomingCall,
        AcceptedIncomingCall,
        Calling,
        WatchingLiveVideo
    }

}
