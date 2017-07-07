using System;
using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class RedGroupFactory : MonopolyPropertyGroupFactory
    {
        public RedGroupFactory(StreetRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
            : base(rentStrategy, propertyFactory)
        {
        }

        protected override int[] Indices => new[] { 21, 23, 24 };

        protected override IEnumerable<int[]> PropertyParameters
        {
            get
            {
                yield return new[] { 18, 220 };
                yield return new[] { 18, 220 };
                yield return new[] { 20, 240 };
            }
        }
    }
}
