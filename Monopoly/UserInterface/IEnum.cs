using System;

namespace UserInterface
{
    public interface IEnum
    {
        Type GetUnderlyingType(Type enumType);
        Array GetValues(Type enumType);
        bool IsDefined(Type enumType, object value);
        bool TryParse<TEnum>(string value, out TEnum result)
            where TEnum : struct;
    }
}
