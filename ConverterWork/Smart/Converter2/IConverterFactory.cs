namespace Smart.Converter2
{
    using System;

    public interface IConverterFactory
    {
        Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType);
    }
}
