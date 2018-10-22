namespace Smart.Converter2.Converters
{
    /// <summary>
    ///
    /// </summary>
    public static class DefaultObjectFactories
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static IConverterFactory[] Create()
        {
            return new IConverterFactory[]
            {
                new DBNullConverterFactory(),               // DBNull
                new AssignableConverterFactory(),           // IsAssignableFrom
                new BooleanConverterFactory(),              // Boolean
                new NumericCastConverterFactory(),          // Numeric cast
                new DecimalConverterFactory(),              // Decimal
                new DateTimeConverterFactory(),             // DateTime/DateTimeOffset
                new EnumConverterFactory(),                 // Enum to Enum, String to Enum, Assignable to Enum, Enum to Assignable
                new ConversionOperatorConverterFactory(),   // Implicit/Explicit operator
                new ToStringConverterFactory(),             // ToString finally
            };
        }
    }
}
