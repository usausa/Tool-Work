namespace Smart.Converter2.Converters
{
    using System;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = "Ignore")]
    public sealed class CastConverterFactory : IConverterFactory
    {
        //private static readonly IReadOnlyDictionary<TypePair, Func<object, object>> Converters = new ReadOnlyDictionary<TypePair, Func<object, object>>(new Dictionary<TypePair, Func<object, object>>
        //{
        //    // int to
        //    { new TypePair(typeof(int), typeof(long)), (s) => (long)(int)s },
        //});

        public Func<object, object> GetConverter(Type sourceType, Type targetType)
        {
            //Converters.TryGetValue(
            //    targetType.IsNullableType()
            //        ? new TypePair(typePair.SourceType, Nullable.GetUnderlyingType(typePair.TargetType))
            //        : typePair,
            //    out var converter);
            //return converter;
            return null;
        }
    }
}
