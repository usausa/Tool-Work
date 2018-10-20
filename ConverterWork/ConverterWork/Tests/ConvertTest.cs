namespace Smart.Tests
{
    using System;

    using Smart.Converter2;

    using Xunit;

    // TODO delete
    public class ConvertTest
    {
        [Fact]
        public void ConvertIntToLong()
        {
            Assert.Equal(0L, ObjectConverter.Default.Convert(0, typeof(long)));
            Assert.Equal((long)Int32.MinValue, ObjectConverter.Default.Convert(Int32.MinValue, typeof(long)));
            Assert.Equal((long)Int32.MaxValue, ObjectConverter.Default.Convert(Int32.MaxValue, typeof(long)));
        }

        [Fact]
        public void ConvertIntToNullableLong()
        {
            Assert.Equal(0L, ObjectConverter.Default.Convert(0, typeof(long?)));
            Assert.Equal((long)Int32.MinValue, ObjectConverter.Default.Convert(Int32.MinValue, typeof(long?)));
            Assert.Equal((long)Int32.MaxValue, ObjectConverter.Default.Convert(Int32.MaxValue, typeof(long?)));
        }
    }
}
