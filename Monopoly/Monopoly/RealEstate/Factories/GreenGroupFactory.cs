using System;
using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class GreenGroupFactory : MonopolyPropertyGroupFactory
    {
        public GreenGroupFactory(StreetRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
            : base(rentStrategy, propertyFactory)
        {
        }

        protected override int[] Indices => new[] { 31, 32, 34 };

        protected override IEnumerable<int[]> PropertyParameters
        {
            get
            {
                yield return new[] { 26, 300 };
                yield return new[] { 26, 300 };
                yield return new[] { 28, 320 };
            }
        }
    }
}
