using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support.Extensions;

namespace Tests.Support.ExtensionTests
{
    public class EnumerableExtensionsTests : BaseTest
    {
        [Test]
        public void CartesianProduct_ReturnsAllPossibleSequencesContainingOneElementFromEachInputSequence()
        {
            var sequencesOfBytes = Fixture.CreateMany<IEnumerable<byte>>().ToArray();
            var numberOfSequences = sequencesOfBytes.Length;
            var numberOfItemsInEachSequence = sequencesOfBytes.First().Count();
            var expectedProductSetCount = (int)Math.Pow(numberOfItemsInEachSequence, numberOfSequences);

            var productSet = sequencesOfBytes.CartesianProduct().ToArray();

            AssertYieldsOnlyTuplesBelongingInProductSet(productSet, sequencesOfBytes);
            AssertYieldsAllTuplesBelongingInProductSetExactlyOnce(productSet, expectedProductSetCount);
        }

        private static void AssertYieldsAllTuplesBelongingInProductSetExactlyOnce(
            IEnumerable<IEnumerable<byte>> productSet, int expectedProductSetCount)
        {
            Assert.That(productSet, Has.Exactly(expectedProductSetCount).Items);
            Assert.That(productSet, Is.Unique);
        }

        private static void AssertYieldsOnlyTuplesBelongingInProductSet<T>(
            IEnumerable<IEnumerable<T>> itemTuples, IEnumerable<IEnumerable<T>> sequences)
        {
            foreach (var itemTuple in itemTuples)
                AssertEachItemIsInCorrespondingSequence(itemTuple, sequences);
        }

        private static void AssertEachItemIsInCorrespondingSequence<T>(
            IEnumerable<T> items, IEnumerable<IEnumerable<T>> sequences)
        {
            var itemSequencePairs = items.Zip(sequences, Tuple.Create);
            foreach (var itemSequencePair in itemSequencePairs)
            {
                var item = itemSequencePair.Item1;
                var sequence = itemSequencePair.Item2;
                Assert.That(sequence, Contains.Item(item));
            }
        }
    }
}
