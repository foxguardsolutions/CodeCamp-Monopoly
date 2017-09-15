using System.ComponentModel;

namespace BoardGame.RealEstate.Choices
{
    public enum UnmortgagePropertyChoice
    {
        [Description("No, leave property mortgaged")]
        No,

        [Description("Yes, unmortgage property")]
        Yes
    }
}
