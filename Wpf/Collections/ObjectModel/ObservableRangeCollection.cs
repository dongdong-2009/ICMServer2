using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ICMServer.WPF.Collections.ObjectModel
{
    /// <summary> 
    /// Represents a dynamic data collection that provides notifications when items get added, 
    /// removed, or when the whole list is refreshed. 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        //private readonly object locker = new object();

        ///// <summary>
        ///// This private variable holds the flag to
        ///// turn on and off the collection changed notification.
        ///// </summary>
        //private bool suspendCollectionChangeNotification = false;

        /// <summary> 
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class. 
        /// </summary> 
        public ObservableRangeCollection()
            : base()
        {
        }

        /// <summary> 
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class that contains elements copied from the specified collection. 
        /// </summary> 
        /// <param name="collection">collection: The collection from which the elements are copied.</param> 
        /// <exception cref="System.ArgumentNullException">The collection parameter cannot be null.</exception> 
        public ObservableRangeCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        ///// <summary>
        ///// This event is overriden CollectionChanged event of the observable collection.
        ///// </summary>
        //public override event NotifyCollectionChangedEventHandler CollectionChanged;

        ////// <summary>
        ///// This method adds the given generic list of items
        ///// as a range into current collection by casting them as type T.
        ///// It then notifies once after all items are added.
        ///// </summary>
        ///// <param name="items">The source collection.</param>
        //public void AddRange(IEnumerable<T> items)
        //{
        //    lock (locker)
        //    {
        //        this.SuspendCollectionChangeNotification();
        //        foreach (var i in items)
        //        {
        //            InsertItem(Count, i);
        //        }
        //        this.NotifyChanges();
        //    }
        //}

        ///// <summary>
        ///// Raises collection change event.
        ///// </summary>
        //public void NotifyChanges()
        //{
        //    this.ResumeCollectionChangeNotification();
        //    var arg
        //         = new NotifyCollectionChangedEventArgs
        //              (NotifyCollectionChangedAction.Reset);
        //    this.OnCollectionChanged(arg);
        //}

        ///// <summary>
        ///// This method removes the given generic list of items as a range
        ///// into current collection by casting them as type T.
        ///// It then notifies once after all items are removed.
        ///// </summary>
        ///// <param name="items">The source collection.</param>
        //public void RemoveRange(IEnumerable<T> items)
        //{
        //    lock (locker)
        //    {
        //        this.SuspendCollectionChangeNotification();
        //        foreach (var i in items)
        //        {
        //            Remove(i);
        //        }
        //        this.NotifyChanges();
        //    }
        //}

        ///// <summary>
        ///// Resumes collection changed notification.
        ///// </summary>
        //public void ResumeCollectionChangeNotification()
        //{
        //    this.suspendCollectionChangeNotification = false;
        //}

        ///// <summary>
        ///// Suspends collection changed notification.
        ///// </summary>
        //public void SuspendCollectionChangeNotification()
        //{
        //    this.suspendCollectionChangeNotification = true;
        //}

        ///// <summary>
        ///// This collection changed event performs thread safe event raising.
        ///// </summary>
        ///// <param name="e">The event argument.</param>
        //protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        //{
        //    // Recommended is to avoid reentry 
        //    // in collection changed event while collection
        //    // is getting changed on other thread.
        //    using (BlockReentrancy())
        //    {
        //        if (!this.suspendCollectionChangeNotification)
        //        {
        //            NotifyCollectionChangedEventHandler eventHandler =
        //                  this.CollectionChanged;
        //            if (eventHandler == null)
        //            {
        //                return;
        //            }

        //            // Walk thru invocation list.
        //            Delegate[] delegates = eventHandler.GetInvocationList();

        //            foreach
        //            (NotifyCollectionChangedEventHandler handler in delegates)
        //            {
        //                // If the subscriber is a DispatcherObject and different thread.
        //                DispatcherObject dispatcherObject
        //                     = handler.Target as DispatcherObject;

        //                if (dispatcherObject != null
        //                       && !dispatcherObject.CheckAccess())
        //                {
        //                    // Invoke handler in the target dispatcher's thread... 
        //                    // asynchronously for better responsiveness.
        //                    dispatcherObject.Dispatcher.BeginInvoke
        //                          (DispatcherPriority.DataBind, handler, this, e);
        //                }
        //                else
        //                {
        //                    // Execute handler as is.
        //                    handler(this, e);
        //                }
        //            }
        //        }
        //    }
        //}

        //public void AddRange(IEnumerable<T> items)
        //{
        //    foreach (var item in items)
        //    {
        //        Add(item);
        //    }
        //}

        //public void RemoveRange(IEnumerable<T> items)
        //{
        //    foreach (var item in items)
        //    {
        //        Remove(item);
        //    }
        //}

        /// <summary> 
        /// Clears the current collection and replaces it with the specified collection. 
        /// </summary> 
        public void ReplaceRange(IEnumerable<T> items)
        {
            //lock (locker)
            CheckReentrancy();
            {
                int oldCount = this.Items.Count();
                int newCount = items.Count();
                int i;

                //this.SuspendCollectionChangeNotification();

                if (oldCount >= newCount)
                {
                    for (i = oldCount - 1; i >= newCount; --i)
                    {
                        this.RemoveAt(i);
                    }

                    i = 0;
                    foreach (var newItem in items)
                    {
                        if (!Eextentions.AreObjectsEqual(Items[i], newItem))
                            SetItem(i, newItem);
                        i++;
                    }
                }
                else
                {
                    i = 0;
                    foreach (var newItem in items)
                    {
                        if (i < oldCount)
                        {
                            if (!Eextentions.AreObjectsEqual(Items[i], newItem))
                                SetItem(i, newItem);
                        }
                        else
                        {
                            Add(newItem);
                        }
                        i++;
                    }
                }
                //this.NotifyChanges();
            }
            //if (collection == null)
            //    throw new ArgumentNullException("collection");

            //CheckReentrancy();

            //int oldCount = Items.Count();
            //int newCount = collection.Count();
            //int i;
            //if (oldCount >= newCount)
            //{
            //    for (i = oldCount - 1; i >= newCount; --i)
            //    {
            //        this.RemoveAt(i);
            //    }

            //    i = 0;
            //    foreach (var newItem in collection)
            //    {
            //        if (!extentions.AreObjectsEqual(Items[i], newItem))
            //            SetItem(i, newItem);
            //        i++;
            //    }
            //}
            //else
            //{
            //    i = 0;
            //    foreach (var newItem in collection)
            //    {
            //        if (i < oldCount)
            //        {
            //            if (!extentions.AreObjectsEqual(Items[i], newItem))
            //                SetItem(i, newItem);
            //        }
            //        else
            //        {
            //            Add(newItem);
            //        }
            //        i++;
            //    }
            //}
        }

    }

}
