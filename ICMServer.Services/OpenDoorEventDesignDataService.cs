using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class OpenDoorEventDesignDataService : DesignDataServiceBase<eventopendoor>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new eventopendoor
            {
                C_from = "01-02-03-04-05-00",
                C_mode = "remote",
                C_open_object = "01-02-03-04-05-00",
                C_time = DateTime.Now,
                C_verified = 1,
            });
            _objects.Add(new eventopendoor
            {
                C_from = "01-02-03-04-05-00",
                C_mode = "card",
                C_open_object = "00000000002074A2",
                C_time = DateTime.Now,
                C_verified = 0
            });
        }
    }
}
