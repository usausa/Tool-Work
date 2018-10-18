namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed class BooleanConverterFactory : IConverterFactory
    {
        private static readonly Type BooleanType = typeof(bool);

        private static readonly Type NullableBooleanType = typeof(bool?);

        // TODO
        private static readonly Dictionary<Type, Func<object, object>> FromBooleanConverters = new Dictionary<Type, Func<object, object>>
        {
            { typeof(int), x => (bool)x ? 1 : 0 },
        };

        // TODO
        private static readonly Dictionary<Type, Func<object, object>> ToBooleanConverters = new Dictionary<Type, Func<object, object>>
        {
            { typeof(int), x => (int)x != 0 },
        };

        public Func<object, object> GetConverter(Type sourceType, Type targetType)
        {
            if (sourceType == BooleanType)
            {
                if (targetType.IsValueType)
                {
                    var type = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
                    if (FromBooleanConverters.TryGetValue(type, out var converter))
                    {
                        return converter;
                    }
                }
            }
            else if ((targetType == BooleanType) || (targetType == NullableBooleanType))
            {
                if (sourceType.IsValueType)
                {
                    var type = sourceType.IsNullableType() ? Nullable.GetUnderlyingType(sourceType) : sourceType;
                    if (ToBooleanConverters.TryGetValue(type, out var converter))
                    {
                        return converter;
                    }
                }
            }

            return null;
        }
    }
}
