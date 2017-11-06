using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class UserDesignDataService : DesignDataServiceBase<user>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new user
            {
                C_id = 1,
                C_userno = "000000",
                C_username = "admin",
                C_powerid = null,
                C_password = "123456"
            });
        }
    }
}
