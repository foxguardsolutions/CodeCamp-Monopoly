using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class GameBuilder
    {
        public const ushort MIN_PLAYERS = 2;
        public const ushort STARTING_SPACE = 0;
        private Board _board;
        private IDice _dice;
        private ICollection<IPlayer> _players = new List<IPlayer>();
        private IRandom _randomPlayerShuffler;

        public GameBuilder(IRandom randomNumberGenerator)
        {
            _randomPlayerShuffler = randomNumberGenerator;
        }

        public void AddPlayer(IPlayer player)
        {
            player.MoveToSpace(STARTING_SPACE);
            _players.Add(player);
        }

        public void SetDice(IDice dice)
        {
            _dice = dice;
        }

        public void SetBoard(Board board)
        {
            _board = board;
        }

        public Game BuildGame()
        {
            if (IsReadyToBuild())
            {
                var players = _players.FisherYatesShuffle(_randomPlayerShuffler).ToArray();
                return new Game(_board, _dice, players);
            }

            return null;
        }

        private bool IsReadyToBuild()
        {
            if (TooFewPlayers())
                return false;
            if (_board == null)
                return false;
            if (_dice == null)
                return false;
            return true;
        }

        private bool TooFewPlayers()
        {
            return _players.Count() < MIN_PLAYERS;
        }
    }
}
