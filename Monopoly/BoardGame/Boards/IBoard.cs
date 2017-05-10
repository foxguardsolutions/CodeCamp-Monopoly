namespace BoardGame.Boards
{
    public interface IBoard
    {
        Space GetOffsetSpace(Space initialSpace, int offset);
    }
}
