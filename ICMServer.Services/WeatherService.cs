using ICMServer.Net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICMServer.Services.Net
{
    public class WeatherService
    {
        enum STATE
        {
            STOPPED,
            STARTED
        }

        private static volatile WeatherService _instance;
        private static object _syncRoot = new Object();
        private STATE _state = STATE.STOPPED;
        private object _lock = new object();

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private ConcurrentBag<Task> _tasks = new ConcurrentBag<Task>();
        private string _data = "";

        private WeatherService()
        {
        }

        private void MainTask(CancellationToken cancelToken)
        {
            try
            {
                while (true)
                {
                    cancelToken.ThrowIfCancellationRequested();

                    try
                    {
                        string data = HttpClient.GetWeatherDataAsync(Config.Instance.WeatherOfCity, cancelToken).Result;
                        if (!string.IsNullOrWhiteSpace(data))
                        {
                            lock (_syncRoot)
                            {
                                _data = data;
                            }
                        }
                    }
                    catch (Exception) { }

                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
            }
            catch (Exception) { }
        }

        public string GetWeatherXmlData()
        {
            lock (_syncRoot)
            { 
                return _data;
            }
        }

        public static WeatherService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new WeatherService();
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
                        _state = STATE.STARTED;
                        _cancellationTokenSource = new CancellationTokenSource();
                        _tasks.Add(Task.Run(() => MainTask(_cancellationTokenSource.Token)));
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
                    try
                    {
                        _cancellationTokenSource.Cancel();
                        Task.WaitAll(_tasks.ToArray());
                    }
                    catch (Exception) { }
                }
            }
        }
    }
}
