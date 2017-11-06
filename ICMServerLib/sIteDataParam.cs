using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ICMServer
{
    public struct SIteDataParam
    {
        //[FieldOffset(0)]
        [MarshalAs(UnmanagedType.BStr)]
        public String address;

        //[FieldOffset(4)]
        [MarshalAs(UnmanagedType.BStr)]
        public String ip;

        //[FieldOffset(8)]
        [MarshalAs(UnmanagedType.BStr)]
        public String lParam;

        //[FieldOffset(12)]
        [MarshalAs(UnmanagedType.BStr)]
        public String wParam;

        //[FieldOffset(16)]
        public int msgtype;

        //[FieldOffset(20)]
        [MarshalAs(UnmanagedType.BStr)]
        public String filepath;

        //[FieldOffset(24)]
        public bool lwstatus;
    }
}
