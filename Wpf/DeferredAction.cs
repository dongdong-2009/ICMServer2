using System;
using System.Threading;
using System.Windows;

namespace ICMServer.WPF
{
    /// <summary>
    /// Represents a timer which performs an action on the UI thread when time elapses.  Rescheduling is supported.
    /// </summary>
    public class DeferredAction : IDisposable
    {
        private Timer timer;
        private Timer timeOutTimer;
        private TimeSpan timeOut = TimeSpan.FromMilliseconds(-1);
        private State timeOutTimerState = State.Stopped;

        enum State
        {
            Stopped,
            Started
        }

        /// <summary>
        /// Creates a new DeferredAction.
        /// </summary>
        /// <param name="action">
        /// The action that will be deferred.  It is not performed until after <see cref="Defer"/> is called.
        /// </param>
        public static DeferredAction Create(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            return new DeferredAction(action);
        }

        public static DeferredAction Create(Action action, TimeSpan timeOut)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            if (timeOut == null)
                throw new ArgumentNullException("timeOut");

            return new DeferredAction(action, timeOut);
        }

        private DeferredAction(Action action)
        {
            this.timer = new Timer(new TimerCallback(delegate
            {
                Application.Current.Dispatcher.Invoke(action);
            }));
        }

        private DeferredAction(Action action, TimeSpan timeOut)
        {
            this.timer = new Timer(new TimerCallback(delegate
            {
                try
                {
                    Application.Current.Dispatcher.Invoke(action);
                }
                catch (Exception) { }

                // stop timeOutTimer
                this.timeOutTimer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1));
                timeOutTimerState = State.Stopped;
                //DebugLog.TraceMessage("Stopped");
            }));

            this.timeOut = timeOut;
            this.timeOutTimer = new Timer(new TimerCallback(delegate
            {
                try
                {
                    Application.Current.Dispatcher.Invoke(action);
                }
                catch (Exception) { }

                // stop timeOutTimer
                this.timeOutTimer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1));
                timeOutTimerState = State.Stopped;
                //DebugLog.TraceMessage("Stopped");
            }));
        }

        /// <summary>
        /// Defers performing the action until after time elapses.  Repeated calls will reschedule the action
        /// if it has not already been performed.
        /// </summary>
        /// <param name="delay">
        /// The amount of time to wait before performing the action.
        /// </param>
        public void Defer(TimeSpan delay)
        {
            // Fire action when time elapses (with no subsequent calls).
            this.timer.Change(delay, TimeSpan.FromMilliseconds(-1));
            switch (timeOutTimerState)
            {
                case State.Stopped:
                    if (timeOut != TimeSpan.FromMilliseconds(-1) && this.timeOutTimer != null)
                    {
                        this.timeOutTimer.Change(timeOut, TimeSpan.FromMilliseconds(-1));
                        //DebugLog.TraceMessage("Started");
                        timeOutTimerState = State.Started;
                    }
                    break;

                case State.Started:
                    break;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.timer != null)
            {
                this.timer.Dispose();
                this.timer = null;
            }
            if (this.timeOutTimer != null)
            {
                this.timeOutTimer.Dispose();
                this.timeOutTimer = null;
            }
        }

        #endregion
    }
}
