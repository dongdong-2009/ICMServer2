using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 门禁设备映射门禁卡
    public partial class icmap
    {
        public int C_id { get; set; }
        // 门禁卡序列号
        public string C_icno { get; set; }
        // 门禁设备房号
        public string C_entrancedoor { get; set; }
    }
}
