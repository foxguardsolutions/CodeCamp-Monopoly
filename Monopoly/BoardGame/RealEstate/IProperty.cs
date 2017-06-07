namespace BoardGame.RealEstate
{
    public interface IProperty
    {
        int BaseRent { get; }
        IPlayer Owner { get; set; }
    }
}
