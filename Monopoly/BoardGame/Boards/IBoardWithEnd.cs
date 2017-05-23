using System;

namespace BoardGame.Boards
{
    public interface IBoardWithEnd : IBoard
    {
        event EventHandler CrossedEndOfBoard;
    }
}
