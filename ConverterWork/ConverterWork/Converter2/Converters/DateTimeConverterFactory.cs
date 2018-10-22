namespace Smart.Converter2.Converters
{
    using System;

    public sealed class DateTimeConverterFactory : IConverterFactory
    {
        private static readonly Type DateTimeType = typeof(DateTime);

        //private static readonly Type NullableDateTimeType = typeof(DateTime?);

        private static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);

        //private static readonly Type NullableDateTimeOffsetType = typeof(DateTimeOffset?);

        private static readonly Type StringType = typeof(string);

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            if (sourceType == DateTimeType)
            {
                // To String
                if (targetType == StringType)
                {
                    return source => ((DateTime)source).ToString();
                }
            }

            if (sourceType == DateTimeOffsetType)
            {
                // To String
                if (targetType == StringType)
                {
                    return source => ((DateTimeOffset)source).ToString();
                }
            }

            // TODO DateTime <-> DateTimeOffset
            // TODO long
            // TODO ulong
            // TODO long, ulong Convert able

            return null;
        }
    }
}
