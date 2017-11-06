using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    /// <summary>
    /// A Fake AppExtraService.
    /// It's used in PPHook Cloud Solution. The real AppExtraService is running on SIP Server,
    /// but in PPHook Cloud Solution, there is no SIP Server. So we make a fake AppExtraService
    /// to make original function work.
    /// </summary>
    public class AppExtraService
    {
        enum STATE
        {
            STOPPED,
            STARTED
        }

        private static volatile AppExtraService _instance;
        private static object _syncRoot = new Object();
        private STATE _state = STATE.STOPPED;
        private object _lock = new object();
        private NancyHost _host;

        public static AppExtraService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new AppExtraService();
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
                    _host = new NancyHost(uri);
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

        private AppExtraService()
        {
        }
    }
}
