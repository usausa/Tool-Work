namespace Smart.Tests
{
    using Smart.Converter2.Converters;

    using Xunit;

    public class ConversionOperatorConverterFactoryTest
    {
        [Fact]
        public void ConvertImplicitToInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(1, converter.Convert<int>(new TestImplicit { Value = 1 }));
            Assert.True(converter.UsedOnly<ConversionOperatorConverterFactory>());
        }

        [Fact]
        public void ConvertImplicitToIntNullable()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(1, converter.Convert<int?>(new TestImplicit { Value = 1 }));
            Assert.True(converter.UsedOnly<ConversionOperatorConverterFactory>());
        }

        [Fact]
        public void ConvertIntToImplicit()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(1, converter.Convert<TestImplicit>(1).Value);
            Assert.True(converter.UsedOnly<ConversionOperatorConverterFactory>());
        }

        [Fact]
        public void ConvertIntToImplicitNullable()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(1, converter.Convert<TestImplicit?>(1)?.Value);
            Assert.True(converter.UsedOnly<ConversionOperatorConverterFactory>());
        }

        [Fact]
        public void ConvertExplicitToInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(1, converter.Convert<int>(new TestExplicit { Value = 1 }));
            Assert.True(converter.UsedOnly<ConversionOperatorConverterFactory>());
        }

        [Fact]
        public void ConvertExplicitToIntNullable()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(1, converter.Convert<int?>(new TestExplicit { Value = 1 }));
            Assert.True(converter.UsedOnly<ConversionOperatorConverterFactory>());
        }

        [Fact]
        public void ConvertIntToExplicit()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(1, converter.Convert<TestExplicit>(1).Value);
            Assert.True(converter.UsedOnly<ConversionOperatorConverterFactory>());
        }

        [Fact]
        public void ConvertIntToExplicitNullable()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(1, converter.Convert<TestExplicit?>(1)?.Value);
            Assert.True(converter.UsedOnly<ConversionOperatorConverterFactory>());
        }
    }
}
