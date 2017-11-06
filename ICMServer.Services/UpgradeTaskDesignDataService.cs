using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class UpgradeTaskDesignDataService : DesignDataServiceBase<UpgradeTask>
    {
        protected override void InitSampleData()
        {
            // initialize devices list
            //List<Device> devices = new List<Device>();
            //for (int i = 0; i < 3; ++i)
            //{
            //    devices.Add(new Device
            //    {
            //        id = i,
            //        type = (i % 4 + 1),
            //        alias = "Design Time Alias " + i,
            //        roomid = "10-00-00-00-0" + i,
            //        online = i % 2,
            //    });
            //}

            //// initialize upgrade files list
            //List<upgrade> upgradeFiles = new List<upgrade>();
            //upgradeFiles.Add(new upgrade
            //{
            //    id = 1,
            //    filepath = @"data\firmware\201705031746363264_1.0_indoor\INDOOR.PKG",
            //    version = "1.0",
            //    filetype = 1,
            //    device_type = 5,
            //    time = new DateTime(2017, 5, 3, 17, 46, 36),
            //    is_default = 1
            //});
            //upgradeFiles.Add(new upgrade
            //{
            //    id = 2,
            //    filepath = @"data\firmware\201705031758341683_1.0_indoor\INDOOR.PKG",
            //    version = "1.1",
            //    filetype = 1,
            //    device_type = 5,
            //    time = new DateTime(2017, 5, 3, 17, 58, 34)
            //});
            //upgradeFiles.Add(new upgrade
            //{
            //    id = 3,
            //    filepath = @"data\firmware\201705031803592529_1_address\ADDRESS.PKG",
            //    version = "1",
            //    filetype = 2,
            //    device_type = -1,
            //    time = new DateTime(2017, 5, 3, 18, 3, 59),
            //    is_default = 1
            //});

            //// initialize upgrade jobs
            //_objects.Add(new UpgradeJob
            //{
            //    ID = 1,
            //    Device = devices[0],
            //    DeviceID = devices[0].id,
            //    UpgradeFile = upgradeFiles[0],
            //    UpgradeID = upgradeFiles[0].id,
            //    Status = UpgradeStatus.TBD,
            //});
            //_objects.Add(new UpgradeJob
            //{
            //    ID = 2,
            //    Device = devices[1],
            //    DeviceID = devices[1].id,
            //    UpgradeFile = upgradeFiles[1],
            //    UpgradeID = upgradeFiles[1].id,
            //    Status = UpgradeStatus.Complete,
            //});
            //_objects.Add(new UpgradeJob
            //{
            //    ID = 3,
            //    Device = devices[2],
            //    DeviceID = devices[2].id,
            //    UpgradeFile = upgradeFiles[2],
            //    UpgradeID = upgradeFiles[2].id,
            //    Status = UpgradeStatus.Removed,
            //});
        }
    }
}
