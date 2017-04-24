namespace BoardGame.Locations
{
    public interface IPlayerLocationMap
    {
        Space Locate(IPlayer player);
        void SetLocation(IPlayer player, Space space);
    }
}
