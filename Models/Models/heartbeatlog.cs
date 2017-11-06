using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 心跳包记录
    public partial class heartbeatlog
    {
        public int C_id { get; set; }
        // 心跳包内容
        public string C_log { get; set; }
        // 心跳包时间
        public Nullable<System.DateTime> C_logtime { get; set; }
    }
}
