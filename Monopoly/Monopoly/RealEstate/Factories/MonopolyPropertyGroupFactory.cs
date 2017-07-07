using System;
using System.Collections.Generic;
using System.Linq;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate.Factories
{
    public abstract class MonopolyPropertyGroupFactory
    {
        private readonly IRentStrategy _rentStrategy;
        private readonly Func<int, uint, IProperty> _propertyFactory;

        protected MonopolyPropertyGroupFactory(IRentStrategy rentStrategy, Func<int, uint, IProperty> propertyFactory)
        {
            _rentStrategy = rentStrategy;
            _propertyFactory = propertyFactory;
        }

        public IPropertyGroup Create()
        {
            var properties = PropertyParameters.Select(p => _propertyFactory(p[0], (uint)p[1])).ToList();
            return new MonopolyPropertyGroup(Indices, _rentStrategy, properties);
        }

        protected abstract int[] Indices { get; }
        protected abstract IEnumerable<int[]> PropertyParameters { get; }
    }
}
