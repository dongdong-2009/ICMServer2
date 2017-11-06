using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class ResidentDesignDataService : DesignDataServiceBase<holderinfo>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new holderinfo
            {
                C_id = 1,
                C_name = "王小明",
                C_sex = 0,
                C_phoneno = "03-1234567",
                C_roomid = "01-01-01-01-01",
                C_isholder = 1
            });
            _objects.Add(new holderinfo
            {
                C_id = 2,
                C_name = "王小華",
                C_sex = 1,
                C_phoneno = "03-1234567",
                C_roomid = "01-01-01-01-01",
                C_isholder = 0
            });
        }
    }
}
