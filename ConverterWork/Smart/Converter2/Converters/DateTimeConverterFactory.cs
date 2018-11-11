namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed class DateTimeConverterFactory : IConverterFactory
    {
        private static readonly Dictionary<Type, Func<object, object>> DateTimeTickConverter = new Dictionary<Type, Func<object, object>>
        {
            { typeof(byte), source => (byte)((DateTime)source).Ticks },
            { typeof(sbyte), source => (sbyte)((DateTime)source).Ticks },
            { typeof(short), source => (short)((DateTime)source).Ticks },
            { typeof(ushort), source => (ushort)((DateTime)source).Ticks },
            { typeof(int), source => (int)((DateTime)source).Ticks },
            { typeof(uint), source => (uint)((DateTime)source).Ticks },
            { typeof(long), source => ((DateTime)source).Ticks },
            { typeof(ulong), source => (ulong)((DateTime)source).Ticks },
            { typeof(char), source => (char)((DateTime)source).Ticks },
            { typeof(double), source => (double)((DateTime)source).Ticks },
            { typeof(float), source => (float)((DateTime)source).Ticks }
        };

        private static readonly Dictionary<Type, Func<object, object>> DateTimeOffsetTickConverter = new Dictionary<Type, Func<object, object>>
        {
            { typeof(byte), source => (byte)((DateTimeOffset)source).Ticks },
            { typeof(sbyte), source => (sbyte)((DateTimeOffset)source).Ticks },
            { typeof(short), source => (short)((DateTimeOffset)source).Ticks },
            { typeof(ushort), source => (ushort)((DateTimeOffset)source).Ticks },
            { typeof(int), source => (int)((DateTimeOffset)source).Ticks },
            { typeof(uint), source => (uint)((DateTimeOffset)source).Ticks },
            { typeof(long), source => ((DateTimeOffset)source).Ticks },
            { typeof(ulong), source => (ulong)((DateTimeOffset)source).Ticks },
            { typeof(char), source => (char)((DateTimeOffset)source).Ticks },
            { typeof(double), source => (double)((DateTimeOffset)source).Ticks },
            { typeof(float), source => (float)((DateTimeOffset)source).Ticks }
        };

        private static readonly Dictionary<Type, Func<object, object>> TimeSpanTickConverter = new Dictionary<Type, Func<object, object>>
        {
            { typeof(byte), source => (byte)((TimeSpan)source).Ticks },
            { typeof(sbyte), source => (sbyte)((TimeSpan)source).Ticks },
            { typeof(short), source => (short)((TimeSpan)source).Ticks },
            { typeof(ushort), source => (ushort)((TimeSpan)source).Ticks },
            { typeof(int), source => (int)((TimeSpan)source).Ticks },
            { typeof(uint), source => (uint)((TimeSpan)source).Ticks },
            { typeof(long), source => ((TimeSpan)source).Ticks },
            { typeof(ulong), source => (ulong)((TimeSpan)source).Ticks },
            { typeof(char), source => (char)((TimeSpan)source).Ticks },
            { typeof(double), source => (double)((TimeSpan)source).Ticks },
            { typeof(float), source => (float)((TimeSpan)source).Ticks }
        };

        // TODO

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

                // DateTime to numeric
                if (DateTimeTickConverter.TryGetValue(underlyingTargetType, out var converter))
                {
                    return converter;
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

                // DateTimeOffset to numeric
                if (DateTimeOffsetTickConverter.TryGetValue(underlyingTargetType, out var converter))
                {
                    return converter;
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

                // TimeSpan to numeric
                if (TimeSpanTickConverter.TryGetValue(underlyingTargetType, out var converter))
                {
                    return converter;
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

            // TODO underlyingTargetTypeベースで3numericにする
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

            // TODO *3
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
