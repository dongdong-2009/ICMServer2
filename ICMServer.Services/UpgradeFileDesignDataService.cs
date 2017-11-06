using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class UpgradeFileDesignDataService : DesignDataServiceBase<upgrade>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new upgrade
            {
                id = 1,
                filepath = @"data\firmware\201705031746363264_1.0_indoor\INDOOR.PKG",
                version = "1.0",
                filetype = 1,
                device_type = 5,
                time = new DateTime(2017, 5, 3, 17, 46, 36),
                is_default = 1
            });
            _objects.Add(new upgrade
            {
                id = 2,
                filepath = @"data\firmware\201705031758341683_1.0_indoor\INDOOR.PKG",
                version = "1.1",
                filetype = 1,
                device_type = 5,
                time = new DateTime(2017, 5, 3, 17, 58, 34)
            });
            _objects.Add(new upgrade
            {
                id = 3,
                filepath = @"data\firmware\201705031803592529_1_address\ADDRESS.PKG",
                version = "1",
                filetype = 2,
                device_type = -1,
                time = new DateTime(2017, 5, 3, 18, 3, 59),
                is_default = 1
            });
        }
    }
}
