using System.ComponentModel;

namespace UserInterface.Extensions
{
    public static class EnumExtensions
    {
        // https://stackoverflow.com/questions/2650080/how-to-get-c-sharp-enum-description-from-value
        public static string GetDescription<T>(this T enumeration)
            where T : struct
        {
            var fieldInfo = enumeration.GetType().GetField(enumeration.ToString());

            var attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            return attributes.Length > 0 ? attributes[0].Description : enumeration.ToString();
        }
    }
}
