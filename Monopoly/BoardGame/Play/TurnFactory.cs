using BoardGame.Dice;
using BoardGame.Locations;

namespace BoardGame.Play
{
    public class TurnFactory : ITurnFactory
    {
        private readonly IDice _dice;
        private readonly IPlayerMover _playerMover;

        public TurnFactory(IDice dice, IPlayerMover playerMover)
        {
            _dice = dice;
            _playerMover = playerMover;
        }

        public Turn Create(IPlayer player)
        {
            return new Turn(player, _dice, _playerMover);
        }
    }
}
