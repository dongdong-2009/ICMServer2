using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public class HeartbeatService : IDisposable
    {
        enum STATE
        {
            STOPPED,
            STARTED
        }

        STATE m_State = STATE.STOPPED;
        private Process m_HeartbeatProcess;
        private static volatile HeartbeatService m_Instance;
        private static object m_SyncRoot = new Object();

        private HeartbeatService() 
        {
            m_HeartbeatProcess = new Process();
        }

        public static HeartbeatService Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (m_SyncRoot)
                    {
                        if (m_Instance == null)
                            m_Instance = new HeartbeatService();
                    }
                }

                return m_Instance;
            }
        }

        public bool Start()
        {
            if (m_State == STATE.STOPPED)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Heartbeat.exe");
#if !DEBUG
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
#endif
                    //startInfo.UseShellExecute = false;
                    //DebugLog.TraceMessage("Heartbeat Service - Start+");
                    m_HeartbeatProcess = Process.Start(startInfo);
                    m_State = STATE.STARTED;
                    //DebugLog.TraceMessage("Heartbeat Service - Start-");
                }
                catch (Exception ex)
                {
                    DebugLog.TraceMessage(ex.InnerException.Message);
                }
            }
            return m_State == STATE.STARTED;
        }

        public void Stop()
        {
            if (m_State == STATE.STARTED && m_HeartbeatProcess != null)
            {
                try
                {
                    //DebugLog.TraceMessage("Heartbeat Service - Stop+");
                    m_HeartbeatProcess.Kill();
                    m_State = STATE.STOPPED;
                    //DebugLog.TraceMessage("Heartbeat Service - Stop-");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        DebugLog.TraceMessage(ex.InnerException.Message);
                    else
                        DebugLog.TraceMessage(ex.Message);
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
