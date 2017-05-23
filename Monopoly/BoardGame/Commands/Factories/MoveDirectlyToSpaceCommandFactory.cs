using BoardGame.Locations;

namespace BoardGame.Commands.Factories
{
    public class MoveDirectlyToSpaceCommandFactory : ICommandFactory
    {
        private readonly IPlayerMover _playerMover;
        private readonly ISpace _destination;

        public MoveDirectlyToSpaceCommandFactory(IPlayerMover playerMover, ISpace destination)
        {
            _playerMover = playerMover;
            _destination = destination;
        }

        public ICommand CreateFor(IPlayer player)
        {
            return new MoveDirectlyToSpaceCommand(player, _playerMover, _destination);
        }
    }
}
