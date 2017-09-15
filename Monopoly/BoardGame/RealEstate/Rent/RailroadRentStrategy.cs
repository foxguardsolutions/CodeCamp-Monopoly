using System.Collections.Generic;
using System.Linq;

namespace BoardGame.RealEstate.Rent
{
    public class RailroadRentStrategy : IRentStrategy
    {
        public int GetRentValue(IProperty thisRailroad, IEnumerable<IProperty> otherRailroads)
        {
            var otherRailroadsOwnedByThisPlayer = otherRailroads.Count(p => p.Owner == thisRailroad.Owner);
            var totalRailroadsOwnedByThisPlayer = otherRailroadsOwnedByThisPlayer + 1;
            return thisRailroad.BaseRent * totalRailroadsOwnedByThisPlayer;
        }
    }
}
