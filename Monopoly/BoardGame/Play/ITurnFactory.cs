namespace BoardGame.Play
{
    public interface ITurnFactory
    {
        Turn CreateFor(IPlayer player);
    }
}
