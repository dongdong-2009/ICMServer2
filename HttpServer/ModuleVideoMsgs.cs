using ICMServer.Models;
using Nancy;
using Nancy.Routing.Trie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace ICMServer.Net
{
    public class ModuleVideoMsgs : NancyModule
    {
        public ModuleVideoMsgs() 
            : base("/doorbell")
        {
            Get["/message_video_list"] = parameters =>
            {
                string responseText = "";
                string DeviceId = this.Request.Query["ro"];
                Match m = Regex.Match(DeviceId, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}");
                if (m.Length == 0)
                    return HttpStatusCode.BadRequest;   // illigal Device address format 

                DeviceId = m.Value;
                using (var db = new ICMDBContext())
                {
                    var videoMsgs = from leaveword in db.Leavewords
                                    where leaveword.dst_addr.StartsWith(DeviceId)
                                    //   && leaveword.readflag == 0
                                    select leaveword;
                    responseText += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
                    responseText += "<MessageVideoList>\n";

                    string pattern = @"<message>" + "\n"
                                   + @"	<id>{0}</id>" + "\n"
                                   + @"	<topic>{1}</topic>" + "\n"
                                   + @"	<time>{2}</time>" + "\n"
                                   + @"	<read>{3}</read>" + "\n"
                                   + @"</message>" + "\n";
                    foreach (var videoMsg in videoMsgs)
                    {
                        if (File.Exists(videoMsg.filenames + ".sdp"))
                        {
                            responseText += string.Format(pattern,
                                videoMsg.id,
                                videoMsg.src_addr,
                                videoMsg.time,
                                videoMsg.readflag != 0 ? "true" : "false");
                        }
                        else
                        {
                            db.Leavewords.Remove(videoMsg);
                        }
                    }
                    db.SaveChanges();
                    responseText += "</MessageVideoList>\r\n";
                }
                return responseText;
            };

            Get["/message_video_delete"] = parameters =>
            {
                string DeviceId = this.Request.Query["ro"];
                int msgId = int.Parse(this.Request.Query["id"]);
                Match m = Regex.Match(DeviceId, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}");
                if (m.Length == 0)
                    return HttpStatusCode.BadRequest;   // illigal Device address format 

                DeviceId = m.Value;
                using (var db = new ICMDBContext())
                {
                    var videoMsg = (from leaveword in db.Leavewords
                                    where leaveword.dst_addr.StartsWith(DeviceId)
                                       && leaveword.id == msgId
                                    select leaveword).FirstOrDefault();
                    if (videoMsg != null)
                    {
                        db.Leavewords.Remove(videoMsg);
                        db.SaveChangesAsync();
                    }
                }
                return "OK";
            };
        }
    }
}
