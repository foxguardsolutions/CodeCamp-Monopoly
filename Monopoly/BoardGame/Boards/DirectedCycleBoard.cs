using System.Collections.Generic;
using BoardGame.Extensions;

namespace BoardGame.Boards
{
    public class DirectedCycleBoard : DirectedPathBoard
    {
        public DirectedCycleBoard(IEnumerable<Space> spaces)
            : base(spaces)
        {
        }

        protected override uint GetSpaceIndexWhenCrossingBoardBoundary(uint initialSpaceIndex, int offset)
        {
            return (uint)(initialSpaceIndex + offset).Modulo(TotalSpaces);
        }
    }
}
