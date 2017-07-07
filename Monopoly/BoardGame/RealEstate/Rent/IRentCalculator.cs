namespace BoardGame.RealEstate.Rent
{
    public interface IRentCalculator
    {
        int GetRentFor(IProperty property);
    }
}