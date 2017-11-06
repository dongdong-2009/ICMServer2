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
    class RoomsModel : CollectionModelBase<Room>, IRoomsModel
    {
        private readonly IDeviceDataService _deviceDataService;

        public RoomsModel(
            IDataService<Room> dataService,
            IDeviceDataService deviceDataService) : base(dataService)
        {
            _deviceDataService = deviceDataService;
        }

        protected override Func<Room, bool> IdentityPredicate(Room obj)
        {
            return (d => d.ID == obj.ID);
        }

        public override void Insert(Room obj)
        {
            try
            {
                _dataService.Insert(obj);
            }
            catch (Exception) { }
            RefillDataAction.Defer(_refillDelay);
        }

        public void DeleteRoomsWhichHaveNoDevices()
        {
            var roomIDs = _deviceDataService.SelectAll().Select(device => device.roomid.Substring(0, 14)).ToList();
            if (roomIDs != null)
            {
                var rooms = _dataService.Select(room => !roomIDs.Contains(room.ID)).ToList();
                this.Delete(rooms);
            }
        }

        public Task DeleteRoomsWhichHaveNoDevicesAsync()
        {
            return Task.Run(() => { DeleteRoomsWhichHaveNoDevices(); });
        }
    }
}
