using System;

namespace UserInterface
{
    public class EnumWrapper : IEnum
    {
        public virtual Type GetUnderlyingType(Type enumType)
        {
            return Enum.GetUnderlyingType(enumType);
        }

        public virtual Array GetValues(Type enumType)
        {
            return Enum.GetValues(enumType);
        }

        public virtual bool IsDefined(Type enumType, object value)
        {
            return Enum.IsDefined(enumType, value);
        }

        public virtual bool TryParse<TEnum>(string value, out TEnum result)
            where TEnum : struct
        {
            return Enum.TryParse(value, out result);
        }
    }
}
