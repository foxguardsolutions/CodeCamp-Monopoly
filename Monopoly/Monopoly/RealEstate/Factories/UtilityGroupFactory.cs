using System;
using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class UtilityGroupFactory : MonopolyPropertyGroupFactory
    {
        public UtilityGroupFactory(UtilityRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
            : base(rentStrategy, propertyFactory)
        {
        }

        protected override int[] Indices => new[] { 12, 28 };

        protected override IEnumerable<int[]> PropertyParameters
        {
            get
            {
                yield return new[] { 0, 150 };
                yield return new[] { 0, 150 };
            }
        }
    }
}
