namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed class DateTimeConverterFactory : IConverterFactory
    {
        private static readonly Dictionary<Tuple<Type, Type>, Func<object, object>> Converters = new Dictionary<Tuple<Type, Type>, Func<object, object>>
        {
            // To DateTimeOffset
            { Tuple.Create(typeof(DateTime), typeof(long)), x => ((DateTime)x).Ticks },
            { Tuple.Create(typeof(DateTime), typeof(long?)), x => ((DateTime)x).Ticks },
            // TODO num
            { Tuple.Create(typeof(DateTime), typeof(string)), x => ((DateTime)x).ToString() },
            { Tuple.Create(typeof(DateTime), typeof(DateTimeOffset)), x => new DateTimeOffset((DateTime)x) },
            { Tuple.Create(typeof(DateTime), typeof(DateTimeOffset?)), x => new DateTimeOffset((DateTime)x) },
            // From DateTimeOffset
            { Tuple.Create(typeof(DateTimeOffset), typeof(long)), x => ((DateTimeOffset)x).Ticks },
            { Tuple.Create(typeof(DateTimeOffset), typeof(long?)), x => ((DateTimeOffset)x).Ticks },
            // TODO num
            { Tuple.Create(typeof(DateTimeOffset), typeof(string)), x => ((DateTimeOffset)x).ToString() },
            { Tuple.Create(typeof(DateTimeOffset), typeof(DateTime)), x => ((DateTimeOffset)x).DateTime },
            { Tuple.Create(typeof(DateTimeOffset), typeof(DateTime?)), x => ((DateTimeOffset)x).DateTime },
            // To DateTime
            { Tuple.Create(typeof(long), typeof(DateTime)), x => { try { return new DateTime((long)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
            { Tuple.Create(typeof(long), typeof(DateTime?)), x => { try { return new DateTime((long)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
            // TODO num uのint以下は安全
            { Tuple.Create(typeof(string), typeof(DateTime)), x => DateTime.TryParse((string)x, out var result) ? result : default },
            { Tuple.Create(typeof(string), typeof(DateTime?)), x => DateTime.TryParse((string)x, out var result) ? result : default(DateTime?) },
            // To DateTimeOffset
            { Tuple.Create(typeof(long), typeof(DateTimeOffset)), x => { try { return new DateTimeOffset(new DateTime((long)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
            { Tuple.Create(typeof(long), typeof(DateTimeOffset?)), x => { try { return new DateTimeOffset(new DateTime((long)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
            // TODO num TZがあるので常に例外は必要
            { Tuple.Create(typeof(string), typeof(DateTimeOffset)), x => DateTimeOffset.TryParse((string)x, out var result) ? result : default(DateTimeOffset) },
            { Tuple.Create(typeof(string), typeof(DateTimeOffset?)), x => DateTimeOffset.TryParse((string)x, out var result) ? result : default(DateTimeOffset?) },
        };

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            var key = Tuple.Create(sourceType, targetType);
            if (Converters.TryGetValue(key, out var converter))
            {
                return converter;
            }

            return null;
        }
    }
}
