using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 户户对讲设备权限关联表
    public partial class addrassociate
    {
        public int C_id { get; set; }
        // 映射地址A
        public string C_addrA { get; set; }
        // 映射地址B
        public string C_addrB { get; set; }
        // 映射設備A類型
        public Nullable<int> C_typeA { get; set; }
        // 映射設備B類型
        public Nullable<int> C_typeB { get; set; }
        public string C_des { get; set; }
    }
}
