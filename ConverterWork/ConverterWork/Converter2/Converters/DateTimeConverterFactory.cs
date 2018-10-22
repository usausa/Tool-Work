namespace Smart.Converter2.Converters
{
    using System;

    public sealed class DateTimeConverterFactory : IConverterFactory
    {
        // TODO Dic * 4

        private static readonly Type DateTimeType = typeof(DateTime);

        //private static readonly Type NullableDateTimeType = typeof(DateTime?);

        private static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);

        //private static readonly Type NullableDateTimeOffsetType = typeof(DateTimeOffset?);

        private static readonly Type StringType = typeof(string);

        private static readonly Type LongType = typeof(long);

        //private static readonly Type ULongType = typeof(ulong);

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            if (sourceType == DateTimeType)
            {
                // To String
                if (targetType == StringType)
                {
                    return source => ((DateTime)source).ToString();
                }

                // To Long
                targetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
                if (targetType == LongType)
                {
                    return source => ((DateTime)source).Ticks;
                }

                // TODO long convert able?

                return null;
            }

            if (sourceType == DateTimeOffsetType)
            {
                // To String
                if (targetType == StringType)
                {
                    return source => ((DateTimeOffset)source).ToString();
                }

                // To Long
                targetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
                if (targetType == LongType)
                {
                    return source => ((DateTimeOffset)source).Ticks;
                }

                // TODO long convert able?

                return null;
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
