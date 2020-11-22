using System;
using System.Collections.Generic;

namespace MyLinqLib
{
    public static class MyLinqExtensions
    {
        #region ElementSelection
        public static T First<T>(this IEnumerable<T> source)
        {
            return source.ElementAt(0);
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> source)
        {
            try
            {
                return First(source);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T First<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            foreach (var element in source)
            {
                if (predicate(element)) return element;
            }
            throw new InvalidOperationException("No element satisfies the condition in predicate");
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            try
            {
                return First(source, predicate);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T Last<T>(this IEnumerable<T> source) => source.ElementAt(source.Count() - 1);

        public static T LastOrDefault<T>(this IEnumerable<T> source)
        {
            try
            {
                return Last(source);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T Last<T>(this IEnumerable<T> source, Predicate<T> predicate) => source.Where(predicate).ElementAt(source.Count() - 1);

        public static T LastOrDefault<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            try
            {
                return Last(source, predicate);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T Single<T>(this IEnumerable<T> source)
        {
            if (source.Count() != 1)
                throw new InvalidOperationException("The collection does not contain exactly one element.");

            return source.ElementAt(0);
        }

        public static T SingleOrDefault<T>(this IEnumerable<T> source)
        {
            try
            {
                return Single(source);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T Single<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            var filtered = source.Where(predicate);

            if (filtered.Count() != 1)
                throw new InvalidOperationException("The collection does not contain exactly one element.");

            return filtered.ElementAt(0);
        }

        public static T SingleOrDefault<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            try
            {
                return Single(source, predicate);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T ElementAt<T>(this IEnumerable<T> source, int index) => new List<T>(source)[index];

        public static T ElementAtOrDefault<T>(this IEnumerable<T> source, int index)
        {
            try
            {
                return source.ElementAt(index);
            }
            catch (Exception)
            {
                return default;
            }
        }
        #endregion ElementSelection

        #region Aggregate
        public static double Average(this IEnumerable<int> source) => source.Sum() / source.Count();

        public static double Average(this IEnumerable<double> source) => source.Sum() / source.Count();

        public static double Average<T>(this IEnumerable<T> source, Func<T, double> func) => source.Select(func).Sum() / source.Count();

        public static int Count<T>(this IEnumerable<T> source, Predicate<T> predicate) => source.Where(predicate).Count();

        public static int Count<T>(this IEnumerable<T> source) => new List<T>(source).Count;

        public static int Sum(this IEnumerable<int> source)
        {
            int sum = 0;
            foreach (var val in source)
            {
                sum += val;
            }
            return sum;
        }

        public static double Sum(this IEnumerable<double> source)
        {
            double sum = 0;
            foreach (var val in source)
            {
                sum += val;
            }
            return sum;
        }

        public static int Min(this IEnumerable<int> source)
        {
            int min = int.MaxValue;
            foreach (var val in source)
            {
                if (val < min) min = val;
            }
            return min;
        }

        public static double Min(this IEnumerable<double> source)
        {
            double min = double.MaxValue;
            foreach (var val in source)
            {
                if (val < min) min = val;
            }
            return min;
        }

        public static int Max(this IEnumerable<int> source)
        {
            int max = int.MinValue;
            foreach (var val in source)
            {
                if (val > max) max = val;
            }
            return max;
        }

        public static double Max(this IEnumerable<double> source)
        {
            double max = double.MinValue;
            foreach (var val in source)
            {
                if (val > max) max = val;
            }
            return max;
        }
        #endregion Aggregate

        #region Filter
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            var ret = new List<T>();
            foreach (var element in source)
            {
                if (predicate(element)) ret.Add(element);
            }
            return ret;
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source) => new HashSet<T>(source);

        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, int count)
        {
            var ret = new List<T>();
            for (int i = 0; i < Math.Min(count, source.Count()); i++)
            {
                ret.Add(source.ElementAt(i));
            }
            return ret;
        }

        public static IEnumerable<T> TakeWhile<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            var ret = new List<T>();
            foreach (var element in source)
            {
                if (!predicate(element)) break;
                ret.Add(element);
            }
            return ret;
        }

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, int count)
        {
            var ret = new List<T>();
            for (int i = 0; i < source.Count(); i++)
            {
                if (i < count) continue;
                ret.Add(source.ElementAt(i));
            }
            return ret;
        }

        public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            var ret = new List<T>();
            foreach (var element in source)
            {
                if (predicate(element)) continue;
                ret.Add(element);
            }
            return ret;
        }
        #endregion Filter

        #region Projection
        public static IEnumerable<Tnew> Select<Told, Tnew>(this IEnumerable<Told> source, Func<Told, Tnew> transform)
        {
            var ret = new List<Tnew>();
            foreach (var element in source)
            {
                ret.Add(transform(element));
            }
            return ret;
        }
        #endregion Projection

        #region Sort
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, Func<T, double> comparer)
        {
            var sorted = new List<T>(source);
            for (int i = 1; i < sorted.Count(); i++)
            {
                var sortValue = sorted.ElementAt(i);
                int j = i;
                while ((j > 0) && (comparer(sorted[j - 1]) > comparer(sortValue)))
                {
                    sorted[j] = sorted[j - 1];
                    j -= 1;
                }
                sorted[j] = sortValue;
            }
            return sorted;
        }

        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> source, Func<T, double> comparer) => source.OrderBy(element => -1.0 * comparer(element));

        public static IEnumerable<T> Revert<T>(this IEnumerable<T> source)
        {
            var ret = new List<T>();
            for (int i = source.Count() - 1; i >= 0; i--)
            {
                ret.Add(source.ElementAt(i));
            }
            return ret;
        }
        #endregion Sort
    }
}
