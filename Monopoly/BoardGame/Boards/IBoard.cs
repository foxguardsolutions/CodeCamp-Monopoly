namespace BoardGame.Boards
{
    public interface IBoard
    {
        ISpace GetOffsetSpace(ISpace initialSpace, int offset);
    }
}
