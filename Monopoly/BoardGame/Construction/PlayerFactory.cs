using System.Collections.Generic;
using System.Linq;

namespace BoardGame.Construction
{
    public class PlayerFactory : IPlayerFactory
    {
        public IEnumerable<IPlayer> Create(IEnumerable<string> names)
        {
            return names.Select(name => new Player(name));
        }
    }
}
