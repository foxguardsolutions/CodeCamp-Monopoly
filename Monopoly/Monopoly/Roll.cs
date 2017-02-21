using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class Roll : IRoll
    {
        private IEnumerable<ushort> _dieRolls;

        public Roll(IRandom randomNumberGenerator, params IEnumerable<ushort>[] dice)
        {
            var randomDieRoller = randomNumberGenerator;
            _dieRolls = RollDice(randomDieRoller, dice);
        }

        private IEnumerable<ushort> RollDice(IRandom randomDieRoller, IEnumerable<ushort>[] dice)
        {
            foreach (var die in dice)
                yield return RollDie(randomDieRoller, die);
        }

        private ushort RollDie(IRandom randomDieRoller, IEnumerable<ushort> die)
        {
            return die.FisherYatesShuffle(randomDieRoller).First();
        }

        public ushort Value
        {
            get { return (ushort)_dieRolls.Sum(d => d); }
        }
    }
}
