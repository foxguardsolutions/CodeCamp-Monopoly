using System.Collections.Generic;
using System.Linq;

namespace BoardGame.RealEstate.Rent
{
    public class StreetRentStrategy : IRentStrategy
    {
        public const int CompletedGroupMultiplier = 2;
        public int GetRentValue(IProperty thisProperty, IEnumerable<IProperty> otherProperties)
        {
            if (otherProperties.All(p => p.Owner == thisProperty.Owner))
                return thisProperty.BaseRent * CompletedGroupMultiplier;
            return thisProperty.BaseRent;
        }
    }
}
