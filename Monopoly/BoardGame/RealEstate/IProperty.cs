namespace BoardGame.RealEstate
{
    public interface IProperty
    {
        int BaseRent { get; }
        uint PurchasePrice { get; }
        IPlayer Owner { get; set; }
        bool IsMortgaged { get; set; }
    }
}
