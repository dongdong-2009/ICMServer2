using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    public class AnnouncementsRoomsModel : CollectionModelBase<AnnouncementRoom>, IAnnouncementsRoomsModel
    {
        private Announcement _Announcement;
        private object _AnnouncementLock = new object();

        public AnnouncementsRoomsModel(
            IDataService<AnnouncementRoom> dataService) : base(dataService)
        {
        }

        public void SetAnnouncement(Announcement obj)
        {
            //bool isDifferent = false;
            lock (_AnnouncementLock)
            {
                //if (_Announcement != obj)
                //{
                _Announcement = obj;
                //    isDifferent = true;
                //}
            }

            //if (isDifferent)
            RefillDataAction.Defer(_refillDelay);
        }

        protected override void RefillData()
        {
            DebugLog.TraceMessage("");
            lock (_AnnouncementLock)
            {
                if (this._Announcement == null)
                    _data.Clear();
                else
                {
                    //_data.ReplaceRange(_dataService.Select(mapping => mapping.AnnouncementID == this._Announcement.id).ToList());
                    var rooms = _dataService.Select(mapping => mapping.AnnouncementID == this._Announcement.id).ToList();
                    _data.ReplaceRange(rooms);
                }
            }
        }

        protected override Func<AnnouncementRoom, bool> IdentityPredicate(AnnouncementRoom obj)
        {
            return (d => d.ID == obj.ID);
        }

        public Task DeleteAnnouncementAsync(Announcement obj)
        {
            return Task.Run(() =>
            {
                if (obj == null)
                    return;

                _dataService.Delete(m => m.AnnouncementID == obj.id);

                bool needToRefresh = false;
                lock (_AnnouncementLock)
                {
                    if (this._Announcement == obj)
                    {
                        this._Announcement = null;
                        needToRefresh = true;
                    }
                }
                if (needToRefresh)
                    RefillDataAction.Defer(_refillDelay);
            });
        }

    }
}
