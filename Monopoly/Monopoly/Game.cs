using System.Collections.Generic;

namespace Monopoly
{
    public class Game
    {
        public const uint TOTAL_ROUNDS_IN_A_GAME = 20;
        private uint _roundsComplete;

        public Game(Board board, IDice dice, IEnumerable<IPlayer> players)
        {
            Board = board;
            Dice = dice;
            Players = players;
        }

        public bool IsFinished
        {
            get { return _roundsComplete >= TOTAL_ROUNDS_IN_A_GAME; }
        }

        public Board Board { get; }
        public IDice Dice { get; }
        public IEnumerable<IPlayer> Players { get; }

        public void PlayRound()
        {
            foreach (var player in Players)
                player.TakeATurn(Dice, Board);

            _roundsComplete++;
        }
    }
}
