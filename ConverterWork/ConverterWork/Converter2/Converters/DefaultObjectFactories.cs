namespace Smart.Converter2.Converters
{
    using System.Collections.Generic;

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
                new AssignableConverterFactory(),           // IsAssignableFrom
                new BooleanConverterFactory(),
                new NumericConverterFactory(),
                // TODO Convert!
                new EnumConverterFactory(),                 // Enum to Enum, String to Enum, Assignable to Enum, Enum to Assignable
                new ConversionOperatorConverterFactory(),   // Implicit/Explicit operator
                new ToStringConverterFactory(),             // ToString finally
            };
        }
    }
}
