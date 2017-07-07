using System;
using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class SkyBlueGroupFactory : MonopolyPropertyGroupFactory
    {
        public SkyBlueGroupFactory(StreetRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
            : base(rentStrategy, propertyFactory)
        {
        }

        protected override int[] Indices => new[] { 6, 8, 9 };

        protected override IEnumerable<int[]> PropertyParameters
        {
            get
            {
                yield return new[] { 6, 100 };
                yield return new[] { 6, 100 };
                yield return new[] { 8, 120 };
            }
        }
    }
}
