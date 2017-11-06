using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 用戶使用功能权限
    public partial class authority
    {
        public int C_id { get; set; }
        // 权限名稱
        public string C_name { get; set; }
        // 权限权值
        public Nullable<int> C_authority { get; set; }
    }
}
