namespace Smart.Converter2.Converters
{
    using System;

    public sealed class AssignableConverterFactory : IConverterFactory
    {
        private static readonly Func<object, object> Converter = source => source;

        public Func<object, object> GetConverter(TypePair typePair)
        {
            return typePair.TargetType.IsAssignableFrom(typePair.SourceType) ? Converter : null;
        }
    }
}
