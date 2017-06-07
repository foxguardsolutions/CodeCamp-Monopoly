namespace BoardGame.Dice
{
    public interface IDiceWithCache : IDice
    {
        IRoll GetLastRoll();
    }
}
