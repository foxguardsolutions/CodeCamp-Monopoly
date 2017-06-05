namespace BoardGame.Locations
{
    public interface IPlayerLocationMap
    {
        ISpace Locate(IPlayer player);
        void SetLocation(IPlayer player, ISpace space);
    }
}
