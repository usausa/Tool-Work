namespace Smart.Tests
{
    using System;

    using Smart.Converter2.Converters;

    using Xunit;

    public class DateTimeConvertTest
    {
        //--------------------------------------------------------------------------------
        // DateTime to
        //--------------------------------------------------------------------------------

        [Fact]
        public void DateTimeToString()
        {
            var converter = new TestObjectConverter();
            Assert.Equal("2000/01/01 0:00:00", converter.Convert(new DateTime(2000, 1, 1), typeof(string)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void DateTimeToDateTimeOffset()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)), converter.Convert(new DateTime(2000, 1, 1), typeof(DateTimeOffset)));
            Assert.Equal(default(DateTimeOffset), converter.Convert(new DateTime(0L), typeof(DateTimeOffset)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void DateTimeToNullableDateTimeOffset()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)), converter.Convert(new DateTime(2000, 1, 1), typeof(DateTimeOffset?)));
            Assert.Null(converter.Convert(new DateTime(0L), typeof(DateTimeOffset?)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void DateTimeToLong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(2000, 1, 1).Ticks, converter.Convert(new DateTime(2000, 1, 1), typeof(long)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void DateTimeToNullableLong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(2000, 1, 1).Ticks, converter.Convert(new DateTime(2000, 1, 1), typeof(long?)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        // TODO can convert

        //--------------------------------------------------------------------------------
        // string to DateTime
        //--------------------------------------------------------------------------------

        [Fact]
        public void StringToDateTime()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(2000, 1, 1), converter.Convert("2000/01/01 0:00:00", typeof(DateTime)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void StringToNullableDateTime()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(2000, 1, 1), converter.Convert("2000/01/01 0:00:00", typeof(DateTime?)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        //--------------------------------------------------------------------------------
        // Numeric to DateTime
        //--------------------------------------------------------------------------------

        [Fact]
        public void LongToDateTime()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(2000, 1, 1), converter.Convert(new DateTime(2000, 1, 1).Ticks, typeof(DateTime)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void LongToNullableDateTime()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(2000, 1, 1), converter.Convert(new DateTime(2000, 1, 1).Ticks, typeof(DateTime?)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        // TODO can convert
    }
}
