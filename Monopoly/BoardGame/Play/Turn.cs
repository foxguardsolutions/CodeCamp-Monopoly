using BoardGame.Dice;
using BoardGame.Locations;

namespace BoardGame.Play
{
    public class Turn
    {
        private readonly IPlayer _player;
        private readonly IDice _dice;
        private readonly IPlayerMover _playerMover;

        public Turn(IPlayer player, IDice dice, IPlayerMover playerMover)
        {
            _player = player;
            _dice = dice;
            _playerMover = playerMover;
        }

        public void Execute()
        {
            var roll = _dice.Roll();
            _playerMover.Move(_player, roll.Value);
        }
    }
}
