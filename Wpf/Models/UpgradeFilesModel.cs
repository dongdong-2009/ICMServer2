using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace ICMServer.WPF.Models
{
    class UpgradeFilesModel : CollectionModelBase<upgrade>
    {
        public UpgradeFilesModel(IDataService<upgrade> dataService) : base(dataService)
        {
        }

        private void UpdateLocalObject(upgrade obj)
        {
            if (obj == null)
                return;

            var objToBeUpdated = (from d in _data
                                  where d.id == obj.id
                                  select d).SingleOrDefault();
            if (objToBeUpdated != null)
            {
                int index = _data.IndexOf(objToBeUpdated);
                _data[index] = obj;
            }
        }

        protected override Func<upgrade, bool> IdentityPredicate(upgrade obj)
        {
            return (d => d.id == obj.id);
        }

        public override Task UpdateAsync(upgrade obj, params Expression<Func<upgrade, object>>[] modifiedProperties)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    upgrade defaultObj = null;
                    using (var ts = new TransactionScope())
                    {
                        if (obj.is_default.HasValue && obj.is_default == 1)
                        {
                            defaultObj = _dataService.Select(d =>
                                d.is_default == 1
                                && d.filetype == obj.filetype
                                && d.device_type == obj.device_type).FirstOrDefault();
                            if (defaultObj != null && defaultObj.id != obj.id)
                            {
                                // TODO: Database Transaction
                                defaultObj.is_default = 0;
                                _dataService.Update(defaultObj, modifiedProperties);
                                UpdateLocalObject(defaultObj);
                            }
                        }
                        _dataService.Update(obj, modifiedProperties);
                        ts.Complete();
                    }
                    UpdateLocalObject(obj);
                }
            });
        }

        public override void Insert(upgrade obj)
        {
            if (obj.device_type == null)
                obj.device_type = -1;
            var defaultObj = _dataService.Select(info => info.filetype == obj.filetype
                                              && info.device_type == obj.device_type
                                              && info.is_default == 1).FirstOrDefault();
            if (defaultObj == null)
                obj.is_default = 1;
            _dataService.Insert(obj);
            RefillDataAction.Defer(_refillDelay);
        }

        public override Task DeleteAsync(IList objs)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    List<upgrade> objsToBeRemoved = new List<upgrade>();
                    foreach (var obj in objs)
                        objsToBeRemoved.Add(obj as upgrade);

                    foreach (var obj in objsToBeRemoved)
                    {
                        try
                        {
                            if (obj == null)
                                break;

                            _dataService.Delete(obj);
                            _data.Remove(obj);
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

                    var defaultObjsToBeRemoved = from obj in objsToBeRemoved
                                                 where obj.is_default == 1
                                                 select obj;
                    foreach (var obj in defaultObjsToBeRemoved)
                    {
                        try
                        {
                            var candidateObj = _dataService.Select(info => info.filetype == obj.filetype
                                                                && info.device_type == obj.device_type).FirstOrDefault();
                            if (candidateObj == null)
                                continue;

                            candidateObj.is_default = 1;
                            _dataService.Update(candidateObj);
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
