using System;
using System.Collections.Generic;

namespace ICMServer.WPF.Collections.ObjectModel
{
    public static class IEnumerableExtension
    {
        /// <summary>
        /// To do some "action" for each element in a enumerable list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">enumberable list</param>
        /// <param name="action">the action that will be done for each element</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
                return;

            foreach (var cur in enumerable)
            {
                action(cur);
            }
        }
    }
}
