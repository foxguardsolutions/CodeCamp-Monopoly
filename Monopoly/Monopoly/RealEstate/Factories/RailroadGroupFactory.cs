using System;
using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public class RailroadGroupFactory : MonopolyPropertyGroupFactory
    {
        public RailroadGroupFactory(RailroadRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
            : base(rentStrategy, propertyFactory)
        {
        }

        protected override int[] Indices => new[] { 5, 15, 25, 35 };

        protected override IEnumerable<int[]> PropertyParameters
        {
            get
            {
                for (var i = 0; i < Indices.Length; i++)
                    yield return new[] { 25, 200 };
            }
        }
    }
}
