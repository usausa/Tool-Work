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
        public static IList<IConverterFactory> Create()
        {
            return new List<IConverterFactory>
            {
                new AssignableConverterFactory(),           // IsAssignableFrom
                new CastConverterFactory(),                 // Cast
                // TODO Convert!
                new EnumConverterFactory(),                 // Enum to Enum, String to Enum, Assignable to Enum, Enum to Assignable
                new ConversionOperatorConverterFactory(),   // Implicit/Explicit operator
                new ToStringConverterFactory(),             // ToString finally
            };
        }
    }
}
