using System.Collections.Generic;

namespace BoardGame.Locations
{
    public class PlayerLocationMap : IPlayerLocationMap
    {
        private readonly IDictionary<IPlayer, ISpace> _playerLocations;

        public PlayerLocationMap()
        {
            _playerLocations = new Dictionary<IPlayer, ISpace>();
        }

        public ISpace Locate(IPlayer player)
        {
            return _playerLocations[player];
        }

        public void SetLocation(IPlayer player, ISpace space)
        {
            _playerLocations[player] = space;
        }
    }
}
