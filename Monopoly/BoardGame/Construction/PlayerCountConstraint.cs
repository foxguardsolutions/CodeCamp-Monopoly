using System.Collections.Generic;
using System.Linq;

namespace BoardGame.Construction
{
    public class PlayerCountConstraint : IPlayerCountConstraint
    {
        private readonly uint _minimumPlayerCount;
        private readonly uint _maximumPlayerCount;

        public PlayerCountConstraint(uint minimumPlayerCount, uint maximumPlayerCount)
        {
            _minimumPlayerCount = minimumPlayerCount;
            _maximumPlayerCount = maximumPlayerCount;
        }

        public bool IsSatisfiedBy(IEnumerable<IPlayer> players)
        {
            return !TooFew(players) && !TooMany(players);
        }

        private bool TooMany(IEnumerable<IPlayer> players)
        {
            return players.Count() > _maximumPlayerCount;
        }

        private bool TooFew(IEnumerable<IPlayer> players)
        {
            return players.Count() < _minimumPlayerCount;
        }
    }
}
