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
    public class ModuleAnnouncements : NancyModule
    {
        public ModuleAnnouncements()
            : base("/")
        {
            Get["/doorbell/message_text_list"] = parameters =>
            {
                string responseText = "";
                string DeviceId = this.Request.Query["ro"];
                //Match m = Regex.Match(DeviceId, @"(?<a1>^\w{2})-(?<a2>\w{2})-(?<a3>\w{2})-(?<a4>\w{2})-(?<a5>\w{2})");
                Match m = Regex.Match(DeviceId, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}");
                if (m.Length == 0)
                    return HttpStatusCode.BadRequest;   // illigal Device address format 

                DeviceId = m.Value;
                using (var db = new ICMDBContext())
                {
                    var announcements = from publishinfo in db.Publishinfoes
                                        where publishinfo.dstaddr.StartsWith(DeviceId)
                                        select publishinfo;
                    responseText += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
                    responseText += "<MessageTextList>\n";

                    string pattern = @"<message>" + "\n"
                                   + @"	<id>{0}</id>" + "\n"
                                   + @"	<topic>{1}</topic>" + "\n"
                                   + @"	<time>{2}</time>" + "\n"
                                   + @"	<image>{3}</image>" + "\n"
                                   + @"	<read>{4}</read>" + "\n"
                                   + @"</message>" + "\n";
                    foreach (var announcement in announcements)
                    {
                        string imagePath = string.Format("http://{0}/{1}?type=publish&amp;id={2}", 
                            this.Request.Url.HostName,
                            announcement.filepath.Replace(@"\", "/"),
                            announcement.id);
                        responseText += string.Format(pattern,
                            announcement.id,
                            announcement.title,
                            ((DateTime)announcement.time).ToString("yyyy-MM-dd HH:mm:ss"),
                            imagePath,
                            announcement.isread != 0 ? "true" : "false");
                    }
                    responseText += "</MessageTextList>\r\n";
                }
                return responseText;
            };

            Get["/doorbell/message_text_delete"] = parameters =>
            {
                string DeviceId = this.Request.Query["ro"];
                int msgId = int.Parse(this.Request.Query["id"]);
                Match m = Regex.Match(DeviceId, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}");
                if (m.Length == 0)
                    return HttpStatusCode.BadRequest;   // illigal Device address format 

                DeviceId = m.Value;
                using (var db = new ICMDBContext())
                {
                    var announcement = (from announce in db.Publishinfoes
                                        where announce.dstaddr.StartsWith(DeviceId)
                                           && announce.id == msgId
                                       select announce).FirstOrDefault();
                    if (announcement != null)
                    {
                        db.Publishinfoes.Remove(announcement);
                        db.SaveChangesAsync();
                    }
                }

                return "OK";
            };

            Get["/data/publish_informations/{imgFileName}"] = parameters =>
            {
                string imgFilePath = Path.GetPublishInfoFolderPath() + @"\" + parameters.imgFileName;
                var response = new Response();
                try
                {
                    int id = int.Parse(this.Request.Query["id"]);
                    using (var db = new ICMDBContext())
                    {
                        var announcement = (from announce in db.Publishinfoes
                                            where announce.id == id
                                            select announce).FirstOrDefault();
                        if (announcement == null)
                            return HttpStatusCode.NotFound;
                        announcement.isread = 1;
                        db.SaveChanges();
                        // Refresh Msgs has read
                        HttpServer.Instance.RaiseReceivedGetMsgsEvent();

                        //get unread message count
                        var count = (from announce in db.Publishinfoes
                                     where announce.dstaddr == announcement.dstaddr
                                        && announce.isread == 0
                                     select announce).Count();
                        Task.Factory.StartNew(async () =>
                        {
                            await ICMServer.Net.HttpClient.SendNewTextMsgCount(this.Request.UserHostAddress, count);
                        }, TaskCreationOptions.LongRunning);
                    }

                    byte[] imageData = System.IO.File.ReadAllBytes(imgFilePath);
                    response.ContentType = "image/jpg";
                    response.Contents = s => s.Write(imageData, 0, imageData.Count());
                }
                catch (Exception)
                {
                    return HttpStatusCode.NotFound;
                }
                return response;
            };
        }
    }
}
