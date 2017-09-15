using System.Collections.Generic;
using System.Linq;

using BoardGame.Dice;

namespace BoardGame.RealEstate.Rent
{
    public class UtilityRentStrategy : IRentStrategy
    {
        public const int AllUtilitiesOwnedMultiplier = 10;
        public const int SingleUtilityOwnedMultiplier = 4;
        private readonly IDiceWithCache _dice;

        public UtilityRentStrategy(IDiceWithCache dice)
        {
            _dice = dice;
        }

        public int GetRentValue(IProperty thisUtility, IEnumerable<IProperty> otherUtilities)
        {
            var multiplier = otherUtilities.Any(u => u.Owner == default(IPlayer))
                ? SingleUtilityOwnedMultiplier
                : AllUtilitiesOwnedMultiplier;
            return _dice.GetLastRoll().Value * multiplier;
        }
    }
}
