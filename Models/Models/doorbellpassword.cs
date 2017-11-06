using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 門口機看門密码表(因为开门密码现在都没存到管理中心，该表未被实际使用)
    public partial class doorbellpassword
    {
        public int C_id { get; set; }
        // 用戶門牌號
        public string C_roomid { get; set; }
        // 用戶設定密码
        public string C_password { get; set; }
        // 密码設定時間
        public Nullable<System.DateTime> C_time { get; set; }
    }
}
