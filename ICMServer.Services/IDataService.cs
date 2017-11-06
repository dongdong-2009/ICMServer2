using ICMServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    /// <summary>
    /// The Interface defining methods for Create Employee and Read All Employees  
    /// </summary>
    public interface IDataService<T> where T : class
    {
        IEnumerable<T> SelectAll(params Expression<Func<T, object>>[] navigationProperties);
        IEnumerable<T> Select(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties);
        void Insert(T obj);
        void Update(T obj, params Expression<Func<T, object>>[] modifiedProperties);
        void Update(IEnumerable<T> objs, params Expression<Func<T, object>>[] modifiedProperties);
        void Delete(Func<T, bool> predicate);
        void Delete(T obj);
        void DeleteAll();

        Task<IEnumerable<T>> SelectAllAsync();
        Task<IEnumerable<T>> SelectAsync(Func<T, bool> predicate);
        Task InsertAsync(T obj);
        Task UpdateAsync(T obj, params Expression<Func<T, object>>[] modifiedProperties);
        Task UpdateAsync(IEnumerable<T> objs, params Expression<Func<T, object>>[] modifiedProperties);
        Task DeleteAsync(Func<T, bool> predicate);
        Task DeleteAsync(T obj);
        Task DeleteAllAsync();
    }
}
