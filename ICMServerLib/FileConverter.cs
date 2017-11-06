using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public class FileConverter
    {
        public static bool Xml2Ucl(string xmlFilePath, string uclFilePath)
        {
            bool result = false;

            Process p = new Process();
            p.StartInfo.FileName = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
                                 + @"\uclpack.exe";
            // 額外的參數
            p.StartInfo.Arguments = string.Format(" --10 --nrv2e \"{0}\" \"{1}\"", xmlFilePath, uclFilePath);
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit();
            result = true;

            return result;
        }
    }
}
