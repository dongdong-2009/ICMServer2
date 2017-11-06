using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    public interface IAnnouncementsRoomsModel : ICollectionModel<AnnouncementRoom>
    {
        void SetAnnouncement(Announcement obj);
        Task DeleteAnnouncementAsync(Announcement obj);
    }
}
