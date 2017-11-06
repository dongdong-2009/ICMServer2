using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class CommonEventDesignDataService : DesignDataServiceBase<eventcommon>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new eventcommon
            {
                srcaddr = "01-02-03-04-05-06",
                time = DateTime.Now,
                // handlestatus = null,
                // handletime = null,
                // type = null,
                content = "需要帮助，维修马桶",
                action = "",
                handler = "王小明"
            });
        }
    }
}
