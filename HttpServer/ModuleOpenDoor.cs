using ICMServer.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class ModuleOpenDoor : NancyModule
    {
        public ModuleOpenDoor()
            : base("/doorbell")
        {
            Get["/check_ic"] = parameters =>
            {
                string DeviceId = this.Request.Query["ro"];
                string cardId = this.Request.Query["ic"];
                //Match m = Regex.Match(DeviceId, @"(?<a1>^\w{2})-(?<a2>\w{2})-(?<a3>\w{2})-(?<a4>\w{2})-(?<a5>\w{2})");
                Match m = Regex.Match(DeviceId, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}-\w{2}$");
                if (m.Length == 0)
                    return HttpStatusCode.BadRequest;   // illigal Device address format 

                DeviceId = m.Value;
                do 
                {
                    using (var db = new ICMDBContext())
                    {
                        var cardMappings = from c in db.Icmaps
                                           where c.C_entrancedoor == DeviceId
                                              && c.C_icno == cardId
                                           select c;
                        if (cardMappings.Count() == 0)
                            break;

                        var card = (from c in db.Iccards
                                    where c.C_icno == cardId
                                    select c).FirstOrDefault();
                        if (card != null && card.C_available == 1)
                        {
                            if (card.C_uptime.HasValue && card.C_downtime.HasValue)
                            {
                                DateTime begin = card.C_uptime.Value;
                                DateTime end = card.C_downtime.Value;
                                DateTime now = DateTime.Now;

                                if (begin <= now && now <= end)
                                    return "OK";
                            }
                            else if (card.C_uptime.HasValue && !card.C_downtime.HasValue)
                            {
                                DateTime begin = card.C_uptime.Value;
                                DateTime now = DateTime.Now;

                                if (begin <= now)
                                    return "OK";
                            }
                            else if (!card.C_uptime.HasValue && card.C_downtime.HasValue)
                            {
                                DateTime end = card.C_downtime.Value;
                                DateTime now = DateTime.Now;

                                if (now <= end)
                                    return "OK";
                            }
                            else
                                return "OK";
                        }
                    }
                } while (false);    
                return "FAIL";
            };

            Get["/check_password"] = parameters =>
            {
                string DeviceId = this.Request.Query["ro"];
                string password = this.Request.Query["password"];
                Match m = Regex.Match(DeviceId, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}-\w{2}$");
                if (m.Length == 0)
                    return HttpStatusCode.BadRequest;   // illigal Device address format 

                DeviceId = m.Value;
                using (var db = new ICMDBContext())
                {
                    var entrance = (from Device in db.Devices
                                   where Device.ip == this.Request.UserHostAddress
                                   select Device).FirstOrDefault();
                    if (entrance != null)
                    {
                        int pos = entrance.roomid.IndexOf("-00");
                        if (pos >= 0 && (entrance.roomid.Substring(0, pos) == DeviceId.Substring(0, pos)))
                        {
                            var indoor = from Device in db.Doorbellpasswords
                                         where Device.C_roomid == DeviceId
                                            && Device.C_password == password
                                         select Device;
                            if (indoor != null)
                                return "OK";
                        }
                    }
                }

                return "FAIL";
            };
        }
    }
}
