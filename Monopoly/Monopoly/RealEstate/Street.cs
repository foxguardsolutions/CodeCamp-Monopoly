namespace Monopoly.RealEstate
{
    public struct Street
    {
        public Street(int index, int baseRent, int purchasePrice)
        {
            Index = index;
            BaseRent = baseRent;
            PurchasePrice = purchasePrice;
        }

        public int Index { get; }
        public int BaseRent { get; }
        public int PurchasePrice { get; }
    }
}
