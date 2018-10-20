namespace Smart.Tests
{
    using System;
    using System.Linq;

    using Smart.Converter2;
    using Smart.Converter2.Converters;

    public sealed class TestObjectConverter : IObjectConverter
    {
        private readonly TestConverterFactory[] converterFactories;

        private readonly ObjectConverter objectConverter;

        public TestObjectConverter()
        {
            converterFactories = DefaultObjectFactories.Create().Select(x => new TestConverterFactory(x)).ToArray();
            objectConverter = new ObjectConverter(converterFactories);
        }

        public bool UsedOnly<T>()
            where T : IConverterFactory
        {
            foreach (var factory in converterFactories)
            {
                if (factory.Factory.GetType() == typeof(T))
                {
                    if (!factory.Used)
                    {
                        return false;
                    }
                }
                else
                {
                    if (factory.Used)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public T Convert<T>(object value)
        {
            return objectConverter.Convert<T>(value);
        }

        public object Convert(object value, Type targetType)
        {
            return objectConverter.Convert(value, targetType);
        }

        public Func<object, object> CreateConverter(Type sourceType, Type targetType)
        {
            return objectConverter.CreateConverter(sourceType, targetType);
        }
    }
}
