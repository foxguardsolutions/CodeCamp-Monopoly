using System;

using BoardGame.Boards;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.BoardsTests
{
    public class LapCounterTests : BaseTest
    {
        private LapCounter _lapCounter;
        private Mock<IBoardWithEnd> _mockBoard;
        private uint _numberOfLaps;

        [SetUp]
        public void SetUp()
        {
            _mockBoard = Fixture.Mock<IBoardWithEnd>();

            _lapCounter = Fixture.Create<LapCounter>();

            _numberOfLaps = Fixture.Create<uint>();
        }

        [Test]
        public void GetLapsCompleted_ReturnsNumberOfLapsThatWereCompleted()
        {
            CompleteLaps(_numberOfLaps);

            Assert.That(_lapCounter.GetLapsCompleted(), Is.EqualTo(_numberOfLaps));
        }

        [Test]
        public void Reset_GivenLapsAlreadyCompleted_SetsLapsCompleteToZero()
        {
            CompleteLaps(_numberOfLaps);

            _lapCounter.Reset();

            Assert.That(_lapCounter.GetLapsCompleted(), Is.EqualTo(0));
        }

        private void CompleteLaps(uint numberOfLaps)
        {
            for (var lapsComplete = 0; lapsComplete < numberOfLaps; lapsComplete++)
                _mockBoard.Raise(b => b.CrossedEndOfBoard += null, EventArgs.Empty);
        }
    }
}
