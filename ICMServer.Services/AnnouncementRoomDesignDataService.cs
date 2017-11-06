using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class AnnouncementRoomDesignDataService : DesignDataServiceBase<AnnouncementRoom>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new AnnouncementRoom
            {
                ID = 1,
                AnnouncementID = 1,
                RoomID = "01-01-01-01-01",
                HasRead = false
            });
            _objects.Add(new AnnouncementRoom
            {
                ID = 1,
                AnnouncementID = 1,
                RoomID = "01-01-01-01-02",
                HasRead = false
            });
            _objects.Add(new AnnouncementRoom
            {
                ID = 2,
                AnnouncementID = 1,
                RoomID = "01-01-01-01-01",
                HasRead = false
            });
        }
    }
}
