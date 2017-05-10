using System.Collections.Generic;

namespace BoardGame.Construction
{
    public interface IPlayerCountConstraint
    {
        bool IsSatisfiedBy(IEnumerable<IPlayer> players);
    }
}