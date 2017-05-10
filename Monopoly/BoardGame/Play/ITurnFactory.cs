namespace BoardGame.Play
{
    public interface ITurnFactory
    {
        Turn Create(IPlayer player);
    }
}