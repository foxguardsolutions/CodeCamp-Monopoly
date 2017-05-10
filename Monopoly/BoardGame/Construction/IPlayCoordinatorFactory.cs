using System.Collections.Generic;

using BoardGame.Play;

namespace BoardGame.Construction
{
    public interface IPlayCoordinatorFactory
    {
        IPlayCoordinator Create(IEnumerable<IPlayer> players);
    }
}