using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    public class SecurityCardsModel : CollectionModelBase<iccard>
    {
        private readonly ISecurityCardsDevicesModel _securityCardsDevicesDataModel;

        public SecurityCardsModel(
            IDataService<iccard> dataService,
            ISecurityCardsDevicesModel securityCardsDevicesDataModel) : base(dataService)
        {
            this._securityCardsDevicesDataModel = securityCardsDevicesDataModel;
        }

        protected override Func<iccard, bool> IdentityPredicate(iccard obj)
        {
            return (d => d.C_icid == obj.C_icid);
        }

        public override Task DeleteAsync(IList objs)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    List<iccard> objsToBeRemoved = new List<iccard>();
                    foreach (var obj in objs)
                        objsToBeRemoved.Add(obj as iccard);

                    foreach (var obj in objsToBeRemoved)
                    {
                        try
                        {
                            if (obj == null)
                                break;

                            _dataService.Delete(obj);
                            _data.Remove(obj);
                            _securityCardsDevicesDataModel.DeleteSecurityCardAsync(obj);
                        }
                        catch (EntityException) // TODO: 更好的錯誤提示
                        {
                            // database is offline
                            break;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                }

                RefillDataAction.Defer(_refillDelay);
            });
        }
    }
}
