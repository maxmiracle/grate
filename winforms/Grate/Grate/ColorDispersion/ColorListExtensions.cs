using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate.ColorDispersion
{
    public static class ColorListExtensions
    {
        public static T ArgMin<T, TResult>(this List<T> list, Func<T, TResult> func, out TResult min) where TResult : IComparable<TResult>
        {
            IEnumerator<T> listEnumerator =  list.GetEnumerator();
            listEnumerator.Reset();
            listEnumerator.MoveNext();
            T argMin = listEnumerator.Current;
            min = func( argMin );
            while ( listEnumerator.MoveNext())
            {
                T arg = listEnumerator.Current;
                TResult res = func( arg );
                if (res.CompareTo( min ) < 0)
                {
                    argMin = arg;
                    min = res;
                }
            }
            return argMin;
        }

    }

    public static class ColorEnumerableExtensions
    {
        public static T ArgMin<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> func, out TResult min) where TResult : IComparable<TResult>
        {
            IEnumerator<T> listEnumerator = enumerable.GetEnumerator();
            listEnumerator.Reset();
            listEnumerator.MoveNext();
            T argMin = listEnumerator.Current;
            min = func(argMin);
            while (listEnumerator.MoveNext())
            {
                T arg = listEnumerator.Current;
                TResult res = func(arg);
                if (res.CompareTo(min) < 0)
                {
                    argMin = arg;
                    min = res;
                }
            }
            return argMin;
        }
    }
}
