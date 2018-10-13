namespace Smart.Tests
{
    using System;

    using Smart.Converter2;

    using Xunit;

    public class ConvertTest
    {
        [Fact]
        public void ConvertIntToInt()
        {
            Assert.Equal(0, ObjectConverter.Default.Convert(0, typeof(int)));
            Assert.Equal(Int32.MinValue, ObjectConverter.Default.Convert(Int32.MinValue, typeof(int)));
            Assert.Equal(Int32.MaxValue, ObjectConverter.Default.Convert(Int32.MaxValue, typeof(int)));
        }

        [Fact]
        public void ConvertIntToNullableInt()
        {
            Assert.Equal(0, ObjectConverter.Default.Convert(0, typeof(int?)));
            Assert.Equal(Int32.MinValue, ObjectConverter.Default.Convert(Int32.MinValue, typeof(int?)));
            Assert.Equal(Int32.MaxValue, ObjectConverter.Default.Convert(Int32.MaxValue, typeof(int?)));
        }
    }
}
