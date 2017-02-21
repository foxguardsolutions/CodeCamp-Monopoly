namespace Monopoly
{
    public interface IPlayer
    {
        uint CurrentSpace { get; }

        void MoveSpaces(uint spacesToMove, Board board);
        void MoveToSpace(uint spaceNumber);
        void TakeATurn(IDice dice, Board board);
    }
}