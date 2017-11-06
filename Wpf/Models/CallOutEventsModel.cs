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
    public class CallOutEventsModel : CollectionModelBase<eventcallout>
    {
        public CallOutEventsModel(IDataService<eventcallout> dataService) : base(dataService)
        {
            Messenger.Default.Register<ReceivedCallOutEvent>(this, (msg) =>
            {
                RefillDataAction.Defer(_refillDelay);
            });
        }

        protected override Func<eventcallout, bool> IdentityPredicate(eventcallout obj)
        {
            return (d => d.@from == obj.@from
                         && d.time == obj.time);
        }

        protected override void RefillData()
        {
            _data.ReplaceRange(_dataService.SelectAll().OrderByDescending(data => data.time));
        }
    }
}
