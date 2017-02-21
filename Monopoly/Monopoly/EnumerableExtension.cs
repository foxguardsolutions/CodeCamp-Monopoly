using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public static class EnumerableExtension
    {
        // http://stackoverflow.com/a/1653204/3697120
        public static IEnumerable<T> FisherYatesShuffle<T>(this IEnumerable<T> source, IRandom randomNumberGenerator)
        {
            var buffer = source.ToList();
            for (var i = 0; i < buffer.Count; i++)
            {
                var j = randomNumberGenerator.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        // https://blogs.msdn.microsoft.com/ericlippert/2010/06/28/computing-a-cartesian-product-with-linq/
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            var emptyProduct = new[] { Enumerable.Empty<T>() } as IEnumerable<IEnumerable<T>>;
            var product = sequences.Aggregate(
              emptyProduct,
              (accumulator, sequence) =>
                from accseq in accumulator
                from item in sequence
                select accseq.Concat(new[] { item }));

            return product;
        }
    }
}
