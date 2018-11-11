namespace Smart.Converter2.Converters
{
    using System;

    public sealed class DateTimeConverterFactory : IConverterFactory
    {
        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            // From DateTime
            if (sourceType == typeof(DateTime))
            {
                // DateTime to String
                if (targetType == typeof(string))
                {
                    return source => ((DateTime)source).ToString();
                }

                var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

                // DateTime to DateTimeOffset(Nullable)
                if (underlyingTargetType == typeof(DateTimeOffset))
                {
                    var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTimeOffset);
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
                if (underlyingTargetType == typeof(long))
                {
                    return source => ((DateTime)source).Ticks;
                }

                // DateTime to can convert from long
                var converter = context.CreateConverter(typeof(long), targetType);
                if (converter != null)
                {
                    return source => converter(((DateTime)source).Ticks);
                }

                return null;
            }

            // From DateTimeOffset
            if (sourceType == typeof(DateTimeOffset))
            {
                // DateTimeOffset to String
                if (targetType == typeof(string))
                {
                    return source => ((DateTimeOffset)source).ToString();
                }

                var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

                // DateTimeOffset to DateTime(Nullable)
                if (underlyingTargetType == typeof(DateTime))
                {
                    return source => ((DateTimeOffset)source).DateTime;
                }

                // DateTimeOffset to long
                if (underlyingTargetType == typeof(long))
                {
                    return source => ((DateTimeOffset)source).Ticks;
                }

                // DateTimeOffset to can convert from long
                var converter = context.CreateConverter(typeof(long), targetType);
                if (converter != null)
                {
                    return source => converter(((DateTimeOffset)source).Ticks);
                }

                return null;
            }

            // From TimeSpan
            if (sourceType == typeof(TimeSpan))
            {
                // TimeSpan to String
                if (targetType == typeof(string))
                {
                    return source => ((TimeSpan)source).ToString();
                }

                var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

                // TimeSpan to long
                if (underlyingTargetType == typeof(long))
                {
                    return source => ((TimeSpan)source).Ticks;
                }

                // TimeSpan to can convert from long
                var converter = context.CreateConverter(typeof(long), targetType);
                if (converter != null)
                {
                    return source => converter(((TimeSpan)source).Ticks);
                }

                return null;
            }

            // From string
            if (sourceType == typeof(string))
            {
                var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

                // String to DateTime(Nullable)
                if (underlyingTargetType == typeof(DateTime))
                {
                    var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTime);
                    return source => DateTime.TryParse((string)source, out var result) ? result : defaultValue;
                }

                // String to DateTimeOffset(Nullable)
                if (underlyingTargetType == typeof(DateTimeOffset))
                {
                    var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTimeOffset);
                    return source => DateTimeOffset.TryParse((string)source, out var result) ? result : defaultValue;
                }

                // String to TimeSpan(Nullable)
                if (underlyingTargetType == typeof(TimeSpan))
                {
                    var defaultValue = targetType.IsNullableType() ? null : (object)default(TimeSpan);
                    return source => TimeSpan.TryParse((string)source, out var result) ? result : defaultValue;
                }

                return null;
            }

            // From long
            if (sourceType == typeof(long))
            {
                var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

                // long to DateTime(Nullable)
                if (underlyingTargetType == typeof(DateTime))
                {
                    var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTime);
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
                if (underlyingTargetType == typeof(DateTimeOffset))
                {
                    var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTimeOffset);
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

                // long to TimeSpan(Nullable)
                if (underlyingTargetType == typeof(TimeSpan))
                {
                    var defaultValue = targetType.IsNullableType() ? null : (object)default(TimeSpan);
                    return source =>
                    {
                        try
                        {
                            return new TimeSpan((long)source);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return defaultValue;
                        }
                    };
                }

                return null;
            }

            // From can convert to long
            var type = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
            if (type == typeof(DateTime))
            {
                // Can convert long to DateTime
                var converter = context.CreateConverter(sourceType, typeof(long));
                if (converter != null)
                {
                    var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTime);
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

            if (type == typeof(DateTimeOffset))
            {
                // Can convert long to DateTimeOffset
                var converter = context.CreateConverter(sourceType, typeof(long));
                if (converter != null)
                {
                    var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTimeOffset);
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

            if (type == typeof(TimeSpan))
            {
                // Can convert long to TimeSpan
                var converter = context.CreateConverter(sourceType, typeof(long));
                if (converter != null)
                {
                    var defaultValue = targetType.IsNullableType() ? null : (object)default(TimeSpan);
                    return source =>
                    {
                        try
                        {
                            return new TimeSpan((long)converter(source));
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
