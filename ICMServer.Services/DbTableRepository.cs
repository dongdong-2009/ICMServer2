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

    public class DbTableRepository<T> : IRepository<T> where T : class
    {
        protected ICMDBContext _db = null;
        protected DbSet<T> _table = null;

        public DbTableRepository() : this(new ICMDBContext())
        {
        }

        public DbTableRepository(ICMDBContext db)
        {
            _db = db;

            _db.Configuration.LazyLoadingEnabled = false;
            _db.Configuration.ProxyCreationEnabled = false;
            _table = _db.Set<T>();
        }

        public IEnumerable<T> SelectAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _table;
#if DEBUG
            //DebugLog.TraceMessage(_table.ToList().ToString());
#endif
            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);
            return dbQuery.AsNoTracking().ToList();
        }

        public IEnumerable<T> Select(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _table;

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            return dbQuery.AsNoTracking().Where(predicate);
        }
 
        public IRepository<T> Insert(T obj)
        {
            _table.Add(obj);
            return this;
        }

        public IRepository<T> Update(T obj, params Expression<Func<T, object>>[] modifiedProperties)
        {
            _table.Attach(obj);
            if (modifiedProperties == null || modifiedProperties.Count() == 0)
                _db.Entry(obj).State = EntityState.Modified;
            else
            {
                var entry = _db.Entry(obj);
                foreach (var modifiedProperty in modifiedProperties)
                {
                    entry.Property(modifiedProperty).IsModified = true;
                }
            }
            return this;
        }

        public IRepository<T> Delete(T obj)
        {
            _table.Attach(obj);
            _db.Entry(obj).State = EntityState.Deleted;
            return this;
        }

        public IRepository<T> Delete(Func<T, bool> predicate)
        {
            var existing = _table.Where(predicate);
            _table.RemoveRange(existing);
            return this;
        }

        public virtual IRepository<T> DeleteAll()
        {
            var existing = _table.ToList();
           _table.RemoveRange(existing);
            return this;
        }

        public IRepository<T> SaveChanges()
        {
            _db.SaveChanges();
            return this;
        }

        internal ICMDBContext DbContext { get { return _db; } }
    }
}
