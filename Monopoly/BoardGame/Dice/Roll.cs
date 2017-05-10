namespace BoardGame.Dice
{
    public class Roll : IRoll
    {
        public ushort Value { get; }

        public Roll(ushort value)
        {
            Value = value;
        }
    }
}
