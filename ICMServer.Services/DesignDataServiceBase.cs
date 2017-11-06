using ICMServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public abstract class DesignDataServiceBase<T> : DataService<T> where T : class
    {
        protected List<T> _objects;

        public DesignDataServiceBase()
        {
            _objects = new List<T>();
            InitSampleData();
        }

        protected abstract void InitSampleData();

        // Read
        public override IEnumerable<T> SelectAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            return _objects; 
        }

        public override IEnumerable<T> Select(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            return _objects.Where(predicate);
        }
 
        // Create
        public override void Insert(T obj)
        {
            _objects.Add(obj);
        }

        public override void DeleteAll()
        {
            _objects.Clear();
        }

        public override void Update(T obj, params Expression<Func<T, object>>[] modifiedProperties)
        {
            int i = _objects.IndexOf(obj);
            if (i >= 0)
                _objects[i] = obj;
        }

        public override void Delete(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override void Delete(T obj)
        {
            _objects.Remove(obj);
        }
    }
}
