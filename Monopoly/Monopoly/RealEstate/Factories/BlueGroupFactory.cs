using System;
using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class BlueGroupFactory : MonopolyPropertyGroupFactory
    {
        public BlueGroupFactory(StreetRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
            : base(rentStrategy, propertyFactory)
        {
        }

        protected override int[] Indices => new[] { 37, 39 };

        protected override IEnumerable<int[]> PropertyParameters
        {
            get
            {
                yield return new[] { 35, 350 };
                yield return new[] { 50, 400 };
            }
        }
    }
}
