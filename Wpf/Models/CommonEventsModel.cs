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
    public class CommonEventsModel : CollectionModelBase<eventcommon>
    {
        public CommonEventsModel(IDataService<eventcommon> dataService) : base(dataService)
        {
            Messenger.Default.Register<ReceivedCommonEvent>(this, (msg) =>
            {
                RefillDataAction.Defer(_refillDelay);
            });
        }

        protected override Func<eventcommon, bool> IdentityPredicate(eventcommon obj)
        {
            return (d => d.srcaddr == obj.srcaddr
                      && d.time == obj.time);
        }

        protected override void RefillData()
        {
            _data.ReplaceRange(_dataService.SelectAll().OrderByDescending(data => data.time));
        }
    }
}
