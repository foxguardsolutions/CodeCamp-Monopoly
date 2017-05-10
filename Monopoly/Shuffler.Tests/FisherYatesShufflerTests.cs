using System;
using System.Linq;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace Shuffler.Tests
{
    public class FisherYatesShufflerTests : BaseTest
    {
        private Mock<Random> _mockRandom;
        private FisherYatesShuffler _shuffler;

        [SetUp]
        public void SetUp()
        {
            _mockRandom = Fixture.Mock<Random>();
            _shuffler = Fixture.Create<FisherYatesShuffler>();
        }

        [Test]
        public void Shuffle_YieldsAllElementsOfOriginalSequence()
        {
            var sequence = Fixture.CreateMany<int>().ToList();
            SetUpRandomBehavior(sequence.Count);

            var shuffledSequence = _shuffler.Shuffle(sequence);

            Assert.That(shuffledSequence, Is.EquivalentTo(sequence));
        }

        private void SetUpRandomBehavior(int sequenceCount)
        {
            _mockRandom.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(sequenceCount - 1);
        }

        [Test]
        public void Shuffle_YieldsElementsInOrderPrescribedByRandomNumberGenerator()
        {
            var sequence = Fixture.CreateMany<int>().ToList();
            var indexOfFirstItemToBeSelectedByRandomNumberGenerator = GivenFirstNumberToBeGeneratedAtRandom(sequence.Count);

            var firstShuffledItem = _shuffler.Shuffle(sequence).First();

            Assert.That(firstShuffledItem, Is.EqualTo(sequence[indexOfFirstItemToBeSelectedByRandomNumberGenerator]));
        }

        private int GivenFirstNumberToBeGeneratedAtRandom(int sequenceCount)
        {
            var index = Fixture.CreateInRange(0, sequenceCount - 1);
            _mockRandom.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(index);
            return index;
        }
    }
}
