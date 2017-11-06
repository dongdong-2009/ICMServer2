using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    public partial class fs_city
    {
        public string Country { get; set; }
        public long CityID { get; set; }
        public string CityName { get; set; }
        public string ZipCode { get; set; }
        public Nullable<long> ProvinceID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
