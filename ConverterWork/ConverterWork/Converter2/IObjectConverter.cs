namespace Smart.Converter2
{
    using System;

    public interface IObjectConverter
    {
        T Convert<T>(object value);

        object Convert(object value, Type targetType);

        Func<object, object> GetConverter(Type sourceType, Type targetType);
    }
}
