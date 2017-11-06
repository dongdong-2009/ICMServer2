using ICMServer.Models;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class PPHookServiceToken
    {
        public string token { get; set; }
    }

    public class ModuleQRCode : NancyModule
    {
        public ModuleQRCode()
            : base("/doorbell")
        {
            Get["/mobile_qrcode", true] = async (parameters, ct) =>
            {
                //string DeviceId = this.Request.Query["ro"];
                string user = this.Request.Query["user"];
                //Match m = Regex.Match(DeviceId, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}-\w{2}$");
                //if (m.Length == 0)
                //    return HttpStatusCode.BadRequest;   // illigal Device address format 

                //DeviceId = m.Value;
                if (user.Length == 0)
                    return HttpStatusCode.BadRequest;

                using (var db = new ICMDBContext())
                {
                    var sipAccount = (from account in db.Sipaccounts
                                      where account.C_user == user
                                      select account).FirstOrDefault();
                    if (sipAccount == null)
                        return HttpStatusCode.BadRequest;

                    string content = "";
                    switch (Config.Instance.CloudSolution)
                    {
                    case CloudSolution.SIPServer:
                        string sipServerIP = Config.Instance.SIPServerIP;
                        content += sipAccount.C_user + "\n" + sipAccount.C_password + "\n" + sipServerIP;
                        break;

                    case CloudSolution.PPHook:
                        string icmServerIP = Config.Instance.OutboundIP;
                        string result = await HttpClient.GetPPHookServiceToken("8001");
                        PPHookServiceToken pphookServiceToken = JsonConvert.DeserializeObject<PPHookServiceToken>(result);
                        string token = pphookServiceToken.token;
                        string userGroup = sipAccount.C_usergroup;
                        content += sipAccount.C_user + "\n" +
                                   sipAccount.C_password + "\n" + 
                                   icmServerIP + "\n" +
                                   token + "\n" +
                                   userGroup;
                        break;
                    }
                    //string content = "http://www.google.com/";

                    //将要編碼的文字生成出QRCode的图檔
                    System.Drawing.Bitmap bitmap = QRCode.Encode(content);
                    //儲存图片
                    MemoryStream ms = new MemoryStream();
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);   // JPG、GIF、PNG等均可  
                    byte[] imageData = ms.ToArray();
                    //bitmap.Save(@"D:\temp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    var response = new Response();
                    response.ContentType = "image/jpg";
                    response.Contents = s => s.Write(imageData, 0, imageData.Count());
                    return response;
                }
            };
        }
    }
}
