namespace BoardGame.RealEstate
{
    public class Property : IProperty
    {
        public IPlayer Owner { get; set; }
        public int BaseRent { get; }
        public uint PurchasePrice { get; }

        public Property(int baseRent, uint purchasePrice)
        {
            BaseRent = baseRent;
            PurchasePrice = purchasePrice;
        }
    }
}
