using System.Collections.Generic;
using System.Linq;

namespace BoardGame.RealEstate.Rent
{
    public class RentCalculator : IRentCalculator
    {
        private readonly IEnumerable<IPropertyGroup> _propertyGroups;

        public RentCalculator(IEnumerable<IPropertyGroup> propertyGroups)
        {
            _propertyGroups = propertyGroups;
        }

        public int GetRentFor(IProperty property)
        {
            var groupContainingProperty = _propertyGroups.Single(g => g.Contains(property));
            return groupContainingProperty.GetRentFor(property);
        }
    }
}
