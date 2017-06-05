using BoardGame.Boards;

namespace BoardGame.Locations
{
    public class PlayerMover : IPlayerMover
    {
        private readonly IPlayerLocationMap _map;
        private readonly IBoard _board;

        public PlayerMover(IPlayerLocationMap map, IBoard board)
        {
            _map = map;
            _board = board;
        }

        public ISpace Move(IPlayer player, ushort spacesToMove)
        {
            var initialSpace = _map.Locate(player);
            var finalSpace = _board.GetOffsetSpace(initialSpace, spacesToMove);
            Place(player, finalSpace);
            return finalSpace;
        }

        public void Place(IPlayer player, ISpace space)
        {
            _map.SetLocation(player, space);
        }
    }
}
