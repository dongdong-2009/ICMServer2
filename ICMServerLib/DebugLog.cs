using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ICMServer
{
    public class DebugLog
    {
        static public void TraceMessage(
               string message,
               [CallerMemberName] string memberName = "",
               [CallerFilePath] string sourceFilePath = "",
               [CallerLineNumber] int sourceLineNumber = 0)
        {
            //Trace.WriteLine("[icm]message: " + message);
            //Trace.WriteLine("[icm]member name: " + memberName);
            //Trace.WriteLine("[icm]source file path: " + sourceFilePath);
            //Trace.WriteLine("[icm]source line number: " + sourceLineNumber);
            Trace.WriteLine(string.Format("{0}({1}): T{4}-{2} {3}", sourceFilePath, sourceLineNumber, memberName, message, Thread.CurrentThread.ManagedThreadId));
        }
    }
}
