using System.Collections.Generic;

namespace BoardGame.Construction
{
    public interface IGameStateConfigurationInitializer
    {
        void ConfigureGame(IEnumerable<IPlayer> players);
    }
}
