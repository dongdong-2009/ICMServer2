using System;
using System.Collections.Generic;

namespace ICMServer.Models
{
    // 开门所生成的照片记录
    public partial class photograph
    {
        public int C_id { get; set; }
        // 拍照门口机装置房号
        public string C_srcaddr { get; set; }
        // 拍照时间
        public Nullable<System.DateTime> C_time { get; set; }
        // 拍照图像
        public byte[] C_img { get; set; }
    }
}
