using ICMServer.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class ModuleAdvertisements : NancyModule
    {
        public ModuleAdvertisements()
            : base("/doorbell")
        {
            Get["/get_adinfo"] = parameters =>
            {
                string responseText = "";
                List<advertisement> advertisements = new List<advertisement>();

                using (var db = new ICMDBContext())
                {
                    try
                    {
                        advertisements = (from a in db.Advertisements
                                          select a).ToList();
                    }
                    catch (Exception) { };
                }

                responseText += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
                responseText += "<advertisement>\n";
                if (advertisements.Count() > 0)
                    responseText += string.Format("<cfg checksum=\"{0}\" count=\"{1}\" btm=\"{2}\" etm=\"{3}\"/>\n",
                        advertisements[0].C_checksum, advertisements.Count(),
                        Config.Instance.AdvertisementBeginTime,
                        Config.Instance.AdvertisementEndTime);
                else
                    responseText += string.Format("<cfg count=\"0\" btm=\"{0}\" etm=\"{1}\"/>\n",
                        Config.Instance.AdvertisementBeginTime,
                        Config.Instance.AdvertisementEndTime);

                string pattern = "<ad no=\"{0}\" path=\"{1}\"/>\n";
                foreach (var advertisement in advertisements)
                {
                    responseText += string.Format(pattern,
                        advertisement.C_no,
                        advertisement.C_path);
                }
                responseText += "</advertisement>\r\n";

                return responseText;
            };
        }
    }
}
