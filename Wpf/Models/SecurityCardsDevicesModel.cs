using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    class SecurityCardsDevicesModel : CollectionModelBase<icmap>, ISecurityCardsDevicesModel
    {
        private iccard _SecurityCard;
        private object _SecurityCardLock = new object();

        public SecurityCardsDevicesModel(
            IDataService<icmap> dataService) : base(dataService)
        {
        }

        public void SetSecurityCard(iccard obj)
        {
            //bool isDifferent = false;
            lock (_SecurityCardLock)
            {
                //if (_SecurityCard != obj)
                //{
                    _SecurityCard = obj;
                //    isDifferent = true;
                //}
            }

            //if (isDifferent)
                RefillDataAction.Defer(_refillDelay);
        }
        
        protected override void RefillData()
        {
            DebugLog.TraceMessage("");
            lock (_SecurityCardLock)
            {
                if (this._SecurityCard == null)
                    _data.Clear();
                else
                {
                    _data.ReplaceRange(_dataService.Select(mapping => mapping.C_icno == this._SecurityCard.C_icno));
                }
            }
        }

        protected override Func<icmap, bool> IdentityPredicate(icmap obj)
        {
            return (d => d.C_icno == obj.C_icno);
        }

        public Task DeleteSecurityCardAsync(iccard obj)
        {
            return Task.Run(() =>
            {
                if (obj == null)
                    return;

                _dataService.Delete(m => m.C_icno == obj.C_icno);

                bool needToRefresh = false;
                lock (_SecurityCardLock)
                {
                    if (this._SecurityCard == obj)
                    {
                        this._SecurityCard = null;
                        needToRefresh = true;
                    }
                }
                if (needToRefresh)
                    RefillDataAction.Defer(_refillDelay);
            });
        }
    }
}
