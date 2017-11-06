using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 软件升级记录
    public partial class upgrade : BusinessObjectBase
    {
        private int _id;
        public int id
        {
            get { return _id; }
            set { this.Set(ref _id, value); }
        }

        private string _filepath;
        /// <summary>
        /// 文件保存位置
        /// </summary>
        public string filepath
        {
            get { return _filepath; }
            set { this.Set(ref _filepath, value); }
        }

        private string _version;
        /// <summary>
        /// 版本
        /// </summary>
        public string version
        {
            get { return _version; }
            set { this.Set(ref _version, value); }
        }

        private int? _filetype;
        /// <summary>
        /// 升级文件类型
        /// </summary>
        public int? filetype
        {
            get { return _filetype; }
            set { this.Set(ref _filetype, value); }
        }

        private int? _Device_type;
        /// <summary>
        /// 升级设备类型
        /// </summary>
        public int? Device_type
        {
            get { return _Device_type; }
            set { this.Set(ref _Device_type, value); }
        }

        private DateTime? _time;
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime? time
        {
            get { return _time; }
            set { this.Set(ref _time, value); }
        }

        private int? _is_default;
        /// <summary>
        /// 是否是默认升级文件
        /// </summary>
        public int? is_default
        {
            get { return _is_default; }
            set { this.Set(ref _is_default, value); }
        }
    }

    public enum UpgradeFileType
    {
        SoftwareUpgrade = 1,        // "軟件升級"
        AddressBookUpgrade,         // "地址薄升級"
        ScreenSaverUpgrade,         // "屏保升級"
        SecurityCardListUpgrade,    // "門禁卡升級"
    };
}
