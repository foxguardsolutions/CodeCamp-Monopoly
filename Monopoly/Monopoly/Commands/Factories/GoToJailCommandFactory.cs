using BoardGame;
using BoardGame.Commands.Factories;
using BoardGame.Locations;

namespace Monopoly.Commands.Factories
{
    public class GoToJailCommandFactory : MoveDirectlyToSpaceCommandFactory
    {
        public const int SpaceIndex = 30;
        public const int JustVisitingSpaceIndex = 10;
        public GoToJailCommandFactory(IPlayerMover playerMover, ISpace space)
            : base(playerMover, space)
        {
        }
    }
}
