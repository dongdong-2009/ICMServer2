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
    public class AlarmsModel : CollectionModelBase<eventwarn>
    {
        public AlarmsModel(IDataService<eventwarn> dataService) : base(dataService)
        {
            Messenger.Default.Register<ReceivedAlarm>(this, (msg) =>
            {
                RefillDataAction.Defer(_refillDelay);
            });
        }

        protected override Func<eventwarn, bool> IdentityPredicate(eventwarn obj)
        {
            return (d => d.srcaddr == obj.srcaddr
                      && d.time == obj.time
                      && d.channel == obj.channel
                      && d.action == obj.action);
        }

        protected override void RefillData()
        {
            _data.ReplaceRange(_dataService.SelectAll().OrderByDescending(data => data.time));
        }
    }
}
