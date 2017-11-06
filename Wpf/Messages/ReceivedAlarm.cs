using GalaSoft.MvvmLight.Messaging;
using ICMServer.Models;
using System.Collections.Generic;

namespace ICMServer.WPF.Messages
{
    class ReceivedAlarm : MessageBase
    {
        public List<eventwarn> Alarms { get; private set; }

        public ReceivedAlarm(List<eventwarn> alarms)
        {
            Alarms = alarms;
        }
    }
}
