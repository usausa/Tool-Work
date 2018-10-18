namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed class NumericConverterFactory : IConverterFactory
    {
        // TODO
        private static readonly Dictionary<Tuple<Type, Type>, Func<object, object>> Converters = new Dictionary<Tuple<Type, Type>, Func<object, object>>
        {
            { Tuple.Create(typeof(int), typeof(long)), x => (long)(int)x },
        };

        public Func<object, object> GetConverter(Type sourceType, Type targetType)
        {
            if (sourceType.IsValueType && targetType.IsValueType)
            {
                var key = Tuple.Create(sourceType, targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType);
                if (Converters.TryGetValue(key, out var converter))
                {
                    return converter;
                }
            }

            return null;
        }
    }
}
