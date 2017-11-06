using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Net
{
    public interface IVideoTalkService
    {
        event EventHandler<EventArgs> ReceivedNewVideoMessageEvent;
        event EventHandler<EventArgs> VideoMessageHasBeenReadEvent;
        event EventHandler<ReceivedIncommingCallEventArgs> ReceivedIncomingCallEvent;
        event EventHandler<EventArgs> AcceptedIncomingCallEvent;
        event EventHandler<EventArgs> ReceivedHangUpEvent;
        event EventHandler<EventArgs> ReceivedOutgoingCallTimeoutEvent;

        bool Start();
        void Stop();
        void SetVideoWindow(System.Windows.Forms.Control control);
    }

    public class ReceivedIncommingCallEventArgs : EventArgs
    {
        public Device ClientDevice;
    }
}
