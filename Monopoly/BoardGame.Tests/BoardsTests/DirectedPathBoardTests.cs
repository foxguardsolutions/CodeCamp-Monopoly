using System.Collections.Generic;
using System.Linq;

using BoardGame.Boards;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.BoardsTests
{
    public class DirectedPathBoardTests : BaseTest
    {
        protected DirectedPathBoard Board { get; set; }

        protected IList<ISpace> Spaces { get; private set; }
        protected ISpace InitialSpace { get; private set; }

        protected uint InitialSpaceIndex { get; private set; }
        protected uint TotalSpaces { get; private set; }
        protected uint HighestSpaceIndex { get; private set; }

        [SetUp]
        public void SetUp()
        {
            Spaces = GivenSpaces();
            InitialSpace = Fixture.SelectFrom(Spaces);

            InitialSpaceIndex = (uint)Spaces.IndexOf(InitialSpace);
            TotalSpaces = (uint)Spaces.Count;
            HighestSpaceIndex = TotalSpaces - 1;

            GivenBoard();
        }

        private IList<ISpace> GivenSpaces()
        {
            var spaces = Fixture.CreateMany<ISpace>();
            Fixture.Register(() => spaces);
            return spaces.ToList();
        }

        protected virtual void GivenBoard()
        {
            Board = Fixture.Create<DirectedPathBoard>();
        }

        [Test]
        public void GetOffsetSpace_GivenOffsetThatLandsOnBoard_ReturnsSpaceWithIndexEqualToSumOfInitialSpaceAndOffset()
        {
            var offset = GivenOffsetThatLandsOnBoard();

            var nextSpace = Board.GetOffsetSpace(InitialSpace, offset);

            Assert.That(Spaces.IndexOf(nextSpace), Is.EqualTo(InitialSpaceIndex + offset));
        }

        private int GivenOffsetThatLandsOnBoard()
        {
            var lowestAllowableOffset = -(int)InitialSpaceIndex;
            var highestAllowableOffset = (int)(HighestSpaceIndex - InitialSpaceIndex);
            return Fixture.CreateInRange(lowestAllowableOffset, highestAllowableOffset);
        }
    }
}
