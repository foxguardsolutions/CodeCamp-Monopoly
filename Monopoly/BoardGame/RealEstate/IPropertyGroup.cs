using System.Collections.Generic;

namespace BoardGame.RealEstate
{
    public interface IPropertyGroup : IEnumerable<IProperty>
    {
        int GetRentFor(IProperty property);
        bool Contains(IProperty property);
    }
}
