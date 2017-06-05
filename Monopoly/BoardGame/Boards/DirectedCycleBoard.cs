using System;
using System.Collections.Generic;

using BoardGame.Extensions;

namespace BoardGame.Boards
{
    public class DirectedCycleBoard : DirectedPathBoard
    {
        public override event EventHandler CrossedEndOfBoard;

        public DirectedCycleBoard(IEnumerable<ISpace> spaces)
            : base(spaces)
        {
        }

        private uint GetSpaceIndexWhenCrossingBoardBoundary(uint initialSpaceIndex, int offset)
        {
            return (uint)(initialSpaceIndex + offset).Modulo(TotalSpaces);
        }

        protected override uint GetSpaceIndexWhenCrossingBeginningOfBoard(uint initialSpaceIndex, int offset)
        {
            return GetSpaceIndexWhenCrossingBoardBoundary(initialSpaceIndex, offset);
        }

        protected override uint GetSpaceIndexWhenCrossingEndOfBoard(uint initialSpaceIndex, int offset)
        {
            for (var laps = 0; laps < LapsCompleted(initialSpaceIndex, offset); laps++)
                CrossedEndOfBoard?.Invoke(this, EventArgs.Empty);
            return GetSpaceIndexWhenCrossingBoardBoundary(initialSpaceIndex, offset);
        }

        private int LapsCompleted(uint initialSpaceIndex, int offset)
        {
            return (int)((initialSpaceIndex + offset) / TotalSpaces);
        }
    }
}
