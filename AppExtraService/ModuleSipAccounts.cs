using ICMServer.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class ModuleSipAccounts: NancyModule
    {
        public ModuleSipAccounts()
            : base("")
        {
            // QuerySipAccountOnline : 查詢 SIP account 上線狀況
            // 
            // account: SIP account
            // 
            // 回應: 100 : online , 200 : offline ,其他為錯誤代碼
            // 範例:
            // http://syscodevoip.duckdns.org:5050/QuerySipAccountOnline?account=6002
            Get["/QuerySipAccountOnline"] = parameters =>
            {
                string responseText = "100";    // 100: success
                string account = this.Request.Query["account"];

                return responseText;
            };

            // PushSipAccount : 發送推播訊息
            //
            // from_account: 發送端 SIP account
            // to_account: 接受端 SIP account
            // message: 發送訊息,如果此參數不給或是空值,則發送內容以後台為主
            // 
            // 回應 0 : success ,其他為錯誤代碼
            // 範例 http://syscodevoip.duckdns.org:5050/PushSipAccount?from_account=6002&to_account=6001
            Get["/PushSipAccount"] = parameters =>
            {
                string responseText = "0";
                string from_account = this.Request.Query["from_account"];
                string to_account = this.Request.Query["to_account"];
                string message = this.Request.Query["message"];

                return responseText;
            };

            // AddSipAccount: 建立 SIP 帳號
            //
            // account: SIP account
            // password: SIP password
            // apply 1: 表示新增成功後自動 apply 進 FreePBX
            //
            // 回應: 0: success ,其他為錯誤代碼
            // 範例: http://syscodevoip.duckdns.org:5050/AddSipAccount?
            // sip_account=6002&sip_pwd=xxxxxxxx＆apply=1
            Get["/AddSipAccount"] = parameters =>
            {
                string responseText = "0";
                string account = this.Request.Query["sip_account"];
                string password = this.Request.Query["sip_pwd"];
                string apply = this.Request.Query["apply"];

                return responseText;
            };

            // ApplySipAccount: 使建立的 SIP 帳號在 FreePBX 生效
            //
            // 回應: 100 : success ,其他為錯誤代碼
            // 範例: http://syscodevoip.duckdns.org:5050/ApplySipAccount
            Get["/ApplySipAccount"] = parameters =>
            {
                string responseText = "0";
                return responseText;
            };

            // AddRingGroup: 建立 Ring Group
            //
            // group_num: Ring Group ID
            // group_list: Extension Lis，各門號用 - (dash)隔開，例如：7002-7003-7007
            // apply: 1:表示新增成功後自動 apply 進 FreePBX
            //
            // 回應 0 : success ,其他為錯誤代碼
            // 範例 http://syscodevoip.duckdns.org:5050/AddRingGroup?
            // group_num=602&group_list=7002-7003-7005
            Get["/AddRingGroup"] = parameters =>
            {
                string responseText = "0";

                string group_num = this.Request.Query["group_num"];
                string group_list = this.Request.Query["group_list"];
                string apply = this.Request.Query["apply"];

                return responseText;
            };
        }
    }
}
