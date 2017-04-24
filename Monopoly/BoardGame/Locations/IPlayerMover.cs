namespace BoardGame.Locations
{
    public interface IPlayerMover
    {
        void Move(IPlayer player, ushort spacesToMove);
        void Place(IPlayer player, Space space);
    }
}
