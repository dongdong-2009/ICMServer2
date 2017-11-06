using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    public interface ISecurityCardUnrelatedDevicesModel : ICollectionModel<Device>
    {
        void SetSecurityCard(iccard obj);
    }
}
