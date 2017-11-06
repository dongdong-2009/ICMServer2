using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class DeviceDataService : DataService<Device>, IDeviceDataService
    {
        public override void DeleteAll()
        {
            //base.DeleteAll();
            var db = new DbTableRepository<Device>();
            db.DbContext.Database.ExecuteSqlCommand("DELETE FROM device");
            db.DbContext.Database.ExecuteSqlCommand("ALTER TABLE device AUTO_INCREMENT = 1");
        }

        public virtual IEnumerable<DeviceSecurityCardDto> SelectCardList()
        {
            var db = new DbTableRepository<Device>().DbContext;
            return (from m in db.icmaps
                    join d in db.Devices on m.C_entrancedoor equals d.roomid
                    orderby m.C_entrancedoor
                    select new DeviceSecurityCardDto { DeviceAddress = d.roomid, DeviceType = d.type, CardNumber = m.C_icno })
                    .ToList();
        }   
    }
}
