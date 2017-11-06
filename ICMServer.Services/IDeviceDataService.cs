using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public interface IDeviceDataService : IDataService<Device>
    {
        IEnumerable<DeviceSecurityCardDto> SelectCardList();
    }
}
