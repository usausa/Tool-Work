namespace Smart.Converter2.Converters
{
    using System;
    using System.Linq;
    using System.Reflection;

    public sealed class ConversionOperatorConverterFactory : IConverterFactory
    {
        public Func<object, object> GetConverter(in TypePair typePair)
        {
            // TODO mode ?
            var methodInfo = GetImplicitConversionOperator(typePair);
            if (methodInfo != null)
            {
                return source => methodInfo.Invoke(null, new[] { source });
            }

            methodInfo = GetExplicitConversionOperator(typePair);
            if (methodInfo != null)
            {
                return source => methodInfo.Invoke(null, new[] { source });
            }

            return null;
        }

        private static MethodInfo GetImplicitConversionOperator(TypePair typePair)
        {
            var targetType = typePair.TargetType.IsNullableType() ? Nullable.GetUnderlyingType(typePair.TargetType) : typePair.TargetType;

            var sourceTypeMethod = typePair.SourceType
                .GetMethods()
                .FirstOrDefault(mi => mi.IsPublic && mi.IsStatic && mi.Name == "op_Implicit" && mi.ReturnType == targetType);
            return sourceTypeMethod ?? targetType
                .GetMethods()
                .FirstOrDefault(mi => mi.IsPublic && mi.IsStatic && mi.Name == "op_Implicit" && mi.GetParameters().Length == 1 && mi.GetParameters()[0].ParameterType == typePair.SourceType);
        }

        private static MethodInfo GetExplicitConversionOperator(TypePair typePair)
        {
            var targetType = typePair.TargetType.IsNullableType() ? Nullable.GetUnderlyingType(typePair.TargetType) : typePair.TargetType;

            var sourceTypeMethod = typePair.SourceType.GetTypeInfo()
                .DeclaredMethods
                .FirstOrDefault(mi => mi.IsPublic && mi.IsStatic && mi.Name == "op_Explicit" && mi.ReturnType == targetType);
            return sourceTypeMethod ?? targetType.GetTypeInfo()
                .DeclaredMethods
                .FirstOrDefault(mi => mi.IsPublic && mi.IsStatic && mi.Name == "op_Explicit" && mi.GetParameters().Length == 1 && mi.GetParameters()[0].ParameterType == typePair.SourceType);
        }
    }
}
