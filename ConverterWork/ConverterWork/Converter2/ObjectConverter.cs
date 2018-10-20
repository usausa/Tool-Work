namespace Smart.Converter2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Smart.Converter2.Converters;

    /// <summary>
    ///
    /// </summary>
    public sealed class ObjectConverter : IObjectConverter
    {
        public static ObjectConverter Default { get; } = new ObjectConverter();

        private readonly TypePairHashArray converterCache = new TypePairHashArray();

        private IConverterFactory[] factories;

        public ObjectConverter()
        {
            factories = DefaultObjectFactories.Create().ToArray();
        }

        public ObjectConverter(IEnumerable<IConverterFactory> converterFactories)
        {
            factories = converterFactories.ToArray();
        }

        public void SetFactories(IEnumerable<IConverterFactory> converterFactories)
        {
            factories = converterFactories.ToArray();
            converterCache.Clear();
        }

        public void Reset()
        {
            factories = DefaultObjectFactories.Create().ToArray();
            converterCache.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Func<object, object> FindConverter(Type sourceType, Type targetType)
        {
            for (var i = 0; i < factories.Length; i++)
            {
                var converter = factories[i].GetConverter(sourceType, targetType);
                if (converter != null)
                {
                    return converter;
                }
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Func<object, object> GetConverter(Type sourceType, Type targetType)
        {
            if (!converterCache.TryGetValue(sourceType, targetType, out var converter))
            {
                converter = converterCache.AddIfNotExist(sourceType, targetType, FindConverter);
            }

            if (converter == null)
            {
                throw new ObjectConverterException($"Type {sourceType} can't convert to {targetType}");
            }

            return converter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Convert<T>(object value)
        {
            return (T)Convert(value, typeof(T));
        }

        public object Convert(object value, Type targetType)
        {
            // Specialized null
            if (value == null)
            {
                return targetType.GetDefaultValue();
            }

            // Specialized same type for performance (Nullable is excluded because operation is slow)
            var sourceType = value.GetType();
            if (sourceType == (targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType))
            {
                return value;
            }

            var converter = GetConverter(value.GetType(), targetType);
            return converter(value);
        }

        public Func<object, object> CreateConverter(Type sourceType, Type targetType)
        {
            return CreateConverter(
                targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType,
                targetType.GetDefaultValue(),
                GetConverter(sourceType.IsNullableType() ? Nullable.GetUnderlyingType(sourceType) : sourceType, targetType));
        }

        private static Func<object, object> CreateConverter(Type targetType, object defaultValue, Func<object, object> converter)
        {
            return value => value == null
                ? defaultValue
                : value.GetType() == targetType
                    ? value
                    : converter(value);
        }
    }
}
