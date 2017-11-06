using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 门禁卡信息表
    public partial class Iccard : BusinessObjectBase
    {
        private int _icid;
        public int C_icid
        {
            get { return _icid; }
            set { this.Set(ref _icid, value); }
        }

        private string _icno;
        /// <summary>
        /// 门禁卡序列号
        /// </summary>
        public string C_icno
        {
            get { return _icno; }
            set { this.Set(ref _icno, value); }
        }

        private string _roomid;
        /// <summary>
        /// 门禁卡使用者门牌号 
        /// </summary>
        public string C_roomid
        {
            get { return _roomid; }
            set { this.Set(ref _roomid, value); }
        }

        private string _username;
        /// <summary>
        /// 使用者名称 
        /// </summary>
        public string C_username
        {
            get { return _username; }
            set { this.Set(ref _username, value); }
        }

        private int? _ictype;
        /// <summary>
        /// 门禁卡类型 
        /// </summary>
        public int? C_ictype
        {
            get { return _ictype; }
            set { this.Set(ref _ictype, value); }
        }

        private string _icpassword;
        /// <summary>
        /// 沒用到 
        /// </summary>
        public string C_icpassword
        {
            get { return _icpassword; }
            set { this.Set(ref _icpassword, value); }
        }

        private int? _available;
        /// <summary>
        /// 是否有效 
        /// </summary>
        public int? C_available
        {
            get { return _available; }
            set { this.Set(ref _available, value); }
        }

        DateTime? _time;
        /// <summary>
        /// 门禁卡添加时间 
        /// </summary>
        public DateTime? C_time
        {
            get { return _time; }
            set { this.Set(ref _time, value); }
        }

        private DateTime? _uptime;
        /// <summary>
        /// 门禁卡时效开始时间 
        /// </summary>
        public DateTime? C_uptime
        {
            get { return _uptime; }
            set { this.Set(ref _uptime, value); }
        }

        private DateTime? _downtime;
        /// <summary>
        /// 门禁卡时效结束时间 
        /// </summary>
        public DateTime? C_downtime
        {
            get { return _downtime; }
            set { this.Set(ref _downtime, value); }
        }
    }

    public enum SecurityCardType
    {
        Resident,   // "住戶"
        Staff,      // "工作人員"
        Manager,    // "管理人員"
        TempCard,   // "臨時卡"
        OtherType   // "其他類型"
    }
}
