using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer
{
    class Program
    {
        static System.Threading.Mutex _mutex;

        /// <summary>
        /// 檢查此程式是否重复開啟運行
        /// </summary>
        /// <returns>true 如果有重复開啟運行</returns>
        private static bool IsThisProgramAlreadyOpened()
        {
            bool createNew;

            Attribute guid_attr =
                Attribute.GetCustomAttribute(
                Assembly.GetExecutingAssembly(),
                typeof(GuidAttribute));
            string guid = ((GuidAttribute)guid_attr).Value;

            _mutex = new System.Threading.Mutex(true, guid, out createNew);
            if (createNew)
                _mutex.ReleaseMutex();

            return !createNew;
        }

        static void Main(string[] args)
        {
            if (IsThisProgramAlreadyOpened())
                return;

            Heartbeat.Instance.Start();

            Console.ReadLine();

            Heartbeat.Instance.Stop();
        }
    }
}
