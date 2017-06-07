using System.Collections.Generic;

namespace BoardGame.RealEstate.Rent
{
    public interface IRentStrategy
    {
        int GetRentValue(IProperty thisProperty, IEnumerable<IProperty> otherProperties);
    }
}
