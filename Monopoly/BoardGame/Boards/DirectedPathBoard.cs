using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGame.Boards
{
    public abstract class DirectedPathBoard : IBoardWithEnd
    {
        public abstract event EventHandler CrossedEndOfBoard;

        protected uint TotalSpaces => (uint)_spaces.Count;
        private readonly IList<ISpace> _spaces;

        protected DirectedPathBoard(IEnumerable<ISpace> spaces)
        {
            _spaces = spaces.ToList();
        }

        public ISpace GetOffsetSpace(ISpace initialSpace, int offset)
        {
            var initialSpaceIndex = _spaces.IndexOf(initialSpace);
            var finalSpaceIndex = GetOffsetSpaceIndex((uint)initialSpaceIndex, offset);
            return _spaces[(int)finalSpaceIndex];
        }

        private uint GetOffsetSpaceIndex(uint initialSpaceIndex, int offset)
        {
            if (CrossesBeginningOfBoard(initialSpaceIndex, offset))
                return GetSpaceIndexWhenCrossingBeginningOfBoard(initialSpaceIndex, offset);
            if (CrossesEndOfBoard(initialSpaceIndex, offset))
                return GetSpaceIndexWhenCrossingEndOfBoard(initialSpaceIndex, offset);
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

        protected abstract uint GetSpaceIndexWhenCrossingBeginningOfBoard(uint initialSpaceIndex, int offset);
        protected abstract uint GetSpaceIndexWhenCrossingEndOfBoard(uint initialSpaceIndex, int offset);
    }
}
