using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq.Expressions;

namespace ICMServer.WPF.Models
{
    public class DevicesModel : CollectionModelBase<Device>
    {
        private Timer _refillTimer;
        //private ICollectionModel<Room> _roomDataModel;

        public DevicesModel(
            IDeviceDataService dataService) : base(dataService)
        {
            this._refillTimer = new Timer(new TimerCallback(delegate
            {
                Application.Current.Dispatcher.Invoke(() => RefillDataAction.Defer(_refillDelay));
            }));
            this._refillTimer.Change(TimeSpan.FromMilliseconds(250), TimeSpan.FromMilliseconds(1000));
        }

        protected override Func<Device, bool> IdentityPredicate(Device obj)
        {
            return (d => d.id == obj.id);
        }

        public override void Insert(Device obj)
        {
            if (obj.type == (int)DeviceType.Control_Server)
                obj.online = 1;
            _dataService.Insert(obj);
            RefillDataAction.Defer(_refillDelay);
        }

        public override void DeleteAll()
        {
            base.DeleteAll();
            //_roomDataModel.DeleteAll();
        }

        //public override Task DeleteAsync(IList objs)
        //{
        //    return Task.Run(() =>
        //    {
        //        lock (_lock)
        //        {
        //            List<Device> objsToBeRemoved = new List<Device>();
        //            foreach (var obj in objs)
        //                objsToBeRemoved.Add(obj as Device);

        //            foreach (var obj in objsToBeRemoved)
        //            {
        //                try
        //                {
        //                    if (obj == null)
        //                        break;

        //                    _dataService.Delete(obj);
        //                    _data.Remove(obj);
        //                }
        //                catch (EntityException) // TODO: 更好的錯誤提示
        //                {
        //                    // database is offline
        //                    break;
        //                }
        //                catch (Exception)
        //                {
        //                    break;
        //                }
        //            }

        //            // _roomDataModel.Data.Select(d => d.ID).Except()
        //        }

        //        RefillDataAction.Defer(_refillDelay);
        //    });
        //}
    }
}
