namespace Smart.Converter2.Converters
{
    using System;

    public sealed class EnumerableConverterFactory : IConverterFactory
    {
        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            // TODO
            return null;
        }
    }
}
