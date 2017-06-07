namespace BoardGame.Dice
{
    public class DiceWithCacheDecorator : IDiceWithCache
    {
        private readonly IDice _decoratedDice;
        private IRoll _lastRoll;

        public DiceWithCacheDecorator(IDice dice)
        {
            _decoratedDice = dice;
        }

        public IRoll Roll()
        {
            var roll = _decoratedDice.Roll();
            _lastRoll = roll;
            return roll;
        }

        public IRoll GetLastRoll()
        {
            return _lastRoll;
        }
    }
}
