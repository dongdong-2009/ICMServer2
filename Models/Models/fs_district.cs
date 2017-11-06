using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    public partial class fs_district
    {
        public long DistrictID { get; set; }
        public string DistrictName { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
