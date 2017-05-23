namespace BoardGame.Boards
{
    public interface ILapCounter
    {
        uint GetLapsCompleted();
        void Reset();
    }
}
