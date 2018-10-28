namespace Smart.Converter2.Converters
{
    using System;

    public sealed class DateTimeConverterFactory : IConverterFactory
    {
        private static readonly Type DateTimeType = typeof(DateTime);
        private static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);
        private static readonly Type StringType = typeof(string);
        private static readonly Type LongType = typeof(long);

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            // From DateTime
            if (sourceType == DateTimeType)
            {
                // DateTime to String
                if (targetType == StringType)
                {
                    return source => ((DateTime)source).ToString();
                }

                var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

                // DateTime to DateTimeOffset(Nullable)
                if (underlyingTargetType == DateTimeOffsetType)
                {
                    var defaultValue = targetType.GetDefaultValue();
                    return source =>
                    {
                        try
                        {
                            return new DateTimeOffset((DateTime)source);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return defaultValue;
                        }
                    };
                }

                // DateTime to long
                if (underlyingTargetType == LongType)
                {
                    return source => ((DateTime)source).Ticks;
                }

                // DateTime to can convert from long
                var converter = context.CreateConverter(LongType, targetType);
                if (converter != null)
                {
                    return source => converter(((DateTime)source).Ticks);
                }

                return null;
            }

            // From DateTimeOffset
            if (sourceType == DateTimeOffsetType)
            {
                // DateTimeOffset to String
                if (targetType == StringType)
                {
                    return source => ((DateTimeOffset)source).ToString();
                }

                var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

                // DateTimeOffset to DateTime(Nullable)
                if (underlyingTargetType == DateTimeType)
                {
                    return source => ((DateTimeOffset)source).DateTime;
                }

                // DateTimeOffset to long
                if (underlyingTargetType == LongType)
                {
                    return source => ((DateTimeOffset)source).Ticks;
                }

                // DateTimeOffset to can convert from long
                var converter = context.CreateConverter(LongType, targetType);
                if (converter != null)
                {
                    return source => converter(((DateTimeOffset)source).Ticks);
                }

                return null;
            }

            // From string
            if (sourceType == StringType)
            {
                var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

                // String to DateTime(Nullable)
                if (underlyingTargetType == DateTimeType)
                {
                    var defaultValue = targetType.GetDefaultValue();
                    return source => DateTime.TryParse((string)source, out var result) ? result : defaultValue;
                }

                // String to DateTimeOffset(Nullable)
                if (underlyingTargetType == DateTimeOffsetType)
                {
                    var defaultValue = targetType.GetDefaultValue();
                    return source => DateTimeOffset.TryParse((string)source, out var result) ? result : defaultValue;
                }
            }

            // From long
            if (sourceType == LongType)
            {
                var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

                // long to DateTime(Nullable)
                if (underlyingTargetType == DateTimeType)
                {
                    var defaultValue = targetType.GetDefaultValue();
                    return source =>
                    {
                        try
                        {
                            return new DateTime((long)source);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return defaultValue;
                        }
                    };
                }

                // long to DateTimeOffset(Nullable)
                if (underlyingTargetType == DateTimeOffsetType)
                {
                    var defaultValue = targetType.GetDefaultValue();
                    return source =>
                    {
                        try
                        {
                            return new DateTimeOffset(new DateTime((long)source));
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return defaultValue;
                        }
                    };
                }
            }

            // From can convert to long
            var type = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
            if (type == DateTimeType)
            {
                // Can convert long to DateTime
                var converter = context.CreateConverter(sourceType, LongType);
                if (converter != null)
                {
                    var defaultValue = targetType.GetDefaultValue();
                    return source =>
                    {
                        try
                        {
                            return new DateTime((long)converter(source));
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return defaultValue;
                        }
                    };
                }
            }

            if (type == DateTimeOffsetType)
            {
                // Can convert long to DateTime
                var converter = context.CreateConverter(sourceType, LongType);
                if (converter != null)
                {
                    var defaultValue = targetType.GetDefaultValue();
                    return source =>
                    {
                        try
                        {
                            return new DateTimeOffset(new DateTime((long)converter(source)));
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return defaultValue;
                        }
                    };
                }
            }

            return null;
        }
    }
}
