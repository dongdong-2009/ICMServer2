using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICMServer.Models
{
    public partial class sipaccount : BusinessObjectBase
    {
        public string C_user { get; set; }
        public string C_password { get; set; }
        public string C_room { get; set; }
        public string C_usergroup { get; set; }
        public string C_randomcode { get; set; }
        public Nullable<System.DateTime> C_updatetime { get; set; }

        private int? _registerstatus;
        /// <summary>
        /// online status
        /// </summary>
        public int? C_registerstatus
        {
            get { return _registerstatus; }
            set { this.Set(ref _registerstatus, value); }
        }

        private int? _sync;
        /// <summary>
        /// 是否已同步到 SIP Server
        /// </summary>
        public int? C_sync
        {
            get { return _sync; }
            set { this.Set(ref _sync, value); }
        }

        //[Index("IX_Device", 1, IsUnique = true)]
        public string Platform { get; set; }
        //[Index("IX_Device", 2, IsUnique = true)]
        public string DeviceID { get; set; }
        public string TokenID { get; set; }
    }
}
