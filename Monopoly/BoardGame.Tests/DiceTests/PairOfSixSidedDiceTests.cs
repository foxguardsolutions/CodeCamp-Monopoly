using System.Collections.Generic;
using System.Linq;
using BoardGame.Dice;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shuffler;
using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.DiceTests
{
    public class PairOfSixSidedDiceTests : BaseTest
    {
        private PairOfSixSidedDice _dice;

        private Mock<IShuffler> _mockShuffler;

        [SetUp]
        public void SetUp()
        {
            _mockShuffler = GivenMockShuffler();
            _dice = Fixture.Create<PairOfSixSidedDice>();
        }

        private Mock<IShuffler> GivenMockShuffler()
        {
            var mockShuffler = Fixture.Mock<IShuffler>();
            var fisherYatesShuffler = Fixture.Create<FisherYatesShuffler>();
            mockShuffler.Setup(s => s.Shuffle(It.IsAny<IEnumerable<int>>()))
                .Returns((IEnumerable<int> source) => fisherYatesShuffler.Shuffle(source));
            return mockShuffler;
        }

        [Test]
        public void Roll_ReturnsAValueInTheRollRangeTwoToTwelve()
        {
            var roll = _dice.Roll();

            Assert.That(roll, Has.Property(nameof(IRoll.Value)).InRange(2, 12));
        }

        [Test]
        public void Roll_ShufflesDieTwice()
        {
            _dice.Roll();

            _mockShuffler.Verify(s => s.Shuffle(Range(1, 6)), Times.Exactly(2));
        }

        private static IEnumerable<int> Range(int start, int count)
        {
            return Match.Create<IEnumerable<int>>(
                collection => MatchesRange(collection, start, count));
        }

        private static bool MatchesRange(IEnumerable<int> collection, int start, int count)
        {
            var range = Enumerable.Range(start, count);
            return range.IsSubsetOf(collection) && collection.IsSubsetOf(range);
        }
    }
}
