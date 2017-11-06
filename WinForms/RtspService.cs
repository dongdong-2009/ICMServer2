using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICMServer
{
    class RtspService
    {
        public void Run()
        {
            var th1 = new Thread(TASK_RTSP_SERVICE);
            th1.IsBackground = true;
            th1.Start();
        }

        private void TASK_RTSP_SERVICE(object obj)
        {
            //bool a = NativeMethods.RunRTSP();
            NativeMethods.RunRTSP();
        }
    }
}
