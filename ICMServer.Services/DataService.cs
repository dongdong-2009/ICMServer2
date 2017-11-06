using ICMServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{     
    /// <summary>
    /// Class implementing IDataService interface and implementing
    /// its methods by making call to the Entities using ICMDBContext object
    /// </summary>

    public class DataService<T> : IDataService<T> where T : class
    {
        protected virtual DbTableRepository<T> CreateDbTableRepository()
        {
            return new DbTableRepository<T>();
        }

        public virtual IEnumerable<T> SelectAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            return CreateDbTableRepository().SelectAll(navigationProperties);
        }

        public virtual IEnumerable<T> Select(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            return CreateDbTableRepository().Select(predicate, navigationProperties);
        }
 
        public virtual void Insert(T obj)
        {
            if (obj == null)
                return;
            //DebugLog.TraceMessage("");
            CreateDbTableRepository().Insert(obj).SaveChanges();
        }

        public virtual void Update(T obj, params Expression<Func<T, object>>[] modifiedProperties)
        {
            if (obj == null)
                return;
            //DebugLog.TraceMessage("");
            CreateDbTableRepository().Update(obj, modifiedProperties).SaveChanges();
            //DebugLog.TraceMessage("");
        }

        public void Update(IEnumerable<T> objs, params Expression<Func<T, object>>[] modifiedProperties)
        {
            if (objs == null)
                return;

            var db = CreateDbTableRepository();
            foreach (var obj in objs)
            {
                db.Update(obj, modifiedProperties);
            }
            db.SaveChanges();
        }

        public virtual void Delete(T obj)
        {
            if (obj == null)
                return;
            CreateDbTableRepository().Delete(obj).SaveChanges();
        }

        public virtual void Delete(Func<T, bool> predicate)
        {
            CreateDbTableRepository().Delete(predicate).SaveChanges();
        }

        public virtual void DeleteAll()
        {
            CreateDbTableRepository().DeleteAll().SaveChanges();
        }

        public Task<IEnumerable<T>> SelectAllAsync()
        {
            return Task.Run<IEnumerable<T>>(() => { return SelectAll(); });
        }

        public Task<IEnumerable<T>> SelectAsync(Func<T, bool> predicate)
        {
            return Task.Run<IEnumerable<T>>(() => { return Select(predicate); });
        }

        public Task InsertAsync(T obj)
        {
            return Task.Run(() => { Insert(obj); });
        }

        public Task UpdateAsync(T obj, params Expression<Func<T, object>>[] modifiedProperties)
        {
            return Task.Run(() => { Update(obj, modifiedProperties); });
        }

        public Task UpdateAsync(IEnumerable<T> objs, params Expression<Func<T, object>>[] modifiedProperties)
        {
            return Task.Run(() => { Update(objs, modifiedProperties); });
        }

        public Task DeleteAsync(Func<T, bool> predicate)
        {
            return Task.Run(() => { Delete(predicate); });
        }

        public Task DeleteAsync(T obj)
        {
            return Task.Run(() => { Delete(obj); });
        }

        public Task DeleteAllAsync()
        {
            return Task.Run(() => { DeleteAll(); });
        }

    }
}
