using ICMServer.Models;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer
{
    public class ModuleSipAccounts : NancyModule
    {
        //1.1 列出特定group下所有sip accounts
        //GET /api/sipaccounts/groups/{groupid}
        //Sample Request:
        //GET /api/sipaccounts/groups/6000
        //Content-Type: application/json; charset=utf-8
        //________________________________________
        //Status: 200 OK
        //Content-Type: application/json; charset=utf-8
        //[
        //  {
        //    "id": "6001",  // sip account
        //    "platform": "ios", // ios, baidu, android…一律小寫
        //    "Device_id": "asdqweqweqweq"
        //    "token_id": "789198498451948191"
        //  },
        //  { 
        //    "id": "6002",
        //    "platform": "baidu",
        //    "Device_id": “weqwe”
        //    "token_id": “8084089048409849008”
        //  }
        //]
        public ModuleSipAccounts()
            : base("/api/sipaccounts")
        {
            Get["/groups/{groupid}"] = parameters =>
            {
                string responseText = "";
                using (var db = new ICMDBContext())
                {
                    string groupID = parameters.groupid;
                    var sipAccounts = (from a in db.Sipaccounts
                                       where a.C_usergroup == groupID
                                       select new
                                       {
                                           id = a.C_user,
                                           platform = a.Platform,
                                           Device_id = a.DeviceID,
                                           token_id = a.TokenID
                                       }).ToList();
                    responseText = JsonConvert.SerializeObject(sipAccounts);
                }
                return responseText;
            };

            Get["/ip/{sipaccount}"] = parameters =>
            {
                string responseText = "";
                using (var db = new ICMDBContext())
                {
                    string sipAccount = parameters.sipaccount;
                    string DeviceAddress = SipAccount2DeviceAddress(sipAccount);
                    var Device = (from d in db.Devices
                                  where d.roomid == DeviceAddress
                                  select d).FirstOrDefault();
                    string ipStr = "";
                    if (Device != null)
                    {
                        ipStr = Device.ip;
                    }
                    responseText = JsonConvert.SerializeObject(new {ip = ipStr});
                }
                return responseText;
            };
        }

        public string SipAccount2DeviceAddress(string sipAccount)
        {
            string s = "";
            if(sipAccount.Length == 12)
                s = string.Format("{0}-{1}-{2}-{3}-{4}-{5}",
                    sipAccount.Substring(0, 2), sipAccount.Substring(2, 2),
                    sipAccount.Substring(4, 2), sipAccount.Substring(6, 2),
                    sipAccount.Substring(8, 2), sipAccount.Substring(10, 2));

            return s;
        }
    }
}
