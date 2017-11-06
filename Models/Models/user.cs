using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 登录用户表
    public partial class user
    {
        public int C_id { get; set; }
        // 用户序列号
        public string C_userno { get; set; }
        // 用户名称
        public string C_username { get; set; }
        // 用户权限(为authority表中的_id key)
        public Nullable<int> C_powerid { get; set; }
        // 登录密码
        public string C_password { get; set; }
    }
}
