using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class AnnouncementDesignDataService : DesignDataServiceBase<Announcement>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new Announcement
            {
                id = 1,
                title = "社區環境消毒",
                //dstaddr = "01-01-01-01-01",
                time = DateTime.Now,
                filepath = @"data\publish_informations\201704261357595016.jpg",
                type = 0,
                //isread = 0
            });
            _objects.Add(new Announcement
            {
                id = 1,
                title = "社區環境消毒",
                //dstaddr = "01-01-01-01-02",
                time = DateTime.Now,
                filepath = @"data\publish_informations\201704261357595016.jpg",
                type = 0,
                //isread = 1
            });

            _objects.Add(new Announcement
            {
                id = 1,
                title = "母親節活動",
                //dstaddr = "01-01-01-01-01",
                time = DateTime.Now,
                filepath = @"data\publish_informations\201704261357595018.jpg",
                type = 0,
            });
        }
    }
}
