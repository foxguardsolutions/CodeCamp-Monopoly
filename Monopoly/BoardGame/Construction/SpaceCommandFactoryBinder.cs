using System.Collections.Generic;
using System.Linq;

using BoardGame.Commands.Factories;

namespace BoardGame.Construction
{
    public abstract class SpaceCommandFactoryBinder : ISpaceCommandFactoryBinder
    {
        private readonly IEnumerable<ISpace> _spaces;
        protected abstract IEnumerable<ICommandFactory> CommandFactories { get; set; }

        protected SpaceCommandFactoryBinder(IEnumerable<ISpace> spaces)
        {
            _spaces = spaces;
        }

        public void BindCommandFactoriesToSpaces()
        {
            foreach (var pair in _spaces.Zip(CommandFactories, (space, factory) => new { Space = space, Factory = factory }))
                BindFactoryToSpace(pair.Space, pair.Factory);
        }

        private static void BindFactoryToSpace(ISpace space, ICommandFactory factory)
        {
            space.CommandFactory = factory;
        }
    }
}
