using System.Collections.Generic;

namespace BoardGame.Construction
{
    public interface IPlayerFactory
    {
        IEnumerable<IPlayer> Create(IEnumerable<string> names);
    }
}