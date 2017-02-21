using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Monopoly.Tests
{
    public class EnumerableExtensionTests : BaseTests
    {
        [Test]
        public void FisherYatesShuffle_ReturnsSequenceWithSameElementsButInDifferentOrder()
        {
            var numberOfItemsToCreate = Fixture.Create<int>();
            var sequence = Fixture.CreateMany<int>(numberOfItemsToCreate);
            var randomNumberGenerator = Fixture.Create<IRandom>();

            var shuffledSequence = sequence.FisherYatesShuffle(randomNumberGenerator);

            AssertContainSameItemsInDifferentOrder(sequence, shuffledSequence);
        }

        private static void AssertContainSameItemsInDifferentOrder<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.That(actual, Is.EquivalentTo(expected));
            Assert.That(actual, Is.Not.EqualTo(expected));
        }

        [Test]
        public void CartesianProduct_ReturnsAllPossibleSequencesContainingOneElementFromEachInputSequence()
        {
            var smallNumberOfSequencesToCreate = (int)Fixture.CreateInRange(2, 5);
            var smallNumberOfItemsInEachSequence = (int)Fixture.CreateInRange(1, 5);
            var expectedProductSetCount = (int)Math.Pow(smallNumberOfItemsInEachSequence, smallNumberOfSequencesToCreate);
            var sequencesOfBytes = CreateSequencesOfUniqueItems<byte>(
                smallNumberOfSequencesToCreate,
                smallNumberOfItemsInEachSequence)
                .ToArray();

            var productSet = sequencesOfBytes.CartesianProduct();

            Assert.That(productSet, Has.Exactly(expectedProductSetCount).Items);
            Assert.That(productSet, Is.Unique);
            AssertEachTupleBelongsInProductSet(productSet, sequencesOfBytes);
        }

        private IEnumerable<IEnumerable<T>> CreateSequencesOfUniqueItems<T>(
            int numberOfSequencesToCreate, int numberOfItemsInEachSequence)
        {
            for (var i = 0; i < numberOfSequencesToCreate; i++)
                yield return Fixture.CreateMany<T>(numberOfItemsInEachSequence);
        }

        private void AssertEachTupleBelongsInProductSet<T>(
            IEnumerable<IEnumerable<T>> itemTuples, IEnumerable<IEnumerable<T>> sequences)
        {
            foreach (var itemTuple in itemTuples)
                AssertEachItemIsInCorrespondingSequence(itemTuple, sequences);
        }

        private void AssertEachItemIsInCorrespondingSequence<T>(
            IEnumerable<T> items, IEnumerable<IEnumerable<T>> sequences)
        {
            var itemSequencePairs = items.Zip(sequences, (i, s) => Tuple.Create(i, s));
            foreach (var itemSequencePair in itemSequencePairs)
            {
                var item = itemSequencePair.Item1;
                var sequence = itemSequencePair.Item2;
                Assert.That(sequence, Contains.Item(item));
            }
        }
    }
}
