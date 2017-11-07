using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class FormVideoTalk : Form
    {
        public static string CurIpAddr, CurAddress;
        public static int CallBackType;
        public string Address = "", addr = "";
        public static string IP, ADDR, lParam, wParam, filepath;
        public string PassIP, PassADDR, PassGroup;
        public static Values CallBackValue = new Values(0);
        public static bool OnCall = false;
        public static bool OnWatch = false;
        public static bool PickUp = false;
        public static int TimeOutInterval;

        private static volatile FormVideoTalk m_Instance;
        private static object m_SyncRoot = new Object();
        public event EventHandler<EventArgs> ReceivedNewVideoMessageEvent;

        // 发出收到新的留影留言事件
        internal void RaiseReceivedReadLeaveMsgs()
        {
            ReceivedNewVideoMessageEvent?.Invoke(this, new EventArgs());
        }

        public static FormVideoTalk Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (m_SyncRoot)
                    {
                        if (m_Instance == null)
                            m_Instance = new FormVideoTalk();
                    }
                }

                return m_Instance;
            }
        }

        public FormVideoTalk()
        {
            InitializeComponent();
        }

        public static void DllCallBackFunc(ref SIteDataParam d)
        {
            string ip = d.ip;
            string address = d.address;
            IP = d.ip;
            ADDR = d.address;
            lParam = d.lParam;
            wParam = d.wParam;
            filepath = d.filepath;
            if(OnCall)
            {
            }
            CallBackValue.Value = d.msgtype;
            d.filepath = filepath;
            if (filepath != "")
                ICMServer.FormVideoTalk.Instance.RaiseReceivedReadLeaveMsgs();
        }

        public class ValueChangedEventArgs : EventArgs
        {
            public readonly int NewValue;

            public ValueChangedEventArgs(int NewValue)
            {
                this.NewValue = NewValue;
            }
        }

        // 作用只是記录某值，並在该值有改變时呼叫 ValueChanged event handler
        public class Values
        {
            public Values(int InitialValue)
            {
                _value = InitialValue;
            }

            // 幹嘛的？
            public event EventHandler<ValueChangedEventArgs> ValueChanged;

            protected virtual void OnValueChanged(ValueChangedEventArgs e)
            {
                ValueChanged?.Invoke(this, e);
            }

            private int _value;

            public int Value
            {
                get { return _value; }
                set
                {
                    _value = value;
                    OnValueChanged(new ValueChangedEventArgs(_value));
                }
            }
        }

        //呼叫等待时长15S
        private void CallingTimerStart()
        {
            TimeOutInterval = 15;
            PickUp = false;
            var CallTimerThread = new Thread(TASK_COUNTDOWN)
            {
                IsBackground = true
            };
            CallTimerThread.Start();
        }

        private void TASK_COUNTDOWN(object obj)
        {
            do
            {
                if (PickUp)
                {
                    return;
                }
                Thread.Sleep(1000);
                TimeOutInterval--;
            } while (TimeOutInterval > 0);
            if (PickUp)
                return;
            else
            {
                StringBuilder ip = new StringBuilder();
                ip.Append(CurIpAddr);
                StringBuilder address = new StringBuilder();
                address.Append("");

                if (OnCall)
                    try
                    {
                        NativeMethods.Dll_DataOut(5, ip, address);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
            }
        }

        //VAL状态改变时处理
        private void Method(object sender, ValueChangedEventArgs e)
        {
            ICMDBContext db = new ICMDBContext();
            switch (e.NewValue)
            {

                case (int)VideoTalkOperation.emMEET_REQUEST:
                    OnCall = true;
                    string showAddr = "";
                    //将控件移到 Z 顺序的前面==显示控件。
                    this.BringToFront();
                    var Device = (from a in db.Devices
                                    where a.ip == IP
                                    select a).FirstOrDefault();                    
                    if (Device != null)
                    {
                        showAddr = DevicesAddressConverter.RoToChStr(Device.roomid);

                        if (1 <= Device.type && Device.type <= 4)
                        {
                            if (Config.Instance.IsMulticastEnabled && !string.IsNullOrEmpty(Device.group))
                            {
                                StringBuilder MultiIP = new StringBuilder();
                                MultiIP.Append(Device.group);
                                NativeMethods.Dll_ConfigMulticastIP(MultiIP);
                            }
                        }
                        if((4 <= Device.type && Device.type <= 7) || Device.type == 2)
                            NativeMethods.Dll_OpenCamera();
                        // Timer start
                        CallingTimerStart();
                        // Timer start
                        PicBoxSwitch("bmp4.bmp", true);
                        labelCallingInfo.Text = "From:" + showAddr;
                    }
                    break;

                case (int)VideoTalkOperation.emMEET_ACCEPT:
                    OnCall = true;
                    var queryType = (from a in db.Devices
                                    where a.ip == IP
                                    select a).FirstOrDefault();
                    if (queryType.type >= 2 && queryType.type <= 4)
                        break;
                    NativeMethods.Dll_OpenCamera();
                    PicBoxSwitch("bmp2.bmp", true);
                    TimeOutInterval = -1;
                    PickUp = true;
                    break;

                case (int)VideoTalkOperation.emMEET_STOP:
                    if (OnCall)
                    {
                        PicBoxSwitch("bmp5.bmp", true);
                        labelCallingInfo.Text = "";
                        NativeMethods.Dll_CloseCamera();
                        CurIpAddr = "";
                        CurAddress = "";
                        //将控件移到Z顺序后面
                        //this.SendToBack();
                        
                        System.Threading.Thread.Sleep(1000);
                        PicBoxSwitch("bmp1.bmp", true);
                        OnCall = false;
                    }
                    break;

                case (int)VideoTalkOperation.emWATCH_REQUEST:
                    NativeMethods.Dll_OpenCamera();
                    OnCall = true;
                    OnWatch = true;
                    PicBoxSwitch("bmp2.bmp", true);
                    labelCallingInfo.Text = "From:" + ADDR;
                    break;
                
                case (int)VideoTalkOperation.emWATCH_STOP:
                    NativeMethods.Dll_CloseCamera();
                    break;

                case (int)VideoTalkOperation.emLW_REQUEST:
                    string sIndoorAddress = lParam;
                    leaveword lw = new leaveword
                    {
                        filenames = wParam,
                        readflag = 0
                    };
                    try
                    {
                        var query = from a in db.Devices
                                    where a.ip == IP
                                    select a;
                        foreach (var dev in query)
                        {
                            lw.src_addr = dev.roomid;
                        }
                    }
                    catch
                    {
                        lw.src_addr = "Unknow";
                    }
                    lw.dst_addr = sIndoorAddress;

                    lw.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//tm.ToString();
                    db.Leavewords.Add(lw);
                    db.SaveChanges();
                    break;

                case (int)VideoTalkOperation.emLW_DOWNLOAD_REQUEST:
                    // Query Video msgs file path from database
                    int id = int.Parse(lParam);
                    try
                    {
                        var query = from a in db.Leavewords
                                    where a.id == id
                                    select a;
                        foreach (var msgs in query)
                        {
                            filepath = msgs.filenames;
                            msgs.readflag = 1;
                        }
                        db.SaveChanges();
                    }
                    catch
                    {
                        filepath = "";
                    }
                    break; 
                    // Query Video msgs file path from database
                case (int)VideoTalkOperation.emPHONERECORDS_FROM:
                    if (OnCall)
                        break;
                    CurIpAddr = IP;
                    CurAddress = ADDR;
                    break;

                case (int)VideoTalkOperation.emPHONERECORDS_TO:
                    PicBoxSwitch("bmp3.bmp", true);
                    // Timer start
                    CallingTimerStart();
                    // Timer start
                    break;

                default:
                    break;
            }
        }

        //视频界面图片处理
        private void PicBoxSwitch(string Filename, bool force = false)
        {
            if (pictureBox.InvokeRequired)
            {
                pictureBox.Invoke((Action)(() => { PicBoxSwitch(Filename); }));
            }
            else
            { 
                pictureBox.Image = Image.FromFile(".\\res\\" + Filename);
                pictureBox.Invalidate();
                pictureBox.Update();
                pictureBox.Refresh();
                Application.DoEvents();
            }
        }

        //监视
        private void BtnWatch_Click(object sender, EventArgs e)
        {
            if (OnCall || labelCallingInfo.Text != "")
                return;
            StringBuilder ip = new StringBuilder();
            StringBuilder addr = new StringBuilder();
            StringBuilder group = new StringBuilder();
            DialogWatchSelectDev form = new DialogWatchSelectDev(this);
            if (form.ShowDialog() == DialogResult.OK)
            {
                /*
                ICMDBContext db = new ICMDBContext();
                var Device = (from dev in db.Devices
                           where dev.type == 0
                           select dev).FirstOrDefault();
                CurIpAddr = Device.ip;
                */
                ip = ip.Append(PassIP);
                addr = addr.Append(PassADDR);
                ADDR = ConstructPosition(PassADDR);
                group = group.Append(PassGroup);
                if (Config.Instance.IsMulticastEnabled && !string.IsNullOrEmpty(PassGroup))
                {
                    StringBuilder MultiIP = new StringBuilder();
                    MultiIP.Append(group);
                    NativeMethods.Dll_ConfigMulticastIP(MultiIP);
                }
                try
                {
                    bool a = NativeMethods.Dll_DataOut(8, ip, addr);
                    OnCall = true;
                    OnWatch = true;
                    PicBoxSwitch("bmp2.bmp", true);
                    labelCallingInfo.Text = "From:" + ADDR;
                    //CallBackValue.Value = 8;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private string ConstructPosition(string address)
        {
            address = address.Insert(10, strings.shi);
            address = address.Insert(8, strings.ceng);
            address = address.Insert(6, strings.unit);
            address = address.Insert(4, strings.dong);
            address = address.Insert(2, strings.qu);
            address += strings.ext;
            return address;
        }

        //停止
        private void BtnStop_Click(object sender, EventArgs e)
        {
            StringBuilder ip = new StringBuilder();
            ip.Append(CurIpAddr);//"");
            StringBuilder address = new StringBuilder();
            address.Append("");

            if(OnCall)
            try
            {
                bool a = NativeMethods.Dll_DataOut(11, ip, address);
                OnCall = false;
                OnWatch = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormVideoTalk_Load(object sender, EventArgs e)
        {
            ComboBoxDev.SelectedIndex = 0;

            trackBarSpeaker.Maximum = 100;
            trackBarSpeaker.Value = NativeMethods.Dll_GetVolExport();
            trackBarMic.Maximum = 100;
            trackBarMic.Value = NativeMethods.Dll_GetVolImport();
            trackBarRing.Maximum = 100;
            trackBarRing.Value = NativeMethods.Dll_GetRingVol();

            CallBackValue.ValueChanged += Method;
            Form.CheckForIllegalCrossThreadCalls = false;

            // set video window handle
            try
            {
                HandleRef handleref = new HandleRef(pictureBox, pictureBox.Handle);
                NativeMethods.Dll_HWND(handleref.Handle);//unmanagedaddr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 點擊 IP 呼叫 按鍵
        private void BtnIPCall_Click(object sender, EventArgs e)
        {
            if (OnCall)
                return;

            DialogIPCall call = new DialogIPCall();
            if (call.ShowDialog() == DialogResult.OK)
            {
                using (ICMDBContext db = new ICMDBContext())
                {
                    string IpAddr = call.ReturnIP;

                    var Device = (from d in db.Devices
                                  where d.ip == IpAddr
                                  select d).FirstOrDefault();
                    if (Device != null)
                    {
                        StringBuilder ip = new StringBuilder(Device.ip);
                        StringBuilder address = new StringBuilder(Device.roomid);

                        try
                        {
                            bool a = NativeMethods.Dll_DataOut(1, ip, address);
                            OnCall = true;
                            CurIpAddr = ip.ToString();
                            CurAddress = address.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        // 開门
        private async void BtnUnlock_Click(object sender, EventArgs e)
        {
            if (OnCall && !OnWatch)
            {
                using (var db = new ICMDBContext())
                {
                    var Device = (from dev in db.Devices
                                     where dev.ip == CurIpAddr
                                     select dev).FirstOrDefault();
                    
                    if (Device != null && (int)DeviceType.Lobby_Phone_Unit <= Device.type && Device.type <= (int)DeviceType.Lobby_Phone_Area)
                    {
                        var controlServer = (from d in db.Devices
                                             where d.type == (int)DeviceType.Control_Server
                                             select d).FirstOrDefault();

                        bool result = await ICMServer.Net.HttpClient.SendOpenDoor(CurIpAddr, controlServer.roomid);
                    }
                }
            }
        }

        //选择设备
        private void BtnSelectDev_Click(object sender, EventArgs e)
        {
            DialogDevSelectCall form = new DialogDevSelectCall(this);
            form.ShowDialog();
        }

        //呼叫
        private void BtnCall_Click(object sender, EventArgs e)
        {
            Device Device;
            if (OnCall) // 通话中
                return;
            if (textBox.Text == "") // 未输入欲通话设备的地址
            {
                MessageBox.Show(strings.CantBeNull);
                return;
            }
            Address = DevicesAddressConverter.ChStrToRo(textBox.Text);
            Device = ICMDBContext.GetDeviceByAddress(Address);
            if (Device == null)
            {
                MessageBox.Show(strings.RoNotExist);    // 房号不存在
                return;
            }
            
            StringBuilder ip = new StringBuilder(Device.ip.ToString());
            StringBuilder address = new StringBuilder(Device.roomid.Replace("-", ""));
            try
            {
                NativeMethods.Dll_DataOut(1, ip, address);
                OnCall = true;
                CurIpAddr = ip.ToString();
                CurAddress = address.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //清除
        private void BtnClear_Click(object sender, EventArgs e)
        {
            Address = addr = "";
            textBox.Text = addr;
        }

        //挂断
        private void BtnHandUp_Click(object sender, EventArgs e)
        {
            StringBuilder ip = new StringBuilder();
            ip.Append(CurIpAddr);
            StringBuilder address = new StringBuilder();
            address.Append("");

            if (OnCall)
            try
            {
                NativeMethods.Dll_DataOut(5, ip, address);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 點擊 "接聽" 按鈕
        private void BtnPickUp_Click(object sender, EventArgs e)
        {
            if (CallBackValue.Value == 1)
            {
                StringBuilder ip = new StringBuilder();
                ip.Append(CurIpAddr);
                StringBuilder address = new StringBuilder();
                address.Append("");              

                try
                {
                    //接听接口
                    NativeMethods.Dll_DataOut(2, ip, address);
                    TimeOutInterval = -1;
                    PickUp = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void InputDigit(string Digit)
        {
            if (addr.Length < 20)
            {
                addr = string.Concat(addr, Digit);
                switch (addr.Length)
                {
                    case 2:
                        addr = string.Concat(addr, strings.qu);
                        break;
                    case 5:
                        addr = string.Concat(addr, strings.dong);
                        break;
                    case 8:
                        addr = string.Concat(addr, strings.unit);
                        break;
                    case 12:
                        addr = string.Concat(addr, strings.ceng);
                        break;
                    case 15:
                        addr = string.Concat(addr, strings.shi);
                        break;
                    case 18:
                        addr = string.Concat(addr, strings.ext);
                        break;
                    default:
                        break;
                }
                textBox.Text = addr;
            }
        }

        //数字
        private void BtnNo_Click(object sender, EventArgs e)
        {
            InputDigit((string)((Button)sender).Tag);
        }

        //置忙
        private void BtnBusy_Click(object sender, EventArgs e)
        {
            try
            {
                NativeMethods.Dll_SetCurrentModeBusy(!NativeMethods.Dll_GetCurrentModeIsBusy());
                if (NativeMethods.Dll_GetCurrentModeIsBusy())
                    BtnBusy.Image = Image.FromFile(".\\res\\busy_x.bmp");
                else
                    BtnBusy.Image = Image.FromFile(".\\res\\busy.bmp");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnPicSwitch(Button Btn, string Filename)
        {
            Btn.Image = Image.FromFile(".\\res\\" + Filename);
        }

        private void BtnPickUp_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnPickUp, "accept_x.bmp");
        }

        private void BtnPickUp_MouseUp(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnPickUp, "accept.bmp");
        }

        private void BtnUnlock_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnUnlock, "lock_x.bmp");
        }

        private void BtnUnlock_MouseUp(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnUnlock, "lock.bmp");
        }

        private void BtnHandUp_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnHandUp, "hangup_x.bmp");
        }

        private void BtnHandUp_MouseUp(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnHandUp, "hangup.bmp");
        }

        private void BtnWatch_MouseDown(object sender, MouseEventArgs e)
        {
             BtnPicSwitch(BtnWatch, "watchx.bmp");
        }

        private void BtnWatch_MouseUp(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnWatch, "watch.bmp");
        }

        private void BtnStop_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnStop, "stopx.bmp");
        }

        private void BtnStop_MouseUp(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnStop, "stop.bmp");
        }

        private void BtnIPCall_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnIPCall, "ipcallx.bmp");
        }

        private void BtnIPCall_MouseUp(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnIPCall, "ipcall.bmp");
        }

        private void BtnSelectDev_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnSelectDev, "Btn_x.bmp");
        }

        private void BtnSelectDev_MouseUp(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnSelectDev, "seldev.bmp");
        }

        private void BtnCall_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnCall, "Btn_x.bmp");
        }

        private void BtnCall_MouseUp(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnCall, "call.bmp");
        }

        private void BtnClear_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnClear, "Btn_x.bmp");
        }

        private void BtnClear_MouseUp(object sender, MouseEventArgs e)
        {
            BtnPicSwitch(BtnClear, "clear.bmp");
        }

        private void BtnNo_MouseDown(object sender, MouseEventArgs e)
        {
            BtnPicSwitch((Button)sender, "back2.bmp");
        }

        private void BtnNo_MouseUp(object sender, MouseEventArgs e)
        {
            BtnPicSwitch((Button)sender, string.Format("Btn_{0}.bmp", ((Button)sender).Tag));
        }

        private void ComboBoxDev_SelectedIndexChanged(object sender, EventArgs e)
        {
            Address = addr = "";
            textBox.Text = addr;
        }

        private void TrackBarSpeaker_MouseUp(object sender, MouseEventArgs e)
        {
            NativeMethods.Dll_SetVolExport((IntPtr)trackBarSpeaker.Value);
        }

        private void TrackBarMic_MouseUp(object sender, MouseEventArgs e)
        {
            NativeMethods.Dll_SetVolImport((IntPtr)trackBarMic.Value);
        }

        private void TrackBarRing_MouseUp(object sender, MouseEventArgs e)
        {
            NativeMethods.Dll_SetRingVol((IntPtr)trackBarRing.Value);
        }
    }
}
