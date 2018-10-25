namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed class DateTimeConverterFactory : IConverterFactory
    {
        // TODO DateTime to num(nullable)
        // TODO DateTime to string
        // TODO DateTime to DateTimeOffset
        // TODO num to DateTime
        // TODO string to DateTime
        // TODO DateTimeOffset to DateTime
        // TODO * 2

        private static readonly Type DateTimeType = typeof(DateTime);
        private static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);
        private static readonly Type StringType = typeof(string);

        // TODO toNumはNullable込みで1つ、FromNumはデフォルトの違いがある！
        private static readonly Dictionary<Tuple<Type, Type>, Func<object, object>> Converters = new Dictionary<Tuple<Type, Type>, Func<object, object>>
        {
            // To DateTimeOffset
            { Tuple.Create(typeof(DateTime), typeof(long)), x => ((DateTime)x).Ticks },
            // From DateTimeOffset
            { Tuple.Create(typeof(DateTimeOffset), typeof(long)), x => ((DateTimeOffset)x).Ticks },

            // TODO defaultの扱い converterをかますか！
            // To DateTime
            { Tuple.Create(typeof(long), typeof(DateTime)), x => { try { return new DateTime((long)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
            { Tuple.Create(typeof(long), typeof(DateTime?)), x => { try { return new DateTime((long)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
            // To DateTimeOffset
            { Tuple.Create(typeof(long), typeof(DateTimeOffset)), x => { try { return new DateTimeOffset(new DateTime((long)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
            { Tuple.Create(typeof(long), typeof(DateTimeOffset?)), x => { try { return new DateTimeOffset(new DateTime((long)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
            // TODO num TZがあるので常に例外は必要
        };

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

                // DateTime to DateTimeOffset(Nullable)
                if (Nullable.GetUnderlyingType(targetType) == DateTimeOffsetType)
                {
                    // TODO 分割？
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

                // TODO num
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

                if (Nullable.GetUnderlyingType(targetType) == DateTimeType)
                {
                    return source => ((DateTimeOffset)source).DateTime;
                }

                // TODO num
                return null;
            }

            if (sourceType == StringType)
            {
                if (Nullable.GetUnderlyingType(targetType) == DateTimeType)
                {
                    // TODO 分割？
                    var defaultValue = targetType.GetDefaultValue();
                    return source => DateTime.TryParse((string)source, out var result) ? result : defaultValue;
                }

                if (Nullable.GetUnderlyingType(targetType) == DateTimeOffsetType)
                {
                    // TODO 分割？
                    var defaultValue = targetType.GetDefaultValue();
                    return source => DateTimeOffset.TryParse((string)source, out var result) ? result : defaultValue;
                }
            }

            // TODO long特殊化？

            // TODO longに変換できる？

            // TODO map with target Nullable.GetUnderlyingType
            if (sourceType.IsValueType && targetType.IsValueType)
            {
                var key = Tuple.Create(sourceType, Nullable.GetUnderlyingType(targetType));
                if (Converters.TryGetValue(key, out var converter))
                {
                    return converter;
                }
            }

            return null;
        }
    }
}
