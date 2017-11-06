using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 留影留言
    public partial class leaveword
    {
        public int id { get; set; }
        // 留影留言文件路径名
        public string filenames { get; set; }
        // 留影留言门口机房号
        public string src_addr { get; set; }
        // 留影留言室内机房号
        public string dst_addr { get; set; }
        // 留影留言时间
        public string time { get; set; }
        // 室内机读取标记
        public Nullable<int> readflag { get; set; }
    }
}
