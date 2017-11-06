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
    public class AdvertisementsModel : CollectionModelBase<advertisement>, IAdvertisementsModel
    {
        public AdvertisementsModel(IDataService<advertisement> dataService) : base(dataService)
        {
        }

        protected override Func<advertisement, bool> IdentityPredicate(advertisement obj)
        {
            return (d => d.C_id == obj.C_id);
        }

        protected override void RefillData()
        {
            _data.ReplaceRange(_dataService.SelectAll().OrderBy(a => a.C_no));
        }

        public override void Insert(advertisement obj)
        {
            var currentMaxPlayOrder = _dataService.SelectAll().Max((advertisement) => advertisement.C_no);
            obj.C_no = (currentMaxPlayOrder == null) ? 1 : (currentMaxPlayOrder + 1);
            obj.C_time = DateTime.Now;
            _dataService.Insert(obj);
            RefillDataAction.Defer(_refillDelay);
        }

        public override Task DeleteAsync(IList objs)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    List<advertisement> objsToBeRemoved = new List<advertisement>();
                    foreach (var obj in objs)
                        objsToBeRemoved.Add(obj as advertisement);

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
                    RelabelThePlayOrder();
                }

                RefillDataAction.Defer(_refillDelay);
            });
        }

        private void RelabelThePlayOrder()
        {
            int i = 1;
            foreach (var obj in _data)
            {
                if (obj.C_no != i)
                {
                    obj.C_no = i;
                    _dataService.Update(obj);
                }
                i++;
            }
        }

        TaskQueue updateTasks = new TaskQueue();

        public Task MoveUpAsync(advertisement obj)
        {
            return Task.Run(async () =>
            {
                advertisement currentItem = obj;
                advertisement previousItem;

                lock (_lock)
                {
                    int currentIndex = _data.IndexOf(currentItem);
                    if (currentIndex == 0)
                        return;
                    previousItem = _data[currentIndex - 1];
                    int? tmp = currentItem.C_no;
                    currentItem.C_no = previousItem.C_no;
                    previousItem.C_no = tmp;

                    _data[currentIndex - 1] = currentItem;
                    _data[currentIndex] = previousItem;
                }

                List<advertisement> objs = new List<advertisement>();
                objs.Add(currentItem);
                objs.Add(previousItem);
                await updateTasks.Enqueue(() => _dataService.UpdateAsync(objs));
                //await updateTasks.Enqueue(() => _dataService.UpdateAsync(currentItem)).ConfigureAwait(false);
                //await updateTasks.Enqueue(() => _dataService.UpdateAsync(previousItem)).ConfigureAwait(false);
            });
        }

        public Task MoveDownAsync(advertisement obj)
        {
            return Task.Run(async () =>
            {
                advertisement currentItem = obj;
                advertisement nextItem;

                lock (_lock)
                {
                    int currentIndex = _data.IndexOf(currentItem);
                    if (currentIndex == _data.Count - 1)
                        return;
                    nextItem = _data[currentIndex + 1];
                    int? tmp = currentItem.C_no;
                    currentItem.C_no = nextItem.C_no;
                    nextItem.C_no = tmp;

                    _data[currentIndex + 1] = currentItem;
                    _data[currentIndex] = nextItem;
                }
                List<advertisement> objs = new List<advertisement>();
                objs.Add(currentItem);
                objs.Add(nextItem);
                await updateTasks.Enqueue(() => _dataService.UpdateAsync(objs));

                //await updateTasks.Enqueue(() => _dataService.UpdateAsync(currentItem)).ConfigureAwait(false);
                //await updateTasks.Enqueue(() => _dataService.UpdateAsync(nextItem)).ConfigureAwait(false);
            });
        }
    }
}
