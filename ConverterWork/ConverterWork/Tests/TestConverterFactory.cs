namespace Smart.Tests
{
    using System;

    using Smart.Converter2;

    public sealed class TestConverterFactory : IConverterFactory
    {
        public IConverterFactory Factory { get; }

        public bool Used { get; set; }

        public TestConverterFactory(IConverterFactory factory)
        {
            Factory = factory;
        }

        public Func<object, object> GetConverter(Type sourceType, Type targetType)
        {
            var converter = Factory.GetConverter(sourceType, targetType);
            if (converter != null)
            {
                Used = true;
            }

            return converter;
        }
    }
}
