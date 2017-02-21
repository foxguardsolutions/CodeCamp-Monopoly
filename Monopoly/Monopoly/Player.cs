namespace Monopoly
{
    public class Player : IPlayer
    {
        private uint _currentSpace;

        public Player(uint initialSpace)
        {
            _currentSpace = initialSpace;
        }

        public uint CurrentSpace
        {
            get { return _currentSpace; }
        }

        public void MoveToSpace(uint spaceNumber)
        {
            _currentSpace = spaceNumber;
        }

        public void MoveSpaces(uint spacesToMove, Board board)
        {
            _currentSpace = (_currentSpace + spacesToMove) % board.TotalSpaces;
        }

        public void TakeATurn(IDice dice, Board board)
        {
            var roll = dice.Roll();
            MoveSpaces(roll.Value, board);
        }
    }
}
