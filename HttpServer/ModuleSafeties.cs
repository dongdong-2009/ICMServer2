using ICMServer.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class ModuleSafeties : NancyModule
    {
        public ModuleSafeties()
            : base("/doorbell")
        {
            // 转發安防設定
            Get["/guard", true] = async (parameters, ct) =>
            {
                string DeviceId = this.Request.Query["ro"];
                string mode = this.Request.Query["mode"];
                Match m = Regex.Match(DeviceId, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}-\w{2}$");
                if (m.Length == 0)
                    return HttpStatusCode.BadRequest;   // illigal Device address format 

                DeviceId = m.Value;
                using (var db = new ICMDBContext())
                {
                    var Device = (from d in db.Devices
                                  where d.roomid == DeviceId
                                  select d).FirstOrDefault();
                    if (Device != null)
                    {
                        string uri = string.Format("http://{0}/guard?ro={1}&mode={2}", Device.ip, DeviceId, mode);
                        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                        try
                        {
                            var response = await client.GetStringAsync(uri);
                            return response;
                        }
                        catch (Exception) { }
                    }
                }
                return "FAIL";
            };
        }    
    }
}
