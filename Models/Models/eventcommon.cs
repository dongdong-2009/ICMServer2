using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 普通事件记录
    public partial class eventcommon
    {
        // 时间上报位置
        public string srcaddr { get; set; }
        // 事件上报时间
        public System.DateTime time { get; set; }
        // 事件处理状态
        public Nullable<int> handlestatus { get; set; }
        // 事件处理时间
        public Nullable<System.DateTime> handletime { get; set; }
        // 事件类型
        public Nullable<int> type { get; set; }
        // 时间备注内容
        public string content { get; set; }
        // 事件动作
        public string action { get; set; }
        // 事件处理人
        public string handler { get; set; }
    }
}
