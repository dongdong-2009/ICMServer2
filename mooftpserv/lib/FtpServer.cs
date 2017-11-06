using mooftpserv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class FtpServer
    {
        enum STATE
        {
            STOPPED,
            STARTED
        }

        private static volatile FtpServer _instance;
        private static object _syncRoot = new Object();
        private STATE _state = STATE.STOPPED;
        private object _lock = new object();
        private mooftpserv.Server _server;

        public event EventHandler<FtpControlConnectionEventArgs> NewControlConnection;
        public event EventHandler<FtpControlConnectionEventArgs> ClosedControlConnection;
        public event EventHandler<FtpDataConnectionEventArgs> NewDataConnection;
        public event EventHandler<FtpDataConnectionEventArgs> ClosedDataConnection;
        public event EventHandler<FtpSentDataEventArgs> SentData;

        public static FtpServer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new FtpServer();
                    }
                }

                return _instance;
            }
        }

        public bool Start(string rootDirectory, int port = 21)
        {
            lock (_lock)
            {
                if (_state == STATE.STOPPED)
                {
                    _server = new mooftpserv.Server();
                    _server.LogHandler = new LogHandler(this);
                    _server.FileSystemHandler = new FileSystemHandler(rootDirectory);
                    try
                    {
                        Task.Run(() => { _server.Run(); });
                        _state = STATE.STARTED;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                    }
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
                    try
                    {
                        _server.Stop();
                    }
                    catch (Exception) { }

                    _state = STATE.STOPPED;
                }
            }
        }

        internal void RaiseNewControlConnectionEvent(IPEndPoint remote)
        {
            if (NewControlConnection != null)
            {
                NewControlConnection(this, new FtpControlConnectionEventArgs(remote));
            }
        }

        internal void RaiseClosedControlConnectionEvent(IPEndPoint remote)
        {
            if (ClosedControlConnection != null)
            {
                ClosedControlConnection(this, new FtpControlConnectionEventArgs(remote));
            }
        }

        internal void RaiseNewDataConnectionEvent(IPEndPoint remote, IPEndPoint local, bool passive)
        {
            if (NewDataConnection != null)
            {
                NewDataConnection(this, new FtpDataConnectionEventArgs(remote, local, passive));
            }
        }

        internal void RaiseClosedDataConnectionEvent(IPEndPoint remote, IPEndPoint local, bool passive)
        {
            if (ClosedDataConnection != null)
            {
                ClosedDataConnection(this, new FtpDataConnectionEventArgs(remote, local, passive));
            }
        }

        internal void RaiseSentDataEvent(
            IPEndPoint remote, 
            IPEndPoint local, 
            string fileName, 
            long fileSize, 
            long sentBytes)
        {
            if (SentData != null)
            {
                SentData(this, new FtpSentDataEventArgs(remote, local, fileName, fileSize, sentBytes));
            }
        }

        private FtpServer()
        {
        }
    }

    public class FtpControlConnectionEventArgs : EventArgs
    {
        public IPEndPoint Remote { get; private set; }

        public FtpControlConnectionEventArgs(IPEndPoint remote)
        {
            this.Remote = remote;
        }
    }

    public class FtpDataConnectionEventArgs : EventArgs
    {
        public IPEndPoint Remote { get; private set; }
        public IPEndPoint Local { get; private set; }
        public bool Passive { get; private set; }

        public FtpDataConnectionEventArgs(IPEndPoint remote, IPEndPoint local, bool passive)
        {
            this.Remote = remote;
            this.Local = local;
            this.Passive = passive;
        }
    }

    public class FtpSentDataEventArgs : EventArgs
    {
        public IPEndPoint Remote { get; private set; }
        public IPEndPoint Local { get; private set; }
        public string FileName { get; private set; }
        public long FileSize { get; private set; }
        public long SentBytes { get; private set; }

        public FtpSentDataEventArgs(
            IPEndPoint remote, 
            IPEndPoint local, 
            string fileName, 
            long fileSize, 
            long sentBytes) 
        {
            this.Remote = remote;
            this.Local = local;
            this.FileName = fileName;
            this.FileSize = fileSize;
            this.SentBytes = sentBytes;
        }
    }
}
