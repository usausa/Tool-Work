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

        [Fact]
        public void ConvertIntToBoolean()
        {
            Assert.False((bool)ObjectConverter.Default.Convert(0, typeof(bool)));
            Assert.True((bool)ObjectConverter.Default.Convert(1, typeof(bool)));
            Assert.True((bool)ObjectConverter.Default.Convert(-1, typeof(bool)));
        }

        [Fact]
        public void ConvertIntToNullableBoolean()
        {
            Assert.False((bool)ObjectConverter.Default.Convert(0, typeof(bool?)));
            Assert.True((bool)ObjectConverter.Default.Convert(1, typeof(bool?)));
            Assert.True((bool)ObjectConverter.Default.Convert(-1, typeof(bool?)));
        }

        [Fact]
        public void ConvertBooleanToInt()
        {
            Assert.Equal(0, ObjectConverter.Default.Convert(false, typeof(int)));
            Assert.Equal(1, ObjectConverter.Default.Convert(true, typeof(int)));
        }

        [Fact]
        public void ConvertBooleanToNullableInt()
        {
            Assert.Equal(0, ObjectConverter.Default.Convert(false, typeof(int?)));
            Assert.Equal(1, ObjectConverter.Default.Convert(true, typeof(int?)));
        }
    }
}
