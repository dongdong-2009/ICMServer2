using ICMServer.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class ModuleDeviceTokens: NancyModule
    {
        public ModuleDeviceTokens()
            : base("")
        {
            // UpdateDeviceTokenInfo : 登記 push device token
            // 
            // platform: IOS or ANDROID or BAIDU
            // account: SIP account
            // device_id: 手機 device id
            // token_id: APNs/ GCM / BAIDU token id
            //
            // 回應: 100 表示成功,其他為錯誤代碼
            // 範例:
            // http://syscodevoip.duckdns.org:5050/UpdateDeviceTokenInfo?
            // platform=ios&account=6002&device_id=asdaksjdhaksjdh&token_i
            // d=893453475089237502387509834708750283745087
            Get["/UpdateDeviceTokenInfo"] = parameters =>
            {
                string platform = this.Request.Query["platform"];
                string account = this.Request.Query["account"];
                string deviceID = this.Request.Query["device_id"];
                string tokenID = this.Request.Query["token_id"];

                return UpdateDeviceToken(platform, account, deviceID, tokenID).ToString();
            };
        }

        public static int UpdateDeviceToken(string platform, string account, string deviceID, string tokenID)
        {
            int result = 0;

            using (var db = new ICMDBContext())
            {   // 開啟資料庫
                var sipAccount = GetSipAccount(db, account);   
                if (sipAccount != null)
                {
                    sipAccount.Platform = platform;
                    sipAccount.DeviceID = deviceID;
                    sipAccount.TokenID = tokenID;
                    db.SaveChanges();
                    result = 100;
                }
            }

            return result;
        }

        public static sipaccount GetSipAccount(
            ICMDBContext db, string account)
        {
            var sipAccount = (from a in db.sipaccounts
                              where a.C_user == account
                              select a).FirstOrDefault();
            return sipAccount;
        }
    }
}
