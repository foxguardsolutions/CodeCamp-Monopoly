using System.ComponentModel;

namespace BoardGame.RealEstate.Choices
{
    public enum UnmortgageProperty
    {
        [Description("No, leave property mortgaged")]
        No,

        [Description("Yes, unmortgage property")]
        Yes
    }
}
