using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class SecurityCardDeviceDesignDataService : DesignDataServiceBase<icmap>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new icmap
            {
                C_id = 1,
                C_icno = "00000123131231231",
                C_entrancedoor = "01-01-10-00-00-02"
            });
            _objects.Add(new icmap
            {
                C_id = 2,
                C_icno = "00000123131231231",
                C_entrancedoor = "01-01-10-01-01-07"
            });
        }
    }
}
