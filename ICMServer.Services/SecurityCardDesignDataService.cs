using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class SecurityCardDesignDataService : DesignDataServiceBase<iccard>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new iccard
            {
                C_icid = 1,
                C_roomid = "02-01-06-01-01",
                C_icno = "00000123131231231",
                C_username = "王小明",
                C_ictype = 0,
                C_available = 1,
                C_uptime = new DateTime(2017, 04, 28, 18, 08, 16),
                C_downtime = new DateTime(2017, 04, 28, 18, 08, 16)
            });
        }
    }
}
