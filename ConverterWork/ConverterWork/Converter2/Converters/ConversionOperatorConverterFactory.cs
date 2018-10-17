namespace Smart.Converter2.Converters
{
    using System;
    using System.Linq;
    using System.Reflection;

    public sealed class ConversionOperatorConverterFactory : IConverterFactory
    {
        public Func<object, object> GetConverter(Type sourceType, Type targetType)
        {
            var methodInfo = GetImplicitConversionOperator(sourceType, targetType);
            if (methodInfo != null)
            {
                return source => methodInfo.Invoke(null, new[] { source });
            }

            methodInfo = GetExplicitConversionOperator(sourceType, targetType);
            if (methodInfo != null)
            {
                return source => methodInfo.Invoke(null, new[] { source });
            }

            return null;
        }

        private static MethodInfo GetImplicitConversionOperator(Type sourceType, Type targetType)
        {
            targetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

            var sourceTypeMethod = sourceType
                .GetMethods()
                .FirstOrDefault(mi => mi.IsPublic && mi.IsStatic && mi.Name == "op_Implicit" && mi.ReturnType == targetType);
            return sourceTypeMethod ?? targetType
                .GetMethods()
                .FirstOrDefault(mi => mi.IsPublic && mi.IsStatic && mi.Name == "op_Implicit" && mi.GetParameters().Length == 1 && mi.GetParameters()[0].ParameterType == sourceType);
        }

        private static MethodInfo GetExplicitConversionOperator(Type sourceType, Type targetType)
        {
            targetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

            var sourceTypeMethod = sourceType
                .GetMethods()
                .FirstOrDefault(mi => mi.IsPublic && mi.IsStatic && mi.Name == "op_Explicit" && mi.ReturnType == targetType);
            return sourceTypeMethod ?? targetType
                .GetMethods()
                .FirstOrDefault(mi => mi.IsPublic && mi.IsStatic && mi.Name == "op_Explicit" && mi.GetParameters().Length == 1 && mi.GetParameters()[0].ParameterType == sourceType);
        }
    }
}
