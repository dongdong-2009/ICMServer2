using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class SipAccountDesignDataService : DesignDataServiceBase<sipaccount>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new sipaccount
            {

                C_user = "000000000102",
                C_password = "qHU7MYw7ud",
                C_room = "00-00-00-00-01",
                C_usergroup = "0000000001",
                C_randomcode = "253",
                C_updatetime = new DateTime(2017, 05, 04, 11, 19, 56),
                C_registerstatus = 0,
                C_sync = 0
            });
        }
    }
}
