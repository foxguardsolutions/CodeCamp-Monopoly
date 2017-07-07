using System;
using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class OrangeGroupFactory : MonopolyPropertyGroupFactory
    {
        public OrangeGroupFactory(StreetRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
            : base(rentStrategy, propertyFactory)
        {
        }

        protected override int[] Indices => new[] { 16, 18, 19 };

        protected override IEnumerable<int[]> PropertyParameters
        {
            get
            {
                yield return new[] { 14, 180 };
                yield return new[] { 14, 180 };
                yield return new[] { 16, 200 };
            }
        }
    }
}
