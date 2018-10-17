namespace Smart.Converter2.Converters
{
    using System;

    public sealed class ToStringConverterFactory : IConverterFactory
    {
        private static readonly Type StringType = typeof(string);

        private static readonly Func<object, object> Converter = source => source.ToString();

        public Func<object, object> GetConverter(Type sourceType, Type targetType)
        {
            return targetType == StringType ? Converter : null;
        }
    }
}
