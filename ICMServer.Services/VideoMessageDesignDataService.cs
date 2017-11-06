using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class VideoMessageDesignDataService : DesignDataServiceBase<leaveword>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new leaveword
            {
                id = 1,
                filenames = @"C:\Program Files (x86)\ICMServer\data\leaveword\1495449270.lw",
                src_addr = "01-01-10-00-00-02",
                dst_addr = "01-01-10-01-01",
                time = DateTime.Now.ToString(),
                readflag = 1
            });
        }
    }
}
