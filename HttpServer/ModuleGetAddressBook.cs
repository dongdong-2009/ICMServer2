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
    public class ModuleGetAddressBook : NancyModule
    {
        public ModuleGetAddressBook()
            : base("/")
        {
            // 待修正
            //设备列表 Address book 的資訊
            //GET /GetAddressBookInfo
            //Sample Request:
            //GET /GetAddressBookInfo
            //Content-Type: application/json; charset=utf-8
            //________________________________________
            //Status: 200 OK
            //Content-Type: application/json; charset=utf-8
            //{ "vesion":"1","submask":"255.255.255.0","gateway":"192.168.1.1"}
            //
            Get["/GetAddressBookInfo"] = parameters =>
            {
                string responseText = "";
                responseText = JsonConvert.SerializeObject(
                    new { vesion = "1", submask = "255.255.255.0", gateway = "192.168.1.1" });
                return responseText;
            };

            // 待修正
            //列出设备列表 Address book
            //GET /GetAddressBook
            //Sample Request:
            //GET /GetAddressBook
            //Content-Type: application/json; charset=utf-8
            //________________________________________
            //Status: 200 OK
            //Content-Type: application/json; charset=utf-8
            //[
            //  {
            //    "ty": "4",
            //    "user_id": "",
            //    "mc": "",
            //    "ip": "127.0.0.1:5061",
            //    "alias": "outdoor",
            //    "user_pwd": "",
            //    "ro": "01-01-10-01-02-06",
            //    "group": ""
            //  },
            //  {
            //    "ty": "5",
            //    "user_id": "",
            //    "mc": "",
            //    "ip": "127.0.0.1:5061",
            //    "alias": "indoor",
            //    "user_pwd": "",
            //    "ro": "01-01-01-01-02-01",
            //    "group": ""
            //  }
            //]
            Get["/GetAddressBook"] = parameters =>
            {
                string responseText = "";
                using (var db = new ICMDBContext())
                {
                    var Devices = (from a in db.Devices
                                   select new
                                   {
                                       ty = a.type.ToString(),
                                       user_id = "",
                                       mc = a.mac ?? "",
                                       ip = "127.0.0.1:5061", // for PPHookService User Phone 
                                       alias = a.Alias ?? "",
                                       user_pwd = "",
                                       ro = a.roomid ?? "",
                                       @group = a.@group ?? ""
                                   }).ToList();
                    responseText = JsonConvert.SerializeObject(Devices);
                }
                return responseText;
            };
        }
    }
}
