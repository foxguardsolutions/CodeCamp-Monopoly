using System.Collections.Generic;
using System.Linq;

namespace Tests.Support.Extensions
{
    public static class EnumerableExtensions
    {
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

        public static bool IsSubsetOf<T>(this IEnumerable<T> sourceCollection, IEnumerable<T> otherCollection)
        {
            if (sourceCollection.Any(value => !otherCollection.Contains(value)))
                return false;
            return true;
        }
    }
}
