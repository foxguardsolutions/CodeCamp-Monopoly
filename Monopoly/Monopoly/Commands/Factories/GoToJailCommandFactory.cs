using System;

using BoardGame;
using BoardGame.Commands;
using BoardGame.Commands.Factories;

namespace Monopoly.Commands.Factories
{
    public class GoToJailCommandFactory : DyadicCommandFactory<ISpace, MoveDirectlyToSpaceCommand>
    {
        public const int SpaceIndex = 30;
        public const int JustVisitingSpaceIndex = 10;
        public GoToJailCommandFactory(ISpace space, Func<IPlayer, ISpace, MoveDirectlyToSpaceCommand> innerCommandFactory)
            : base(space, innerCommandFactory)
        {
        }
    }
}
