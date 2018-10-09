namespace Smart.Converter2.Converters
{
    using System;

    public sealed class ToStringConverterFactory : IConverterFactory
    {
        private static readonly Type StringType = typeof(string);

        private static readonly Func<TypePair, object, object> Converter = (typePair, source) => source.ToString();

        public Func<TypePair, object, object> GetConverter(TypePair typePair)
        {
            return typePair.TargetType == StringType ? Converter : null;
        }
    }
}
