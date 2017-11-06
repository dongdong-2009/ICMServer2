using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 报警事件
    public partial class eventwarn : BusinessObjectBase
    {
        // 报警事件上报位置
        public string srcaddr { get; set; }
        // 报警事件生成事件
        public System.DateTime time { get; set; }

        // 处理状态
        private int? _handlestatus;
        public int? handlestatus
        {
            get { return _handlestatus ?? 0; }
            set { this.Set(ref _handlestatus, value); }
        }


        private DateTime? _handleTime;
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? handletime 
        { 
            get
            {
                if (this.handlestatus == (int)ProcessState.Processed)
                    return _handleTime;
                return null;
            }
            set { this.Set(ref _handleTime, value); } 
        }

        // 报警事件类型
        public string type { get; set; }
        // 通道位置
        public int channel { get; set; }
        // 动作
        public string Action { get; set; }

        private string _handler;
        /// <summary>
        /// 处理者
        /// </summary>
        public string handler
        {
            get { return _handler; }
            set { this.Set(ref _handler, value); }
        }
    }

    public enum ProcessState
    {
        Unprocessed = 0,    // "未處理",
        UnderProcessing,    // "處理中",
        Processed           // "已處理"
    }
}
