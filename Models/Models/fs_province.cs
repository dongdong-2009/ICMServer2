using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    public partial class fs_province
    {
        public long ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
