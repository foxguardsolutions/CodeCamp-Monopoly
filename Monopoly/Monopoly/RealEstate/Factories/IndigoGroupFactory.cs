using System;
using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class IndigoGroupFactory : MonopolyPropertyGroupFactory
    {
        public IndigoGroupFactory(StreetRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
            : base(rentStrategy, propertyFactory)
        {
        }

        protected override int[] Indices => new[] { 1, 3 };

        protected override IEnumerable<int[]> PropertyParameters
        {
            get
            {
                yield return new[] { 2, 60 };
                yield return new[] { 4, 60 };
            }
        }
    }
}
