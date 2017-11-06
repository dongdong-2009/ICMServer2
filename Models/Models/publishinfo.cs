using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 信息发布
    public partial class publishinfo
    {
        public int id { get; set; }
        // 标题
        public string title { get; set; }
        // 发布位置
        public string dstaddr { get; set; }
        // 发布时间
        public Nullable<System.DateTime> time { get; set; }
        // 生成图片保存位置
        public string filepath { get; set; }
        // 发布类型
        public Nullable<int> type { get; set; } // 0: text, 1: picture
        // 文件格式, 沒用到
        public Nullable<int> fmt { get; set; }
        // 读取标志位
        public Nullable<int> isread { get; set; }
    }

    public enum MessageType
    {
        Text = 0,
        Image = 1
    }
}
