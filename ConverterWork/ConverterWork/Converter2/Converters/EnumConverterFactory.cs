namespace Smart.Converter2.Converters
{
    using System;
    using System.Globalization;

    public sealed class EnumConverterFactory : IConverterFactory
    {
        private static readonly Type StringType = typeof(string);

        public Func<object, object> GetConverter(Type sourceType, Type targetType)
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
                if (sourceType == StringType)
                {
                    return source => Enum.Parse(targetEnumType, (string)source, true);
                }

                // Assignable
                if (sourceType.IsAssignableFrom(Enum.GetUnderlyingType(targetEnumType)))
                {
                    return source => Enum.ToObject(targetEnumType, source);
                }

                return null;
            }

            if (sourceEnumType != null)
            {
                // Enum to !Enum

                // Assignable
                if (targetType.IsAssignableFrom(Enum.GetUnderlyingType(sourceEnumType)))
                {
                    targetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
                    return source => Convert.ChangeType(source, targetType, CultureInfo.CurrentCulture);
                }

                return null;
            }

            return null;
        }
    }
}
