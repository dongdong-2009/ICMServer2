using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 住户信息表
    public partial class holderinfo : BusinessObjectBase
    {
        private int _id;
        public int C_id
        {
            get { return _id; }
            set { this.Set(ref _id, value); }
        }

        // 住户名称
        private string _name;
        public string C_name
        {
            get { return _name; }
            set { this.Set(ref _name, value); }
        }

        // 住户性别
        public int? _sex;
        public int? C_sex
        {
            get { return _sex; }
            set { this.Set(ref _sex, value); }
        }

        // 电话号码
        public string _phone;
        public string C_phoneno
        {
            get { return _phone; }
            set { this.Set(ref _phone, value); }
        }

        // 住户门牌号
        public string _roomid;
        public string C_roomid
        {
            get { return _roomid; }
            set { this.Set(ref _roomid, value); }
        }

        // 是否户主
        int? _isholder;
        public int? C_isholder
        {
            get { return _isholder; }
            set { this.Set(ref _isholder, value); }
        }
    }

    public enum Sex
    {
        Male = 0,
        Female
    }
}
