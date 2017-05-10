namespace BoardGame.Extensions
{
    public static class MathExtensions
    {
        public static ulong Modulo(this long value, uint modulus)
        {
            if (value < 0)
                value = GetCongruentPositiveValue(value, modulus);
            return (ulong)(value % modulus);
        }

        private static long GetCongruentPositiveValue(long value, uint modulus)
        {
            return (value % modulus) + modulus;
        }
    }
}
