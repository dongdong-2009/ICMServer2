using mooftpserv;
using System;
using System.IO;
using System.Net;

namespace ICMServer.Net
{
    /// <summary>
    /// Default log handler.
    ///
    /// Writes to stdout by default. Writes messages for every event
    /// in Verbose mode, otherwise only new and closed control connections.
    /// </summary>
    public class LogHandler : ILogHandler
    {
        private FtpServer m_Server;
        private IPEndPoint m_Peer;

        public LogHandler(FtpServer server)
        {
            this.m_Server = server;
        }

        private LogHandler(IPEndPoint peer, FtpServer server)
        {
            this.m_Peer = peer;
            this.m_Server = server;
        }

        public ILogHandler Clone(IPEndPoint peer)
        {
            return new LogHandler(peer, m_Server);
        }

        private void Write(string format, params object[] args)
        {
            string now = DateTime.Now.ToString("HH:mm:ss.fff");
            //DebugLog.TraceMessage(String.Format("{0}, {1}: {2}", now, m_Peer, String.Format(format, args)));
        }

        public void NewControlConnection()
        {
            m_Server.RaiseNewControlConnectionEvent(m_Peer);
            Write("new control connection: {0}", m_Peer);
        }

        public void ClosedControlConnection()
        {
            m_Server.RaiseClosedControlConnectionEvent(m_Peer);
            Write("closed control connection: {0}", m_Peer);
        }

        public void ReceivedCommand(string verb, string arguments)
        {
            string argtext = (arguments == null || arguments == "" ? "" : ' ' + arguments);
            Write("received command: {0}{1}", verb, argtext);
        }

        public void SentResponse(uint code, string description)
        {
            Write("sent response: {0} {1}", code, description);
        }

        public void NewDataConnection(IPEndPoint remote, IPEndPoint local, bool passive)
        {
            m_Server.RaiseNewDataConnectionEvent(remote, local, passive);
            Write("new data connection: {0} <-> {1} ({2})", remote, local, (passive ? "passive" : "active"));
        }

        public void ClosedDataConnection(IPEndPoint remote, IPEndPoint local, bool passive)
        {
            m_Server.RaiseClosedDataConnectionEvent(remote, local, passive);
            Write("closed data connection: {0} <-> {1} ({2})", remote, local, (passive ? "passive" : "active"));
        }

        public void SentData(IPEndPoint remote, IPEndPoint local, string fileName, long fileSize, long sentBytes)
        {
            m_Server.RaiseSentDataEvent(remote, local, fileName, fileSize, sentBytes);
            Write("sent data: {0} <-> {1} ({2}: {3}/{4})", remote, local, fileName, sentBytes, fileSize);
        }
    }
}

