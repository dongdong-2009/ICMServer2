using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class RoomDesignDataService : DesignDataServiceBase<Room>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new Room
            {
                ID = "01-01-01-01-01",
            });
            _objects.Add(new Room
            {
                ID = "01-01-01-01-02",
            });
            _objects.Add(new Room
            {
                ID = "01-10-01-01-01",
            });
        }
    }
}
