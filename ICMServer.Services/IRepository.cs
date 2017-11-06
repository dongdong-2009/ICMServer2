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
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> SelectAll(params Expression<Func<T, object>>[] navigationProperties);
        IEnumerable<T> Select(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties);
        IRepository<T> Insert(T obj);
        IRepository<T> Update(T obj, params Expression<Func<T, object>>[] modifiedProperties);
        IRepository<T> Delete(Func<T, bool> predicate);
        IRepository<T> Delete(T obj);
        IRepository<T> DeleteAll();
        IRepository<T> SaveChanges();
    }
}
