using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Monopoly.RealEstate.Factories;

namespace Monopoly.RealEstate
{
    public class MonopolyProperties : IEnumerable<MonopolyPropertyGroup>
    {
        private readonly IEnumerable<MonopolyPropertyGroupFactory> _groupFactories;
        private IEnumerable<MonopolyPropertyGroup> _propertyGroups;

        public MonopolyProperties(IEnumerable<MonopolyPropertyGroupFactory> groupFactories)
        {
            _groupFactories = groupFactories;
        }

        private IEnumerable<MonopolyPropertyGroup> Groups => _propertyGroups ??
            (_propertyGroups = _groupFactories.Select(f => f.Create()).Cast<MonopolyPropertyGroup>().ToList());

        public IEnumerator<MonopolyPropertyGroup> GetEnumerator() => Groups.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
