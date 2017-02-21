using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class PairOfSixSidedDice : IDice
    {
        private IEnumerable<ushort>[] _faceValues;
        private IRandom _randomDieRoller;

        public PairOfSixSidedDice(IRandom randomNumberGenerator)
        {
            var die = SixSidedDie();
            _faceValues = new IEnumerable<ushort>[] { die, die };
            _randomDieRoller = randomNumberGenerator;
        }

        private IEnumerable<ushort> SixSidedDie()
        {
            return Enumerable.Range(1, 6).Select(i => (ushort)i);
        }

        public IRoll Roll()
        {
            return new Roll(_randomDieRoller, _faceValues);
        }
    }
}
