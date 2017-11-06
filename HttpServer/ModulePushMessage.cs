using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class ModulePushMessage : NancyModule
    {
        public ModulePushMessage()
            : base("/doorbell")
        {
            Get["/pushmessage", true] = async (parameters, ct) =>
            {
                string fromAccount = this.Request.Query["from_account"];
                string toAccount = this.Request.Query["to_account"];
                string sipServerIP = Config.Instance.SIPServerIP;
                int sipServerPort = Config.Instance.SIPServerPort;
                string uri = string.Format("http://{0}:{1}/PushSipAccount?from_account={2}&to_account={3}", 
                    sipServerIP, sipServerPort, fromAccount, toAccount);
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                try
                {
                    var response = await client.GetStringAsync(uri);
                    return HttpStatusCode.OK;
                }
                catch (Exception) { }
                return HttpStatusCode.Continue;
            };
        }
    }
}
