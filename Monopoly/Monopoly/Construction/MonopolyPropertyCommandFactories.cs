using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using BoardGame.Commands.Factories;
using BoardGame.RealEstate;
using Monopoly.RealEstate;

namespace Monopoly.Construction
{
    public class MonopolyPropertyCommandFactories : IEnumerable<KeyValuePair<int, LandOnPropertyCommandFactory>>
    {
        private readonly MonopolyProperties _properties;
        private readonly Func<IProperty, LandOnPropertyCommandFactory> _innerFactory;
        private readonly IDictionary<int, LandOnPropertyCommandFactory> _factoriesByIndex;

        public MonopolyPropertyCommandFactories(MonopolyProperties properties, Func<IProperty, LandOnPropertyCommandFactory> factory)
        {
            _properties = properties;
            _innerFactory = factory;
            _factoriesByIndex = CreatePropertyCommandFactories();
        }

        private IDictionary<int, LandOnPropertyCommandFactory> CreatePropertyCommandFactories()
        {
            var factories = new Dictionary<int, LandOnPropertyCommandFactory>();

            foreach (var group in _properties)
                CreateAllInGroup(factories, group);

            return factories;
        }

        private void CreateAllInGroup(IDictionary<int, LandOnPropertyCommandFactory> factories, MonopolyPropertyGroup group)
        {
            foreach (var pair in group.Indices
                                .Zip(group, (index, property) => new { Index = index, Property = property }))
                factories[pair.Index] = _innerFactory(pair.Property);
        }

        public IEnumerator<KeyValuePair<int, LandOnPropertyCommandFactory>> GetEnumerator()
        {
            return _factoriesByIndex.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
