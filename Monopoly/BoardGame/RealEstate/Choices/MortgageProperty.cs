using System.ComponentModel;

namespace BoardGame.RealEstate.Choices
{
    public enum MortgageProperty
    {
        [Description("No, leave property unmortgaged")]
        No,

        [Description("Yes, mortgage property")]
        Yes
    }
}
