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
    public class ModuleUpgradeInfo : NancyModule
    {
        const string PATH_CARDLIST = @"data/cardlist";
        const string PATH_ADDRESSBOOK = @"data/addressbook";

        public ModuleUpgradeInfo()
            : base("/doorbell")
        {
            Get["/get_info"] = parameters =>
            {
                string responseText = "";
                string DeviceId = this.Request.Query["ro"];
                Match m = Regex.Match(DeviceId, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}-\w{2}$");
                if (m.Length == 0)
                    return HttpStatusCode.BadRequest;   // illigal Device address format 

                DeviceId = m.Value;
                using (var db = new ICMDBContext())
                {
                    string path;
                    var Device = (from d in db.Devices
                                  where d.roomid == DeviceId
                                  select d).FirstOrDefault();
                    if (Device == null)
                        return HttpStatusCode.BadRequest;

                    responseText += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
                    responseText += "<ServerInfo>\n";

                    string pattern = "<{0} version=\"{1}\">"
                                   + @"ftp://{2}/{3}"
                                   + @"</{0}>" + "\n";

                    #region firmware
                    var upgradeFile = (from u in db.Upgrades
                                       where u.Device_type == Device.type
                                          && u.is_default == 1
                                       select u).FirstOrDefault();
                    if (upgradeFile != null)
                    {
                        responseText += string.Format(pattern,
                            "firmware",
                            upgradeFile.version,
                            this.Request.Url.HostName,
                            upgradeFile.filepath);
                    }
                    #endregion

                    #region addressbook
                    // no error check?
                    path = PATH_ADDRESSBOOK + "/"
                         + DeviceId + "/"
                         + "output/ADDRESS.PKG";
                    responseText += string.Format(pattern,
                        "addressbook",
                        Device.laVer,
                        this.Request.Url.HostName,
                        path);
                    #endregion

                    #region cardlist
                    // no error check?
                    path = PATH_CARDLIST + "/"
                         + DeviceId + "/"
                         + "output/CARD.PKG";
                    responseText += string.Format(pattern,
                        "cardlist",
                        Device.lcVer,
                        this.Request.Url.HostName,
                        path);
                    #endregion

                    responseText += "</ServerInfo>\r\n";
                }
                return responseText;
            };
        }
    }
}
