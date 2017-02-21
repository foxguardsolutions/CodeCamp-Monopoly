using System.Collections.Generic;

namespace Monopoly
{
    public class Board
    {
        public Board()
        {
            _spaces = new Space[40];
        }

        private IReadOnlyList<Space> _spaces;

        public uint TotalSpaces
        {
            get { return (uint)_spaces.Count; }
        }

        public uint HighestPossibleSpace
        {
            get { return TotalSpaces - 1; }
        }
    }
}
