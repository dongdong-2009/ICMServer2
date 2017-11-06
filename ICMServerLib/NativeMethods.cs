using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICMServer
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void pCallBackPro(ref SIteDataParam d);

    public static class NativeMethods
    {
        /// <summary> 
        /// 初始化dll，并初始化回调函数，回调函数类型:typedef void (CALLBACK * pCallBackPro)(ITE::sIteDataParam &d);
        /// <param name="func"> 回调函数 </param>
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "dll_init")]
        public static extern int Dll_init([MarshalAs(UnmanagedType.FunctionPtr)] pCallBackPro func);

        /// <summary>
        /// 释放dll资源
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "dll_uninit")]
        public static extern void Dll_uninit();

        /// <summary>
        /// 初始化摄像头环境 
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#21", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Initcamera();

        /// <summary>
        /// 呼叫，监视，挂断接口
        /// <param name="nCallType"> 类型为enum VideoTalkOperation </param>
        /// <param name="ip"> 为ip地址 </param>
        /// <param name="address"> 为设备地址 </param>
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "dll_DataOut")]
        public static extern bool Dll_DataOut(int nType, StringBuilder ip, StringBuilder address);

        /// <summary>
        /// 取得通話狀態
        /// </summary>
        /// <returns>VideoTalkStatus</returns>
        [DllImport("Phone.dll", EntryPoint = "dll_CallState")]
        public static extern int Dll_CallState();

        /// <summary>
        /// 设置本机IP、房号、SIP通信端口
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#27")]
        public static extern bool Dll_SetLocalIpAddress(StringBuilder ip, StringBuilder UserName, uint unSipPort);

        /// <summary>  
        /// 获取当前状态 
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#15", CallingConvention = CallingConvention.Cdecl)][return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Dll_GetCurrentModeIsBusy();

        /// <summary>  
        /// 设置当前忙状态 
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#26")]
        public static extern void Dll_SetCurrentModeBusy(bool bOpen);

        /// <summary>
        /// 初始化摄像头环境 
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#21", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Dll_initCamera();

        /// <summary>
        /// 首先必须要初始化设备，不然会调用失败返回-1
        /// 调用成功返回摄像头设备数
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#14")]
        private static extern int Dll_GetAllCaptureDevice([MarshalAs(UnmanagedType.Struct)] ref object pVariant);

        public static int GetCaptureDeviceCount()
        {
            object v = null;
            int nDevice = Dll_GetAllCaptureDevice(ref v);
            return nDevice;
        }

        /// <summary>
        /// 设置当前启动设备的索引id，该id从0开始计算
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#25")]
        public static extern bool Dll_SetCameraDeviceId(int nDeviceid);

        /// <summary>
        /// 视频输出窗口设置 
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#20")]
        public static extern void Dll_Handle(IntPtr hWnd);

        /// <summary> 
        /// 视频输出窗口句柄设置 
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#19")]
        public static extern void Dll_HWND(IntPtr hWnd);

        [DllImport("OleAut32.dll")]
        public static extern void VariantInit([Out][MarshalAs(UnmanagedType.Struct)] out object pVariant);

        /// <summary>
        /// 开启摄像头
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#22", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Dll_OpenCamera();

        /// <summary>
        /// 配置multicast IP，此ip为门口机设备发送 multicast IP.
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#12")]
        public static extern void Dll_ConfigMulticastIP(StringBuilder ip);

        /// <summary>
        /// 关闭摄像头
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#10", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Dll_CloseCamera();

        [DllImport("Phone.dll", EntryPoint = "#24", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Dll_SIPCallStop();


        /// <summary>
        /// 获取喇叭音量
        /// <returns>返回值为喇叭当前音量的大小，取值为[0,100]</returns>
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#17")]
        public static extern int Dll_GetVolExport();

        /// <summary> 
        /// 获取麦克风音量
        /// <returns>返回值为麦克风音量的大小，取值为[0,100]</returns>
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#18")]
        public static extern int Dll_GetVolImport();

        /// <summary>
        /// 获取响铃音量
        /// <returns>返回值为响铃音量的大小，取值为[0,100]</returns>
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#16")]
        public static extern int Dll_GetRingVol();

        /// <summary> 
        /// 设置响铃音量
        /// <param name="v"> 取值为[0,100] </param>
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#29")]
        public static extern int Dll_SetRingVol(IntPtr v);

        /// <summary> 
        /// 设置喇叭音量
        /// <param name="dw"> 取值为[0,100] </param>
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#30")]
        public static extern int Dll_SetVolExport(IntPtr dw);

        /// <summary> 
        /// 设置麦克风音量
        /// <param name="dw"> 取值为[0,100] </param>
        /// </summary>
        [DllImport("Phone.dll", EntryPoint = "#31")]
        public static extern int Dll_SetVolImport(IntPtr dw);


        /* 播放器接口 */
        [DllImport("Phone.dll", EntryPoint = "#23")]
        public static extern void Dll_Player(string file, IntPtr hWnd);

        [DllImport("Phone.dll", EntryPoint = "#11")]
        public static extern void Dll_ClosePlayer();

        [DllImport("Rtsp.dll", EntryPoint = "CreateRtspServerForICM", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunRTSP();

        [DllImport("pkgtool.dll", CallingConvention = CallingConvention.Cdecl)]
#pragma warning disable IDE1006 // 命名样式
        public static extern bool create_package_execute(int argc, string[] argv);
#pragma warning restore IDE1006 // 命名样式
    }

    public enum VideoTalkOperation
    {
        emMEET_INVAL = 0,           // Request unvailable
        emMEET_REQUEST = 1,         // Call request 呼叫
        emMEET_ACCEPT = 2,          // Call accept 接受呼叫
        emMEET_REFUSE = 3,          // Call denied 拒绝呼叫
        emMEET_TIMEOVER = 4,        // Request timeout 请求超时
        emMEET_STOP = 5,            // Handup call 停止呼叫
        emMEET_OPERA = 6,           // Support operate
        emMEET_FAILED = 7,          // Operate failure
        emWATCH_REQUEST = 8,        // Request watch
        emWATCH_BUSY = 9,           // Watch busy
        emWATCH_TIMEOVER = 10,      // Watch timeout
        emWATCH_STOP = 11,          // Stop watch
        emWATCH_ACCEPT = 12,        // Accept watch
        emMEET_LeaveWord = 13,      // SIP Leave video msgs
        emLW_STOP = 14,             // SIP Leave video msgs stop
        emLW_REQUEST = 15,          // Leave video request
        emLW_DOWNLOAD_REQUEST = 16, // Watch leave video request
        emPHONERECORDS_FROM = 17,
        emPHONERECORDS_TO = 18
    }

        
    /// <summary>
    /// 对讲的状态  
    /// </summary>
    public enum VideoTalkStatus {
        emMEET_STATE_FREE,          // 空闲状态
        emMEET_STATE_REQUESTING,    // 正在呼叫
        emMEET_STATE_CALLING,       // 正在通话
    } 
}
