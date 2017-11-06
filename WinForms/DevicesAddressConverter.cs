using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICMServer
{
    class DevicesAddressConverter
    {
        static readonly string[] SeparatorTexts = { 
            strings.qu,     // "区"
            strings.dong,   // "栋"
            strings.unit,   // "单元" 
            strings.ceng,   // "层"
            strings.shi,    // "室"
            strings.ext     // "分机"
        };

        public static string RoToChStr(string shortAddr)
        {
            if (shortAddr == null || shortAddr == "")
                return "";

            string longAddr = "";
            string[] tokens = shortAddr.Split('-');
            int min = Math.Min(tokens.Length, SeparatorTexts.Length);

            for (int i = 0; i < min; ++i)
            {
                longAddr += tokens[i] + SeparatorTexts[i];
            }
            
            return longAddr;
        }

        public static string ChStrToRo(string StrRoom)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;
            if (currentCulture.ToString() == "zh-CN")
            {
                string[] addrsplit = StrRoom.Split('区', '栋', '单', '层', '室', '分');
                StrRoom = string.Join("-", addrsplit);//parse & remove chinese from addr
                addrsplit = StrRoom.Split('元', '机');
                StrRoom = string.Join("", addrsplit).TrimEnd('-');
            }
            else
            {
                string[] addrsplit = StrRoom.Split('区', '栋', '单', '层', '室', '分');
                StrRoom = string.Join("-", addrsplit);//parse & remove chinese from addr
                addrsplit = StrRoom.Split('元', '机');
                StrRoom = string.Join("", addrsplit).TrimEnd('-');
            }
            return StrRoom;
        }
    }
}
