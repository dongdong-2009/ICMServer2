using ICMServer.Models;
using ICMServer.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    class SecurityCardUnrelatedDevicesModel : CollectionModelBase<Device>, ISecurityCardUnrelatedDevicesModel
    {
        private iccard _SecurityCard;
        private object _SecurityCardLock = new object();
        private IDataService<icmap> _securityCardsRelatedDevicesDataService;

        public SecurityCardUnrelatedDevicesModel(
            IDataService<Device> dataService,
            IDataService<icmap> securityCardsRelatedDevicesDataService) : base(dataService)
        {
            this._securityCardsRelatedDevicesDataService = securityCardsRelatedDevicesDataService;
            //RefillDataAction.Defer(refillDelay);
        }

        public void SetSecurityCard(iccard obj)
        {
            //bool isDifferent = false;
            lock (_SecurityCardLock)
            {
                //if (_SecurityCard != obj)
                //{
                    _SecurityCard = obj;
                    //isDifferent = true;
                //}
            }

            //if (isDifferent)
                RefillDataAction.Defer(refillDelay);
        }
        
        protected override void RefillData()
        {
            lock (_SecurityCardLock)
            {
                var allEntranceDevices = _dataService.Select(device => (int)DeviceType.Control_Server < device.type
                                                                    && device.type < (int)DeviceType.Indoor_Phone);
                if (this._SecurityCard == null)
                {
                    _data.ReplaceRange(allEntranceDevices);
                }
                else
                {
                    var securityCardRelatedDevices = _securityCardsRelatedDevicesDataService.Select(mapping => mapping.C_icno == this._SecurityCard.C_icno);
                    var securityCardUnrelatedDevices = from d in allEntranceDevices
                                                       where !(from rd in securityCardRelatedDevices
                                                               select rd.C_entrancedoor).Equals(d.roomid)
                                                       select d;
                    _data.ReplaceRange(securityCardUnrelatedDevices);
                }
            }
        }

        public override Task UpdateAsync(Device obj)
        {
            return Task.Run(() => { });
        }
    }
}
