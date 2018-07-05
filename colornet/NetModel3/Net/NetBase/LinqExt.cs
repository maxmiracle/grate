namespace NetBase
{

    using System;
    using System.Collections.Generic;


    public static class LinqExt
    {

        public static TSrc ArgMax<TSrc, TArg>(this IEnumerable<TSrc> ie, Converter<TSrc, TArg> fn)
            where TArg : IComparable<TArg>
        {
            IEnumerator<TSrc> e = ie.GetEnumerator();
            if (!e.MoveNext())
                throw new InvalidOperationException("Sequence has no elements.");

            TSrc t_try, t = e.Current;
            if (!e.MoveNext())
                return t;

            TArg v, max_val = fn(t);
            do
            {
                if ((v = fn(t_try = e.Current)).CompareTo(max_val) > 0)
                {
                    t = t_try;
                    max_val = v;
                }
            }
            while (e.MoveNext());
            return t;
        }

        public static TSrc ArgMin<TSrc, TArg>(this IEnumerable<TSrc> ie, Converter<TSrc, TArg> fn)
            where TArg : IComparable<TArg>
        {
            IEnumerator<TSrc> e = ie.GetEnumerator();
            if (!e.MoveNext())
                throw new InvalidOperationException("Sequence has no elements.");

            TSrc t_try, t = e.Current;
            if (!e.MoveNext())
                return t;

            TArg v, min_val = fn(t);
            do
            {
                if ((v = fn(t_try = e.Current)).CompareTo(min_val) < 0)
                {
                    t = t_try;
                    min_val = v;
                }
            }
            while (e.MoveNext());
            return t;
        }

        public static int IndexOfMax<TSrc, TArg>(this IEnumerable<TSrc> ie, Converter<TSrc, TArg> fn)
            where TArg : IComparable<TArg>
        {
            IEnumerator<TSrc> e = ie.GetEnumerator();
            if (!e.MoveNext())
                return -1;

            int max_ix = 0;
            TSrc t = e.Current;
            if (!e.MoveNext())
                return max_ix;

            TArg tx, max_val = fn(t);
            int i = 1;
            do
            {
                if ((tx = fn(e.Current)).CompareTo(max_val) > 0)
                {
                    max_val = tx;
                    max_ix = i;
                }
                i++;
            }
            while (e.MoveNext());
            return max_ix;
        }

        public static int IndexOfMin<TSrc, TArg>(this IEnumerable<TSrc> ie, Converter<TSrc, TArg> fn)
            where TArg : IComparable<TArg>
        {
            IEnumerator<TSrc> e = ie.GetEnumerator();
            if (!e.MoveNext())
                return -1;

            int min_ix = 0;
            TSrc t = e.Current;
            if (!e.MoveNext())
                return min_ix;

            TArg tx, min_val = fn(t);
            int i = 1;
            do
            {
                if ((tx = fn(e.Current)).CompareTo(min_val) < 0)
                {
                    min_val = tx;
                    min_ix = i;
                }
                i++;
            }
            while (e.MoveNext());
            return min_ix;
        }
    }
}