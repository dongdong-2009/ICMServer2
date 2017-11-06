using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    class SecurityCardRepository : DbTableRepository<iccard>, ISecurityCardRepository
    {
        public IEnumerable<Device> GetRelatedDevices(iccard obj)
        {
            return (from m in _db.icmaps
                    join d in _db.Devices on m.C_entrancedoor equals d.roomid
                    where m.C_icno == obj.C_icno
                    orderby m.C_entrancedoor
                    select d).ToList();
        }
    }
}
