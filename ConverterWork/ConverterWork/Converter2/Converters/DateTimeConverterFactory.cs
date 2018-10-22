namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed class DateTimeConverterFactory : IConverterFactory
    {
        // TODO Tuple
        private static readonly Dictionary<Type, Func<object, object>> FromDateTimeConverters = new Dictionary<Type, Func<object, object>>
        {
            { typeof(long), x => ((DateTime)x).Ticks },
            { typeof(long?), x => ((DateTime)x).Ticks },
            { typeof(string), x => ((DateTime)x).ToString() },
            { typeof(DateTimeOffset), x => new DateTimeOffset((DateTime)x) },
            { typeof(DateTimeOffset?), x => new DateTimeOffset((DateTime)x) }
        };

        private static readonly Dictionary<Type, Func<object, object>> FromDateTimeOffsetConverters = new Dictionary<Type, Func<object, object>>
        {
            { typeof(long), x => ((DateTimeOffset)x).Ticks },
            { typeof(long?), x => ((DateTimeOffset)x).Ticks },
            { typeof(string), x => ((DateTimeOffset)x).ToString() },
            { typeof(DateTimeOffset), x => ((DateTimeOffset)x).DateTime },
            { typeof(DateTimeOffset?), x => ((DateTimeOffset)x).DateTime }
        };

        private static readonly Dictionary<Type, Func<object, object>> ToDateTimeConverters = new Dictionary<Type, Func<object, object>>
        {
            { typeof(long), x => { try { return new DateTime((long)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
            { typeof(string), x => DateTime.TryParse((string)x, out var result) ? result : default }
        };

        private static readonly Dictionary<Type, Func<object, object>> ToNullableDateTimeConverters = new Dictionary<Type, Func<object, object>>
        {
            { typeof(long), x => { try { return new DateTime((long)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
            { typeof(string), x => DateTime.TryParse((string)x, out var result) ? result : default }
        };

        // TODO Dic * 2 ToNullable

        private static readonly Type DateTimeType = typeof(DateTime);

        private static readonly Type NullableDateTimeType = typeof(DateTime?);

        private static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);

        private static readonly Type NullableDateTimeOffsetType = typeof(DateTimeOffset?);

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            if (sourceType == DateTimeType)
            {
                if (FromDateTimeConverters.TryGetValue(targetType, out var converter))
                {
                    return converter;
                }

                // TODO able?
            }
            else if (sourceType == DateTimeOffsetType)
            {
                if (FromDateTimeOffsetConverters.TryGetValue(targetType, out var converter))
                {
                    return converter;
                }

                // TODO able?
            }
            else if ((targetType == DateTimeType) || (targetType == NullableDateTimeType))
            {

            }
            else if ((targetType == DateTimeOffsetType) || (targetType == NullableDateTimeOffsetType))
            {

            }

            //targetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
            //if (targetType == DateTimeType)
            //{
            //    // From String
            //    if (sourceType == StringType)
            //    {
            //        var defaultValue = targetType
            //        return source => DateTime.TryParse((string)source, out var result) ? result : ;
            //    }

            //}

            // TODO DateTime <-> DateTimeOffset
            // TODO long
            // TODO ulong
            // TODO long, ulong Convert able

            return null;
        }
    }
}
