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
    public class OpenDoorEventsModel : CollectionModelBase<eventopendoor>
    {
        public OpenDoorEventsModel(IDataService<eventopendoor> dataService) : base(dataService)
        {
            Messenger.Default.Register<ReceivedOpenDoorEvent>(this, (msg) =>
            {
                RefillDataAction.Defer(_refillDelay);
            });
        }

        protected override Func<eventopendoor, bool> IdentityPredicate(eventopendoor obj)
        {
            return (d => d.C_from == obj.C_from
                      && d.C_time == obj.C_time);
        }

        protected override void RefillData()
        {
            _data.ReplaceRange(_dataService.SelectAll().OrderByDescending(data => data.C_time));
        }
    }
}
