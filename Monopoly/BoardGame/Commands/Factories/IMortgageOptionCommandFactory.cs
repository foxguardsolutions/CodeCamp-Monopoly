using BoardGame.RealEstate;

namespace BoardGame.Commands.Factories
{
    public interface IMortgageOptionCommandFactory
    {
        ICommand Create(IPlayer player, IProperty property);
    }
}
