using ICMServer.Services.Data;
using ICMServer.WPF.Collections.ObjectModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ICMServer.WPF.Models
{
    public abstract class CollectionModelBase<T> : ICollectionModel<T> where T : class
    {
        protected IDataService<T> _dataService;
        protected ObservableRangeCollection<T> _data;
        protected object _lock = new object();

        protected DeferredAction _refillDataAction;
        protected TimeSpan _refillDelay = TimeSpan.FromMilliseconds(250);
        protected TimeSpan _refillTimeOut = TimeSpan.FromMilliseconds(1000);

        protected abstract Func<T, bool> IdentityPredicate(T obj);

        public CollectionModelBase(
            IDataService<T> dataService)
        {
            this._dataService = dataService;
            //this._identityPredicate = identityPredicate;
            _data = new ObservableRangeCollection<T>();
            //DebugLog.TraceMessage("");
            BindingOperations.EnableCollectionSynchronization(_data, _lock);
        }

        protected DeferredAction RefillDataAction
        {
            get
            {
                return _refillDataAction ?? (_refillDataAction = DeferredAction.Create(
                    async () => await RefillDataAsync().ConfigureAwait(false),
                    _refillTimeOut));
            }
        }

        public virtual ObservableRangeCollection<T> Data
        {
            get { return _data; }
        }

        public virtual void DeleteAll()
        {
            _dataService.DeleteAll();
            lock (_lock)
            {
                _data.Clear();
            }
        }

        //public virtual int Count()
        //{
        //    lock (_lock)
        //    {
        //        return _data.Count;
        //    }
        //}

        public virtual bool Delete(T obj)
        {
            if (obj == null)
                return false;

            try
            {
                _dataService.Delete(obj);
                lock (_lock)
                {
                    _data.Remove(obj);
                }
                return true;
            }
            catch (EntityException) // TODO: 更好的錯誤提示
            {
            }
            catch (Exception)
            {
            }

            return false;
        }

        public virtual void Delete(IList objs)
        {
            if (objs == null)
                return;

            lock (_lock)
            {
                List<T> objsToBeRemoved = new List<T>();
                foreach (var obj in objs)
                    objsToBeRemoved.Add(obj as T);

                foreach (var obj in objsToBeRemoved)
                {
                    if (!Delete(obj))
                        break;
                }
            }

            RefillDataAction.Defer(_refillDelay);
        }

        public virtual Task DeleteAsync(IList objs)
        {
            return Task.Run(() =>
            {
                Delete(objs);
            });
        }

        public virtual void Insert(T obj)
        {
            _dataService.Insert(obj);
            RefillDataAction.Defer(_refillDelay);
        }

        public virtual Task InsertAsync(T obj)
        {
            return Task.Run(() =>
            {
                Insert(obj);
            });
        }

        protected virtual void RefillData()
        {
            _data.ReplaceRange(_dataService.SelectAll());
        }

        protected int _refillDataRequestWriteIndex = 0;
        protected int _refillDataRequestReadIndex = 0;
        protected object _refillDataRequestLock = new object();
        //TaskQueue _refillDataTasks = new TaskQueue();
        //await validationTasks.Enqueue<bool>(() => this.ValidateAsync());
        public virtual Task RefillDataAsync()
        {
            lock (_refillDataRequestLock)
            {
                _refillDataRequestWriteIndex++;
                //DebugLog.TraceMessage(string.Format("Insert request w({0}), r({1})", _refillDataRequestWriteIndex, _refillDataRequestReadIndex));
            }

            return Task.Run(() =>
            {
                {
                    int w, r;
                    lock (_refillDataRequestLock)
                    {
                        w = _refillDataRequestWriteIndex;
                        r = ++_refillDataRequestReadIndex;
                        //DebugLog.TraceMessage(string.Format("w({0}), r({1})", w, r));
                    }

                    if (w != r)
                    {
                        //DebugLog.TraceMessage(string.Format("Skip Refill w({0}), r({1})", w, r));
                        return;
                    }

                    try
                    {
                        lock (_lock)
                        {
                            //DebugLog.TraceMessage(string.Format("Do Refill w({0}), r({1})", _refillDataRequestWriteIndex, _refillDataRequestReadIndex));
                            RefillData();
                        }
                    }
                    catch (Exception) { }

                    //lock (_refillDataRequestLock)
                    //{
                    //    _refillDataRequestReadIndex = w;
                    //    DebugLog.TraceMessage(string.Format("w({0}), r({1})", _refillDataRequestWriteIndex, _refillDataRequestReadIndex));
                    //}
                }

            });
        }

        public virtual IEnumerable<T> Select(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            return this._dataService.Select(predicate, navigationProperties);
        }

        //public abstract Task UpdateAsync(T obj, params Expression<Func<T, object>>[] modifiedProperties);
        public virtual Task UpdateAsync(T obj, params Expression<Func<T, object>>[] modifiedProperties)
        {
            return Task.Run(() =>
            {
                try
                {
                    _dataService.Update(obj, modifiedProperties);
                }
                catch (DBConcurrencyException) { }
                lock (_lock)
                {
                    var objToBeUpdated = _data
                        .Where(IdentityPredicate(obj))
                        .Select(d => d)
                        .SingleOrDefault();
                    if (objToBeUpdated != null)
                    {
                        int index = _data.IndexOf(objToBeUpdated);
                        if (modifiedProperties.Any())
                        {
                            foreach (var modifiedProperty in modifiedProperties)
                            {
                                SetProperty(_data[index], obj, modifiedProperty);
                            }
                        }
                        else
                            _data[index] = obj;
                    }
                }
            });
        }

        protected void SetProperty(T target, T source, Expression<Func<T, object>> modifiedProperty)
        {
            Expression expression = modifiedProperty.Body;
            var withoutConvert = expression.RemoveConvert(); // Removes boxing
            var memberExpression = withoutConvert as MemberExpression;
            var callExpression = withoutConvert as MethodCallExpression;

            if (memberExpression != null)
            {
                var property = memberExpression.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(target, property.GetValue(source), null);
                }
            }
        }
    }

    internal static class ExpressionExtensions
    {
        public static Expression RemoveConvert(this Expression expression)
        {
            //DebugCheck.NotNull(expression);

            while (expression.NodeType == ExpressionType.Convert
                || expression.NodeType == ExpressionType.ConvertChecked)
            {
                expression = ((UnaryExpression)expression).Operand;
            }

            return expression;
        }
    }
}
