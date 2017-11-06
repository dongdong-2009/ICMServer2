using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public class NtpServer : IDisposable
    {
        enum STATE
        {
            STOPPED,
            STARTED
        }

        STATE m_State = STATE.STOPPED;
        private Process m_NtpProcess;
        private static volatile NtpServer m_Instance;
        private static object m_SyncRoot = new Object();

        private NtpServer() 
        {
            m_NtpProcess = new Process();
        }

        public static NtpServer Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (m_SyncRoot)
                    {
                        if (m_Instance == null)
                            m_Instance = new NtpServer();
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
                    ProcessStartInfo startInfo = new ProcessStartInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\sntp_server.exe");
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    //startInfo.UseShellExecute = false;
                    //DebugLog.TraceMessage("NTP Server - Start+");
                    m_NtpProcess = Process.Start(startInfo);
                    m_State = STATE.STARTED;
                    //DebugLog.TraceMessage("NTP Server - Start-");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        DebugLog.TraceMessage(ex.InnerException.Message);
                    else
                        DebugLog.TraceMessage(ex.Message);
                }
            }
            return m_State == STATE.STARTED;
        }

        public void Stop()
        {
            if (m_State == STATE.STARTED && m_NtpProcess != null)
            {
                try
                {
                    //DebugLog.TraceMessage("NTP Server - Stop+");
                    m_NtpProcess.Kill();
                    m_State = STATE.STOPPED;
                    //DebugLog.TraceMessage("NTP Server - Stop-");
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
