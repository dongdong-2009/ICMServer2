using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class DeviceDesignDataService : DesignDataServiceBase<Device>, IDeviceDataService
    {
        public IEnumerable<DeviceSecurityCardDto> SelectCardList()
        {
            throw new NotImplementedException();
        }

        protected override void InitSampleData()
        {
            //Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                Device device = new Device
                {
                    id = i,
                    type = (i % 4 + 1),
                    alias = "Design Time Alias " + i,
                    roomid = "10-00-00-00-0" + i,
                    online = i % 2,
                };

                _objects.Add(device);
            }
        }
    }
}
