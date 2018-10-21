namespace Smart.Tests
{
    using Smart.Converter2.Converters;

    using Xunit;

    public class AssignableConvertTest
    {
        [Fact]
        public void Assignable()
        {
            var converter = new TestObjectConverter();
            var instance = new TestDeliveredClass();
            Assert.Same(instance, converter.Convert(instance, typeof(TestBaseClass)));
            Assert.True(converter.UsedOnly<AssignableConverterFactory>());
        }
    }
}
