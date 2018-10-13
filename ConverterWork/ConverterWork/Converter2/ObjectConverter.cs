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

        // TODO inline ?
        public T Convert<T>(object value)
        {
            return (T)Convert(value, typeof(T));
        }

        public object Convert(object value, Type targetType)
        {
            // TODO check point 1
            if (value == null)
            {
                return targetType.GetDefaultValue();
            }

            // TODO check point 2
            var sourceType = value.GetType();
            if (sourceType == (targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType))
            {
                return value;
            }

            var typePair = new TypePair(sourceType, targetType);
            var converter = converterCache.AddIfNotExist(
                typePair,
                tp => factories.Select(f => f.GetConverter(tp)).FirstOrDefault(c => c != null));
            if (converter == null)
            {
                throw new ObjectConverterException(String.Format(CultureInfo.InvariantCulture, "Type {0} can't convert to {1}", value.GetType().ToString(), targetType));
            }

            return converter(value);
        }

        public Func<TSource, TTarget> GetConverter<TSource, TTarget>()
        {
            throw new NotImplementedException();
        }

        public Func<object, object> GetConverter(Type sourceType, Type targetType)
        {
            throw new NotImplementedException();
        }
    }
}
