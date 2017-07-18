using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jongbloed.SelectMoreThings
{
    public struct Maybe<T>
        where T : class
    {
        private readonly bool _hasValue;
        private readonly T _value;
        private Maybe(bool hasValue, T value)
        {
            _hasValue = hasValue;
            _value = value;
        }
        public T WithDefault(T @default)
        {
            return _hasValue ? _value : @default;
        }
        public static Maybe<T> From(T value)
        {
            return new Maybe<T>(value == null, value);
        }
        public Maybe<T1> Map<T1>(Func<T,T1> transform)
            where T1 : class
        {
            if(_hasValue)
            {
                var newValue = transform(_value);
                return new Maybe<T1>(newValue != null, newValue);
            }
            return new Maybe<T1>(false, null);
        }
        public Maybe<T2> Map<T1,T2>(Func<T, T1> transform1, Func<T1,T2> transform2)
            where T1 : class
            where T2 : class
        {
            if (_hasValue)
            {
                var newValue = transform1(_value);
                if(newValue != null)
                {
                    var newerValue = transform2(newValue);
                    return new Maybe<T2>(newerValue != null, newerValue);
                }
            }
            return new Maybe<T2>(false, null);
        }
    }

    public static class DoubleEnumerable
    {


        public static Tuple<T1, T2> T<T1, T2>(T1 i1, T2 i2) => Tuple.Create(i1, i2);
        public static IEnumerable<Tuple<TOut1,TOut2>> Where<TIn1,TIn2,TOut1,TOut2>(this IEnumerable<Tuple<TIn1, TIn2>> source, Func<TIn1,TOut1> transformLeft, Func<TIn2,TOut2> transformRight)
        {
            if (null == source) throw new NullReferenceException();

            return source.Select(tuple2 => T(transformLeft(tuple2.Item1), transformRight(tuple2.Item2)));
        }
        public static Tuple<IEnumerable<T1>,IEnumerable<T2>> Branch<T1,T2>(this IEnumerable<Tuple<T1, T2>> source, Func<T1,bool> filterLeft, Func<T2,bool> filterRight)
        {
            if (null == source) throw new NullReferenceException();

            return T(source.Select(tuple2 => tuple2.Item1).Where(filterLeft),
                     source.Select(tuple2 => tuple2.Item2).Where(filterRight));
        }
        public static IEnumerable<Tuple<T1,T2>> Filter<T1, T2>(this IEnumerable<Tuple<T1, T2>> source, Func<T1, bool> filterLeft, Func<T2, bool> filterRight)
        {
            if (null == source) throw new NullReferenceException();

            return source.Where(tuple2 => filterLeft(tuple2.Item1) && filterRight(tuple2.Item2));
        }

        public static IEnumerable<TResult> Zip<T1,T2,TResult>(this Tuple<IEnumerable<T1>,IEnumerable<T2>> source, Func<T1,T2,TResult> merge)
        {
            if (null == source) throw new NullReferenceException();

            var enumerateLeft = source.Item1.GetEnumerator();
            var enumerateRight = source.Item2.GetEnumerator();
            while (enumerateLeft.MoveNext() && enumerateRight.MoveNext())
                yield return merge(enumerateLeft.Current, enumerateRight.Current);
            yield break;
        }
        public static IEnumerable<Tuple<T1,T2>> Join<T1,T2,TKey>(this Tuple<IEnumerable<T1>, IEnumerable<T2>> source, Func<T1,TKey> on, Func<T2,TKey> equals)
        {
            return source.Item1.Join(source.Item2, on, equals, (i1, i2) => T(i1, i2));
        }
    }
}
