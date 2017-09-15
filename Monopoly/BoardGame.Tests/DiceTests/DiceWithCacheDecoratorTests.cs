using BoardGame.Dice;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.DiceTests
{
    public class DiceWithCacheDecoratorTests : BaseTest
    {
        private Mock<IDice> _mockDice;
        private IRoll _lastDecoratedDieRoll;

        private DiceWithCacheDecorator _diceWithCache;

        [SetUp]
        public void SetUp()
        {
            _mockDice = Fixture.Mock<IDice>();
            _mockDice.Setup(d => d.Roll()).Callback(() => _lastDecoratedDieRoll = Fixture.Create<IRoll>()).Returns(() => _lastDecoratedDieRoll);

            _diceWithCache = new DiceWithCacheDecorator(_mockDice.Object);
        }

        [Test]
        public void Roll_ReturnsRollFromDecoratedDice()
        {
            var roll = _diceWithCache.Roll();

            _mockDice.Verify(d => d.Roll());
            Assert.That(roll, Is.EqualTo(_lastDecoratedDieRoll));
        }

        [Test]
        public void GetLastRoll_GivenNoRolls_ReturnsNull()
        {
            var lastRoll = _diceWithCache.GetLastRoll();

            _mockDice.Verify(d => d.Roll(), Times.Never);
            Assert.That(lastRoll, Is.Null);
        }

        [Test]
        public void GetLastRoll_GivenOneOrMoreRolls_ReturnsTheLastRollFromDecoratedDice()
        {
            var numberOfRolls = Fixture.Create<int>();
            for (var i = 0; i < numberOfRolls; i++)
                _diceWithCache.Roll();

            var lastRoll = _diceWithCache.GetLastRoll();

            Assert.That(lastRoll, Is.EqualTo(_lastDecoratedDieRoll));
        }
    }
}
