namespace Smart.Converter2.Converters
{
    using System;
    using System.Globalization;

    public sealed class EnumConverterFactory : IConverterFactory
    {
        private static readonly Type StringType = typeof(string);

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
                if (sourceType == StringType)
                {
                    return source => Enum.Parse(targetEnumType, (string)source, true);
                }

                // Assignable
                var targetUnderlyingType = Enum.GetUnderlyingType(targetEnumType);
                if (sourceType.IsAssignableFrom(targetUnderlyingType))
                {
                    return source => Enum.ToObject(targetEnumType, source);
                }

                // Not Assignable
                var converter = context.CreateConverter(sourceType, targetUnderlyingType);
                if (converter != null)
                {
                    return source => Enum.ToObject(targetEnumType, converter(source));
                }

                return null;
            }

            if (sourceEnumType != null)
            {
                // Enum to !Enum

                // Enum to String
                if (targetType == StringType)
                {
                    return source => ((Enum)source).ToString();
                }

                // Assignable
                var sourceUnderlyingType = Enum.GetUnderlyingType(sourceEnumType);
                if (targetType.IsAssignableFrom(sourceUnderlyingType))
                {
                    targetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
                    return source => Convert.ChangeType(source, targetType, CultureInfo.CurrentCulture);
                }

                // Not Assignable
                var converter = context.CreateConverter(sourceUnderlyingType, targetType);
                if (converter != null)
                {
                    return source => converter(Convert.ChangeType(source, sourceUnderlyingType, CultureInfo.CurrentCulture));
                }

                return null;
            }

            return null;
        }
    }
}
