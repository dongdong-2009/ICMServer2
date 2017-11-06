using GalaSoft.MvvmLight.Messaging;
using ICMServer.Models;
using ICMServer.Services.Data;
using ICMServer.WPF.Messages;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    class VideoMessagesModel : CollectionModelBase<leaveword>
    {
        public VideoMessagesModel(IDataService<leaveword> dataService) : base(dataService)
        {
            Messenger.Default.Register<ReceivedNewVideoMessageEvent>(this, (msg) =>
            {
                RefillDataAction.Defer(_refillDelay);
            });
            Messenger.Default.Register<VideoMessageHasBeenReadEvent>(this, (msg) =>
            {
                RefillDataAction.Defer(_refillDelay);
            });
        }

        public override Task UpdateAsync(leaveword obj, params Expression<Func<leaveword, object>>[] modifiedProperties)
        {
            return Task.Run(() =>
            {
                _dataService.Update(obj, modifiedProperties);
                lock (_lock)
                {
                    var objToBeUpdated = (from d in _data
                                          where d.id == obj.id
                                          select d).SingleOrDefault();
                    if (objToBeUpdated != null)
                    {
                        int index = _data.IndexOf(objToBeUpdated);
                        _data[index] = obj;
                    }
                }
            });
        }

        protected override Func<leaveword, bool> IdentityPredicate(leaveword obj)
        {
            return (d => d.id == obj.id);
        }
    }
}
