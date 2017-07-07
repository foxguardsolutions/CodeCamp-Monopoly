using System;
using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class DarkOrchidGroupFactory : MonopolyPropertyGroupFactory
    {
        public DarkOrchidGroupFactory(StreetRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
            : base(rentStrategy, propertyFactory)
        {
        }

        protected override int[] Indices => new[] { 11, 13, 14 };

        protected override IEnumerable<int[]> PropertyParameters
        {
            get
            {
                yield return new[] { 10, 140 };
                yield return new[] { 10, 140 };
                yield return new[] { 12, 160 };
            }
        }
    }
}
