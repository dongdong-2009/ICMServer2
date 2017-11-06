using ICMServer.Models;
using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class HttpServer 
    {
        enum STATE
        {
            STOPPED,
            STARTED
        }

        private static volatile HttpServer _instance;
        private static object _syncRoot = new Object();
        private STATE _state = STATE.STOPPED;
        private object _lock = new object();
        private NancyHost _host;

        public event EventHandler<HttpReceivedAlarmEventArgs> ReceivedAlarmEvent;
        public event EventHandler<EventArgs> ReceivedCommonEvent;
        public event EventHandler<EventArgs> ReceivedCallOutEvent;
        public event EventHandler<EventArgs> ReceivedOpenDoorEvent;
        public event EventHandler<EventArgs> ReceivedReadAnnouncementEvent;

        public static HttpServer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new HttpServer();
                    }
                }

                return _instance;
            }
        }

        public bool Start(Uri uri)
        {
            lock (_lock)
            {
                if (_state == STATE.STOPPED)
                {
                    // close chunked encoding
                    var hostConfiguration = new HostConfiguration();
                    hostConfiguration.AllowChunkedEncoding = false;

                    //m_Host = new NancyHost(uri);
                    _host = new NancyHost(hostConfiguration, uri);
                    _host.Start();

                    _state = STATE.STARTED;
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
                    _host.Stop();

                    _state = STATE.STOPPED;
                }
            }
        }

        internal void RaiseReceivedAlarmEvent(List<eventwarn> alarms)
        {
            if (ReceivedAlarmEvent != null)
            {
                ReceivedAlarmEvent(this, new HttpReceivedAlarmEventArgs(alarms));
            }
        }

        internal void RaiseReceivedCommonEvent()
        {
            if (ReceivedCommonEvent != null)
            {
                ReceivedCommonEvent(this, new EventArgs());
            }
        }

        internal void RaiseReceivedCallOutEvent()
        {
            if (ReceivedCallOutEvent != null)
            {
                ReceivedCallOutEvent(this, new EventArgs());
            }
        }

        internal void RaiseReceivedOpenDoorEvent()
        {
            if (ReceivedOpenDoorEvent != null)
            {
                ReceivedOpenDoorEvent(this, new EventArgs());
            }
        }
        internal void RaiseReceivedGetMsgsEvent()
        {
            if (ReceivedReadAnnouncementEvent != null)
            {
                ReceivedReadAnnouncementEvent(this, new EventArgs());
            }
        }

        private HttpServer()
        {
        }
    }

    public class HttpReceivedAlarmEventArgs : EventArgs
    {
        public List<eventwarn> Alarms { get; private set; }

        public HttpReceivedAlarmEventArgs(List<eventwarn> alarms)
        {
            Alarms = alarms;
        }
    }
}
