namespace BoardGame.RealEstate
{
    public class Property : IProperty
    {
        public IPlayer Owner { get; set; }
        public int BaseRent { get; }

        public Property(int baseRent)
        {
            BaseRent = baseRent;
        }
    }
}
