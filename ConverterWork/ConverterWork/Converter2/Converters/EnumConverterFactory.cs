namespace Smart.Converter2.Converters
{
    using System;
    using System.Globalization;

    public sealed class EnumConverterFactory : IConverterFactory
    {
        private static readonly Type StringType = typeof(string);

        public Func<object, object> GetConverter(in TypePair typePair)
        {
            var sourceEnumType = typePair.SourceType.GetEnumType();
            var targetEnumType = typePair.TargetType.GetEnumType();

            if ((sourceEnumType != null) && (targetEnumType != null))
            {
                // Enum to Enum
                return source => Enum.ToObject(targetEnumType, source);
            }

            if (targetEnumType != null)
            {
                // !Enum to Enum

                // String to Enum
                if (typePair.SourceType == StringType)
                {
                    return source => Enum.Parse(targetEnumType, (string)source, true);
                }

                // Assignable
                if (typePair.SourceType.IsAssignableFrom(Enum.GetUnderlyingType(targetEnumType)))
                {
                    return source => Enum.ToObject(targetEnumType, source);
                }

                return null;
            }

            if (sourceEnumType != null)
            {
                // Enum to !Enum

                // Assignable
                if (typePair.TargetType.IsAssignableFrom(Enum.GetUnderlyingType(sourceEnumType)))
                {
                    var targetType = typePair.TargetType.IsNullableType() ? Nullable.GetUnderlyingType(typePair.TargetType) : typePair.TargetType;
                    return source => Convert.ChangeType(source, targetType, CultureInfo.CurrentCulture);
                }

                return null;
            }

            return null;
        }
    }
}
