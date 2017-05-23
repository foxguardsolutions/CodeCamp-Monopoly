namespace BoardGame.Locations
{
    public interface IPlayerMover
    {
        ISpace Move(IPlayer player, ushort spacesToMove);
        void Place(IPlayer player, ISpace space);
    }
}
