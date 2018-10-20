namespace Smart.Tests
{
    using System;

    using Xunit;

    public class SameConvertTest
    {
        [Fact]
        public void ConvertBooleanToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(0, typeof(int)));
            Assert.Equal(Int32.MinValue, converter.Convert(Int32.MinValue, typeof(int)));
            Assert.Equal(Int32.MaxValue, converter.Convert(Int32.MaxValue, typeof(int)));
            Assert.True(converter.NotUsed());
        }

        // TODO

        [Fact]
        public void ConvertIntToInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(0, typeof(int)));
            Assert.Equal(Int32.MinValue, converter.Convert(Int32.MinValue, typeof(int)));
            Assert.Equal(Int32.MaxValue, converter.Convert(Int32.MaxValue, typeof(int)));
            Assert.True(converter.NotUsed());
        }

        [Fact]
        public void ConvertIntToNullableInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(0, typeof(int?)));
            Assert.Equal(Int32.MinValue, converter.Convert(Int32.MinValue, typeof(int?)));
            Assert.Equal(Int32.MaxValue, converter.Convert(Int32.MaxValue, typeof(int?)));
            Assert.True(converter.NotUsed());
        }

        // TODO

        [Fact]
        public void ConvertStringToString()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(string.Empty, converter.Convert(string.Empty, typeof(string)));
            Assert.Equal("abc", converter.Convert("abc", typeof(string)));
            Assert.True(converter.NotUsed());
        }
    }
}
