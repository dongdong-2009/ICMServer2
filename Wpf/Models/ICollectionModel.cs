using ICMServer.WPF.Collections.ObjectModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    public interface ICollectionModel<T>
    {
        ObservableRangeCollection<T> Data { get; }
        Task RefillDataAsync();
        Task InsertAsync(T obj);
        void Insert(T obj);
        Task UpdateAsync(T obj, params Expression<Func<T, object>>[] modifiedProperties);
        Task DeleteAsync(IList objs);
        void Delete(IList objs);
        bool Delete(T obj);
        void DeleteAll();
        IEnumerable<T> Select(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties);
        //int Count();
    }
}
