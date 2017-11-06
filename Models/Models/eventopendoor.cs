using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 开门事件记录
    public partial class eventopendoor
    {
        // 开门上报位置
        public string C_from { get; set; }
        // 开门方式
        public string C_mode { get; set; }
        // 开门者
        public string C_open_object { get; set; }
        // 开门时间
        public System.DateTime C_time { get; set; }
        // 开门成功标记
        public Nullable<int> C_verified { get; set; }
        // 该字段已经无效
        public string C_img { get; set; }
    }

    public enum OpenDoorResult
    {
        Fail = 0,
        Success
    }
}
