using System.Collections.Generic;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace Monopoly.RealEstate
{
    public class MonopolyPropertyGroup : PropertyGroup
    {
        public int[] Indices { get; }
        public MonopolyPropertyGroup(int[] indices, IRentStrategy rentStrategy, IEnumerable<IProperty> properties)
            : base(rentStrategy, properties)
        {
            Indices = indices;
        }
    }
}
