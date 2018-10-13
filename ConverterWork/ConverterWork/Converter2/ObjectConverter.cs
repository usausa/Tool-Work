namespace Smart.Converter2
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Smart.Collections.Concurrent;
    using Smart.Converter2.Converters;

    /// <summary>
    ///
    /// </summary>
    public sealed class ObjectConverter : IObjectConverter
    {
        public static ObjectConverter Default { get; } = new ObjectConverter();

        private readonly ThreadsafeHashArrayMap<TypePair, Func<object, object>> converterCache = new ThreadsafeHashArrayMap<TypePair, Func<object, object>>();

        private IList<IConverterFactory> factories;

        public ObjectConverter()
        {
            ResetFactories();
        }

        public void SetFactories(IList<IConverterFactory> list)
        {
            factories = list;
            converterCache.Clear();
        }

        public void ResetFactories()
        {
            factories = DefaultObjectFactories.Create();
            converterCache.Clear();
        }

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
            if (sourceType == targetType)
            {
                return value;
            }

            var typePair = new TypePair(value.GetType(), targetType);
            if (converterCache.TryGetValue(typePair, out var converter))
            {
                return converter;
            }

            converter = converterCache.AddIfNotExist(
                typePair,
                GetConverter);
            if (converter == null)
            {
                throw new ObjectConverterException(String.Format(CultureInfo.InvariantCulture, "Type {0} can't convert to {1}", value.GetType().ToString(), targetType));
            }

            return converter(value);
        }

        public Func<object, object> GetConverter(Type sourceType, Type targetType)
        {
            throw new NotImplementedException();
        }

        private Func<object, object> GetConverter(TypePair typePair)
        {
            return factories.Select(f => f.GetConverter(typePair)).FirstOrDefault(c => c != null);
        }
    }
}
