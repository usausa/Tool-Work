namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed class DBNullConverterFactory : IConverterFactory
    {
        private static readonly Type DBNullType = typeof(DBNull);

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            if (sourceType == DBNullType)
            {
                var defaultValue = targetType.GetDefaultValue();
                return source => defaultValue;
            }

            return null;
        }
    }
}
