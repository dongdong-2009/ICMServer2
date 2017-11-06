using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 在大門口機待機時提供輪播廣告功能
    public partial class advertisement : BusinessObjectBase
    {
        private int _id;
        public int C_id
        {
            get { return _id; }
            set { this.Set(ref _id, value); }
        }

        /// <summary>
        /// 播放順序
        /// </summary>
        int? _no;
        public int? C_no
        {
            get { return _no; }
            set { this.Set(ref _no, value); }
        }

        /// <summary>
        /// 影片標題
        /// </summary>
        private string _title;
        public string C_title
        {
            get { return _title; }
            set { this.Set(ref _title, value); }
        }

        /// <summary>
        /// 影片上傳時間
        /// </summary>
        public DateTime? _time;
        public DateTime? C_time
        {
            get { return _time; }
            set { this.Set(ref _time, value); }
        }

        /// <summary>
        /// 影片位於icm server內的絕對路徑
        /// </summary>
        private string _path;
        public string C_path
        {
            get { return _path; }
            set { this.Set(ref _path, value); }
        }

        /// <summary>
        /// 影片內容的checksum
        /// </summary>
        private string _checksum;
        public string C_checksum
        {
            get { return _checksum; }
            set { this.Set(ref _checksum, value); }
        }
    }
}
