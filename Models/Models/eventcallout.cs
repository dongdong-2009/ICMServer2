using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 对讲事件表
    public partial class eventcallout
    {
        // 呼叫方位置
        public string from { get; set; }
        // 被呼叫方位置
        public string to { get; set; }
        public string owner { get; set; }
        // 事件發生時間
        public System.DateTime time { get; set; }
        // 事件類型
        public Nullable<int> type { get; set; }
        // 事件动作
        public string action { get; set; }
        // 该字段為無效字段
        public string img { get; set; }
    }
}
