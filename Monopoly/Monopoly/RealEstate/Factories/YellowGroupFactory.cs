using System;
using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class YellowGroupFactory : MonopolyPropertyGroupFactory
    {
        public YellowGroupFactory(StreetRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
            : base(rentStrategy, propertyFactory)
        {
        }

        protected override int[] Indices => new[] { 26, 27, 29 };

        protected override IEnumerable<int[]> PropertyParameters
        {
            get
            {
                yield return new[] { 22, 260 };
                yield return new[] { 22, 260 };
                yield return new[] { 24, 280 };
            }
        }
    }
}
