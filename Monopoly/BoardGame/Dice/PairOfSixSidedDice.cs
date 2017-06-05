using System.Collections.Generic;
using System.Linq;

using Shuffler;

namespace BoardGame.Dice
{
    public class PairOfSixSidedDice : IDice
    {
        private readonly IShuffler _randomDieRoller;
        private static IEnumerable<int> Die => Enumerable.Range(1, 6);

        public PairOfSixSidedDice(IShuffler shuffler)
        {
            _randomDieRoller = shuffler;
        }

        public IRoll Roll()
        {
            var firstDieRoll = RollDie();
            var secondDieRoll = RollDie();

            return new Roll((ushort)(firstDieRoll + secondDieRoll));
        }

        private ushort RollDie()
        {
            return (ushort)_randomDieRoller.Shuffle(Die).First();
        }
    }
}
