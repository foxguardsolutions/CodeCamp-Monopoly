using BoardGame.Boards;
using BoardGame.Extensions;

using NUnit.Framework;
using Ploeh.AutoFixture;

namespace BoardGame.Tests.BoardsTests
{
    public class DirectedCycleBoardTests : DirectedPathBoardTests
    {
        protected override void GivenBoard()
        {
            Board = Fixture.Create<DirectedCycleBoard>();
        }

        [Test]
        public void GetOffsetSpace_GivenOffsetThatLandsBeforeLowestSpaceOnBoard_ReturnsSpaceAtWrappedIndex()
        {
            var offset = GivenOffsetThatLandsBeforeLowestSpaceOnBoard(InitialSpaceIndex);
            var expectedFinalSpaceIndex = (InitialSpaceIndex + offset).Modulo(TotalSpaces);

            var nextSpace = Board.GetOffsetSpace(InitialSpace, offset);

            Assert.That(Spaces.IndexOf(nextSpace), Is.EqualTo(expectedFinalSpaceIndex));
        }

        private int GivenOffsetThatLandsBeforeLowestSpaceOnBoard(uint initialSpace)
        {
            var offsetWithMagnitudeGreaterThanInitialSpaceNumber = initialSpace + Fixture.Create<int>();
            return (int)-offsetWithMagnitudeGreaterThanInitialSpaceNumber;
        }

        [Test]
        public void GetOffsetSpace_GivenOffsetThatLandsAfterHighestSpaceOnBoard_ReturnsSpaceAtWrappedIndex()
        {
            var offset = GivenOffsetThatLandsAfterHighestSpaceOnBoard();
            var expectedFinalSpaceIndex = (InitialSpaceIndex + offset).Modulo(TotalSpaces);

            var nextSpace = Board.GetOffsetSpace(InitialSpace, offset);

            Assert.That(Spaces.IndexOf(nextSpace), Is.EqualTo(expectedFinalSpaceIndex));
        }

        [Test]
        public void GetOffsetSpace_GivenOffsetThatLandsAfterHighestSpaceOnBoard_RaisesCrossedEndOfBoardEventForEachLapCompleted()
        {
            var offset = GivenOffsetThatLandsAfterHighestSpaceOnBoard();
            var lapsCompleted = 0;
            Board.CrossedEndOfBoard += (sender, args) => lapsCompleted++;
            var expectedLapsCompleted = (InitialSpaceIndex + offset) / TotalSpaces;

            Board.GetOffsetSpace(InitialSpace, offset);

            Assert.That(lapsCompleted, Is.EqualTo(expectedLapsCompleted));
        }

        private int GivenOffsetThatLandsAfterHighestSpaceOnBoard()
        {
            var largestOffsetNotCrossingEndOfBoard = HighestSpaceIndex - InitialSpaceIndex;
            var offsetThatCrossesEndOfBoard = largestOffsetNotCrossingEndOfBoard + Fixture.Create<int>();
            return (int)offsetThatCrossesEndOfBoard;
        }
    }
}
