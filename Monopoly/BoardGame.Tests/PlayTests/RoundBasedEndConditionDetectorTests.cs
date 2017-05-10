using System;
using BoardGame.Play;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.PlayTests
{
    public class RoundBasedEndConditionDetectorTests : BaseTest
    {
        private uint _roundLimit;
        private Mock<IPlayCoordinator> _mockPlayCoordinator;
        private RoundBasedEndConditionDetector _endConditionDetector;

        [SetUp]
        public void SetUp()
        {
            _mockPlayCoordinator = Fixture.Mock<IPlayCoordinator>();

            _roundLimit = Fixture.Create<uint>();
            _endConditionDetector = new RoundBasedEndConditionDetector(_roundLimit);
            _endConditionDetector.Subscribe(_mockPlayCoordinator.Object);
        }

        [Test]
        public void IsFinished_GivenRoundsPlayedEqualToGameRoundLimit_ReturnsTrue()
        {
            PlayRounds(_roundLimit);

            Assert.That(_endConditionDetector.IsInEndState(), Is.True);
        }

        [Test]
        public void IsFinished_GivenFewerRoundsPlayedThanGameRoundLimit_ReturnsFalse()
        {
            var numberLowerThanRoundLimit = GetValueLessThan(_roundLimit);
            PlayRounds(numberLowerThanRoundLimit);

            Assert.That(_endConditionDetector.IsInEndState(), Is.False);
        }

        private void PlayRounds(uint roundsToPlay)
        {
            for (var i = 0; i < roundsToPlay; i++)
                _mockPlayCoordinator.Raise(p => p.RoundCompleted += null, EventArgs.Empty);
        }

        private uint GetValueLessThan(uint upperBound)
        {
            return Fixture.Create<uint>() % upperBound;
        }
    }
}
