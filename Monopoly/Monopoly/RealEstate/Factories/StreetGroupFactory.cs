using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class StreetGroupFactory : MonopolyPropertyGroupFactory
    {
        private readonly IEnumerable<Street> _streets;
        public StreetGroupFactory(StreetRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory, IEnumerable<Street> streets)
            : base(rentStrategy, propertyFactory)
        {
            _streets = streets.ToArray();
        }

        protected override int[] Indices => _streets.Select(s => s.Index).ToArray();

        protected override IEnumerable<int[]> PropertyParameters
            => _streets.Select(s => new[] { s.BaseRent, s.PurchasePrice });
    }
}
