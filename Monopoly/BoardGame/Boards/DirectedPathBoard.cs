using System.Collections.Generic;
using System.Linq;

namespace BoardGame.Boards
{
    public abstract class DirectedPathBoard : IBoard
    {
        protected uint TotalSpaces => (uint)_spaces.Count;
        private readonly IList<Space> _spaces;

        protected DirectedPathBoard(IEnumerable<Space> spaces)
        {
            _spaces = spaces.ToList();
        }

        public Space GetOffsetSpace(Space initialSpace, int offset)
        {
            var initialSpaceIndex = _spaces.IndexOf(initialSpace);
            var finalSpaceIndex = GetOffsetSpaceIndex((uint)initialSpaceIndex, offset);
            return _spaces[(int)finalSpaceIndex];
        }

        private uint GetOffsetSpaceIndex(uint initialSpaceIndex, int offset)
        {
            if (CrossesBeginningOfBoard(initialSpaceIndex, offset) || CrossesEndOfBoard(initialSpaceIndex, offset))
                return GetSpaceIndexWhenCrossingBoardBoundary(initialSpaceIndex, offset);
            return (uint)(initialSpaceIndex + offset);
        }

        private static bool CrossesBeginningOfBoard(uint initialSpaceIndex, int offset)
        {
            return -offset > initialSpaceIndex;
        }

        private bool CrossesEndOfBoard(uint initialSpaceIndex, int offset)
        {
            return initialSpaceIndex + offset >= TotalSpaces;
        }

        protected abstract uint GetSpaceIndexWhenCrossingBoardBoundary(uint initialSpaceIndex, int offset);
    }
}
