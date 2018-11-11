namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public sealed class EnumConverterFactory : IConverterFactory
    {
        private static readonly HashSet<Type> UnderlyingTypes = new HashSet<Type>
        {
            { typeof(byte) },
            { typeof(sbyte) },
            { typeof(short) },
            { typeof(ushort) },
            { typeof(int) },
            { typeof(uint) },
            { typeof(long) },
            { typeof(ulong) }
        };

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            var sourceEnumType = sourceType.GetEnumType();
            var targetEnumType = targetType.GetEnumType();

            if ((sourceEnumType != null) && (targetEnumType != null))
            {
                // Enum to Enum
                return source => Enum.ToObject(targetEnumType, source);
            }

            if (targetEnumType != null)
            {
                // !Enum to Enum

                // String to Enum
                if (sourceType == typeof(string))
                {
                    return ((IConverter)Activator.CreateInstance(typeof(StringToEnumConverter<>).MakeGenericType(targetEnumType))).Convert;
                }

                // Assignable
                if (UnderlyingTypes.Contains(sourceType))
                {
                    var targetUnderlyingType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
                    return source => Enum.ToObject(targetUnderlyingType, source);
                }

                return null;
            }

            if (sourceEnumType != null)
            {
                // Enum to !Enum

                // Enum to String
                if (targetType == typeof(string))
                {
                    return ((IConverter)Activator.CreateInstance(typeof(EnumToStringConverter<>).MakeGenericType(sourceEnumType))).Convert;
                }

                // Enum to Numeric
                var targetUnderlyingType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
                if (UnderlyingTypes.Contains(targetUnderlyingType))
                {
                    return source => Convert.ChangeType(source, targetUnderlyingType, CultureInfo.CurrentCulture);
                }

                return null;
            }

            return null;
        }

        private sealed class EnumToStringConverter<T> : IConverter
            where T : struct
        {
            public object Convert(object source)
            {
                return EnumHelper<T>.GetName((T)source);
            }
        }

        private sealed class StringToEnumConverter<T> : IConverter
            where T : struct
        {
            public object Convert(object source)
            {
                return EnumHelper<T>.TryParseValue((string)source, out var value) ? value : default(T);
            }
        }
    }
}
