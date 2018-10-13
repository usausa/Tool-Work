namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;

    /// <summary>
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = "Ignore")]
    public sealed class CastConverterFactory : IConverterFactory
    {
        private static readonly IReadOnlyDictionary<TypePair, Func<object, object>> Converters = new ReadOnlyDictionary<TypePair, Func<object, object>>(new Dictionary<TypePair, Func<object, object>>
        {
            // int to
            { new TypePair(typeof(int), typeof(long)), (s) => (long)(int)s },
        });

        /// <summary>
        ///
        /// </summary>
        /// <param name="typePair"></param>
        /// <returns></returns>
        public Func<object, object> GetConverter(TypePair typePair)
        {
            if (typePair.TargetType.IsNullableType())
            {
                typePair = new TypePair(typePair.SourceType, Nullable.GetUnderlyingType(typePair.TargetType));
            }

            Converters.TryGetValue(typePair, out var converter);
            return converter;
        }
    }
}
