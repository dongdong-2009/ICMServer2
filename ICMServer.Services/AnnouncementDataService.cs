using ICMServer.Models;
using System.IO;
using System.Linq;

namespace ICMServer.Services.Data
{
    public class AnnouncementDataService : DataService<Announcement>
    {
        public override void Insert(Announcement dto)
        {
            if (dto == null)
                return;

            var db = CreateDbTableRepository();
            Announcement bdo = new Announcement();
            bdo.id = dto.id;
            bdo.title = dto.title;
            bdo.time = dto.time;    // DateTime.Now;
            bdo.filepath = dto.filepath;
            bdo.type = dto.type;
            bdo.fmt = dto.fmt;
            foreach (var dtoar in dto.AnnouncementRooms)
            {
                var bdoar = new AnnouncementRoom();
                bdoar.Announcement = bdo;
                bdoar.RoomID = dtoar.Room != null ? dtoar.Room.ID : dtoar.RoomID;
                bdo.AnnouncementRooms.Add(bdoar);
            }
            db.Insert(bdo);
            db.SaveChanges();
        }

        public override void Delete(Announcement dto)
        {
            if (dto == null)
                return;

            var db = CreateDbTableRepository();
            var bdo = db.Select(a => a.id == dto.id).FirstOrDefault();
            if (bdo != null)
            {
                string filePath = Path.GetAppExeFolderPath() + @"\" + bdo.filepath;
                db.Delete(bdo);
                File.Delete(filePath);
            }
            db.SaveChanges();
        }
    }
}
