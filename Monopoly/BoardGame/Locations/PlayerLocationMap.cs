using System.Collections.Generic;

namespace BoardGame.Locations
{
    public class PlayerLocationMap : IPlayerLocationMap
    {
        private readonly IDictionary<IPlayer, Space> _playerLocations;

        public PlayerLocationMap()
        {
            _playerLocations = new Dictionary<IPlayer, Space>();
        }

        public Space Locate(IPlayer player)
        {
            return _playerLocations[player];
        }

        public void SetLocation(IPlayer player, Space space)
        {
            _playerLocations[player] = space;
        }
    }
}
