using ICMServer.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class ModulePushServer : NancyModule
    {
        public ModulePushServer()
            : base("/")
        {
            Get["/register_udid.php", true] = async (parameters, ct) =>
            {
                string udid = this.Request.Query["udid"];
                string responseText = "";

                using (var db = new ICMDBContext())
                {
                    var sipAccount = (from account in db.Sipaccounts
                                      where account.C_randomcode == udid
                                         && account.C_registerstatus == 0
                                      select account).FirstOrDefault();
                    if (sipAccount != null)
                    {
                        responseText = sipAccount.C_user + "\n" + sipAccount.C_password;
                        sipAccount.C_registerstatus = 1;
                        db.SaveChanges();

                        var Device = (from d in db.Devices
                                      where d.roomid == sipAccount.C_room
                                      select d).FirstOrDefault();
                        if (Device != null)
                        {
                            string uri = string.Format("http://{0}/doorbell/register_success?ro={1}&account={2}", Device.ip, Device.roomid, sipAccount.C_user);
                            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                            try
                            {
                                var response = await client.GetStringAsync(uri);
                            }
                            catch (Exception) { }
                        }
                    }
                }
                return responseText;
            };

            Get["/open_door.php", true] = async (parameters, ct) =>
            {
                string toAccount = this.Request.Query["to_account"];
                string responseText = "open door fail,error = 1";

                using (var db = new ICMDBContext())
                {
                    var sipAccount = (from account in db.Sipaccounts
                                      where account.C_user == toAccount
                                      select account).FirstOrDefault();
                    if (sipAccount != null)
                    {
                        var Device = (from d in db.Devices
                                      where d.roomid == sipAccount.C_room
                                      select d).FirstOrDefault();
                        if (Device != null)
                        {
                            string uri = string.Format("http://{0}/open_door?ro=00-00-00-00-00-00", Device.ip);
                            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                            try
                            {
                                var response = await client.GetStringAsync(uri);
                                return "open door success";
                            }
                            catch (Exception) { }
                        }
                    }
                    else
                        responseText = "open door fail,error = 2";
                }

                return responseText;
            };
        }
    }
}
