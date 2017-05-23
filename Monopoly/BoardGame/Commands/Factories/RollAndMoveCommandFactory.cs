using BoardGame.Dice;
using BoardGame.Locations;

namespace BoardGame.Commands.Factories
{
    public class RollAndMoveCommandFactory : ICommandFactory
    {
        private readonly IDice _dice;
        private readonly IPlayerMover _playerMover;

        public RollAndMoveCommandFactory(IDice dice, IPlayerMover playerMover)
        {
            _dice = dice;
            _playerMover = playerMover;
        }

        public ICommand CreateFor(IPlayer player)
        {
            return new RollAndMoveCommand(player, _playerMover, _dice);
        }
    }
}
