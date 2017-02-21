using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Monopoly.Tests
{
    public class PlayerTests : BaseTests
    {
        private Board _board;
        private uint _initialSpace;
        private Player _player;
        private IRoll _roll;

        [SetUp]
        public void SetUp()
        {
            _board = Fixture.Freeze<Board>();
            _initialSpace = (uint)Fixture.CreateInRange(0, (int)_board.HighestPossibleSpace);

            _player = Fixture.Create<Player>();
            _player.MoveToSpace(_initialSpace);

            _roll = Fixture.Create<IRoll>();
        }

        [Test]
        public void CurrentSpace_GivenNoTurns_ReturnsInitialSpace()
        {
            var currentSpace = _player.CurrentSpace;

            Assert.That(currentSpace, Is.EqualTo(_initialSpace));
        }

        [Test]
        public void CurrentSpace_GivenPlayerWhoTakesATurnThatDoesNotCrossEndOfBoard_ReturnsInitialSpacePlusRollValue()
        {
            GivenPlayerWhoWillNotCrossEndOfBoardOnNextTurn(_roll.Value);

            _player.MoveSpaces(_roll.Value, _board);
            var finalSpace = _player.CurrentSpace;

            Assert.That(finalSpace, Is.EqualTo(_initialSpace + _roll.Value));
        }

        [Test]
        public void CurrentSpace_GivenPlayerWhoTakesATurnThatDoesCrossEndOfBoard_ReturnsSumOfInitialSpaceAndRollValueModuloTheNumberOfSpacesOnTheBoard()
        {
            GivenPlayerWhoWillCrossEndOfBoardOnNextTurn(_roll.Value);
            var expectedLandingSpace = (_initialSpace + _roll.Value) % _board.TotalSpaces;

            _player.MoveSpaces(_roll.Value, _board);
            var finalSpace = _player.CurrentSpace;

            Assert.That(finalSpace, Is.EqualTo(expectedLandingSpace));
        }

        private void GivenPlayerWhoWillCrossEndOfBoardOnNextTurn(uint rollValue)
        {
            _initialSpace = GivenLocationWithinXSpacesOfEndOfBoard(rollValue);
            _player.MoveToSpace(_initialSpace);
        }

        private uint GivenLocationWithinXSpacesOfEndOfBoard(uint numberOfSpaces)
        {
            var lowestAllowableSpace = _board.TotalSpaces - numberOfSpaces;
            return (uint)Fixture.CreateInRange((int)lowestAllowableSpace, (int)_board.HighestPossibleSpace);
        }

        private void GivenPlayerWhoWillNotCrossEndOfBoardOnNextTurn(uint rollValue)
        {
            _initialSpace = GivenLocationAtLeastXSpacesFromEndOfBoard(rollValue);
            _player.MoveToSpace(_initialSpace);
        }

        private uint GivenLocationAtLeastXSpacesFromEndOfBoard(uint numberOfSpaces)
        {
            var highestAllowableSpace = _board.HighestPossibleSpace - numberOfSpaces;
            return (uint)Fixture.CreateInRange(0, (int)highestAllowableSpace);
        }

        [Test]
        public void TakeATurn_RollsDiceAndMovesPlayer()
        {
            var mockDice = GivenTwoStandardSixSidedDice();

            _player.TakeATurn(mockDice.Object, _board);
            var finalSpace = _player.CurrentSpace;

            mockDice.Verify(d => d.Roll());
            AssertPlayerMovedTwoToTwelveSpaces(finalSpace);
        }

        private Mock<IDice> GivenTwoStandardSixSidedDice()
        {
            var mockDice = Fixture.Create<Mock<IDice>>();
            mockDice.Setup(d => d.Roll()).Returns(_roll);
            return mockDice;
        }

        private void AssertPlayerMovedTwoToTwelveSpaces(uint finalSpace)
        {
            if (_initialSpace + 12 <= _board.HighestPossibleSpace)
            {
                Assert.That(finalSpace, Is.AtLeast(_initialSpace + 2).And.AtMost(_initialSpace + 12));
            }
            else
            {
                Assert.That(
                    finalSpace,
                    Is.AtLeast(_initialSpace + 2).And.AtMost(_board.HighestPossibleSpace)
                    .Or.AtLeast(0).And.AtMost((_initialSpace + 12) % _board.TotalSpaces));
            }
        }
    }
}
