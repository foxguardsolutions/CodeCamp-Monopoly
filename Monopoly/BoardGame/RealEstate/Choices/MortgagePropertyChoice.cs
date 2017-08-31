using System.ComponentModel;

namespace BoardGame.RealEstate.Choices
{
    public enum MortgagePropertyChoice
    {
        [Description("No, leave property unmortgaged")]
        No,

        [Description("Yes, mortgage property")]
        Yes
    }
}
