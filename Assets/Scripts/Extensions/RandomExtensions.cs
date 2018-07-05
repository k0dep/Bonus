using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Extensions
{
    public static class ConnectionRandomExtensions
    {
        public static T Random<T>(this IEnumerable<T> source)
        {    
            var count = source.Count();
            if (count == 0)
            {
                return default(T);
            }

            return source.ElementAt(UnityEngine.Random.Range(0, count));
        }
        
        public static T Random<T>(this IEnumerable<T> source, T exclude)
        {
            var count = source.Count();

            if (count == 0)
            {
                return default(T);
            }

            if (exclude != null && (count == 1 && !Equals(source.First(), exclude)))
            {
                return source.First();
            }

            var random = default(T);
            do
            {
                random = source.Random();
            } while (exclude != null && random != null && Equals(random, exclude));

            return random;
        }

        public static IEnumerable<T> RandomRange<T>(this IEnumerable<T> source, uint count)
        {
            Assert.IsTrue(source.Count() >= count);
            
            var result = new HashSet<T>();

            while (result.Count < count)
            {
                result.Add(source.Random());
            }

            return result;
        }
    }
}