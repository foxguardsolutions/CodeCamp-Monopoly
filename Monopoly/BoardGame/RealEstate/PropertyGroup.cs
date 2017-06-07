using System.Collections.Generic;
using System.Linq;

using BoardGame.RealEstate.Rent;

namespace BoardGame.RealEstate
{
    public class PropertyGroup : IPropertyGroup
    {
        private readonly IRentStrategy _rentStrategy;
        private readonly IEnumerable<IProperty> _properties;

        public PropertyGroup(IRentStrategy rentStrategy, IEnumerable<IProperty> properties)
        {
            _rentStrategy = rentStrategy;
            _properties = properties;
        }

        public int GetRentFor(IProperty property)
        {
            var otherProperties = _properties.Except(new[] { property });
            return _rentStrategy.GetRentValue(property, otherProperties);
        }

        public bool Contains(IProperty property)
        {
            return _properties.Contains(property);
        }
    }
}
