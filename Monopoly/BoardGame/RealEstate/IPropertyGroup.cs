namespace BoardGame.RealEstate
{
    public interface IPropertyGroup
    {
        int GetRentFor(IProperty property);
        bool Contains(IProperty property);
    }
}
