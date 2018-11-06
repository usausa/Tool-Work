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

        [Fact]
        public void DateTimeToByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)new DateTime(2000, 1, 1).Ticks, converter.Convert(new DateTime(2000, 1, 1), typeof(byte)));
            Assert.True(converter.UsedIn(typeof(DateTimeConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void DateTimeToNullableByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)new DateTime(2000, 1, 1).Ticks, converter.Convert(new DateTime(2000, 1, 1), typeof(byte?)));
            Assert.True(converter.UsedIn(typeof(DateTimeConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void DateTimeCanNotCovert()
        {
            var converter = new TestObjectConverter();
            Assert.False(converter.CanConvert(typeof(DateTime), typeof(TestStruct)));
        }

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
            Assert.Equal(default(DateTime), converter.Convert(-1L, typeof(DateTime)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void LongToNullableDateTime()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(2000, 1, 1), converter.Convert(new DateTime(2000, 1, 1).Ticks, typeof(DateTime?)));
            Assert.Null(converter.Convert(-1L, typeof(DateTime?)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void ShortToDateTime()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(0L), converter.Convert((short)0, typeof(DateTime)));
            Assert.Equal(default(DateTime), converter.Convert((short)-1, typeof(DateTime)));
            Assert.True(converter.UsedIn(typeof(DateTimeConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void ShortToNullableDateTime()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(0L), converter.Convert((short)0, typeof(DateTime?)));
            Assert.Null(converter.Convert((short)-1, typeof(DateTime?)));
            Assert.True(converter.UsedIn(typeof(DateTimeConverterFactory), typeof(NumericCastConverterFactory)));
        }

        //--------------------------------------------------------------------------------
        // DateTimeOffset to
        //--------------------------------------------------------------------------------

        [Fact]
        public void DateTimeOffsetToString()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)).ToString(), converter.Convert(new DateTimeOffset(new DateTime(2000, 1, 1)), typeof(string)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void DateTimeOffsetToDateTime()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(2000, 1, 1), converter.Convert(new DateTimeOffset(new DateTime(2000, 1, 1)), typeof(DateTime)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void DateTimeOffsetToNullableDateTime()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTime(2000, 1, 1), converter.Convert(new DateTimeOffset(new DateTime(2000, 1, 1)), typeof(DateTime?)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void DateTimeOffsetToLong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)).Ticks, converter.Convert(new DateTimeOffset(new DateTime(2000, 1, 1)), typeof(long)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void DateTimeOffsetToNullableLong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)).Ticks, converter.Convert(new DateTimeOffset(new DateTime(2000, 1, 1)), typeof(long?)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void DateTimeOffsetToByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)new DateTimeOffset(new DateTime(2000, 1, 1)).Ticks, converter.Convert(new DateTimeOffset(new DateTime(2000, 1, 1)), typeof(byte)));
            Assert.True(converter.UsedIn(typeof(DateTimeConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void DateTimeOffsetToNullableByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)new DateTimeOffset(new DateTime(2000, 1, 1)).Ticks, converter.Convert(new DateTimeOffset(new DateTime(2000, 1, 1)), typeof(byte?)));
            Assert.True(converter.UsedIn(typeof(DateTimeConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void DateTimeOffsetCanNotCovert()
        {
            var converter = new TestObjectConverter();
            Assert.False(converter.CanConvert(typeof(DateTimeOffset), typeof(TestStruct)));
        }

        //--------------------------------------------------------------------------------
        // string to DateTimeOffset
        //--------------------------------------------------------------------------------

        [Fact]
        public void StringToDateTimeOffset()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)), converter.Convert("2000/01/01 0:00:00", typeof(DateTimeOffset)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void StringToNullableDateTimeOffset()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)), converter.Convert("2000/01/01 0:00:00", typeof(DateTimeOffset?)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        //--------------------------------------------------------------------------------
        // Numeric to DateTimeOffset
        //--------------------------------------------------------------------------------

        [Fact]
        public void LongToDateTimeOffset()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)), converter.Convert(new DateTimeOffset(new DateTime(2000, 1, 1)).Ticks, typeof(DateTimeOffset)));
            Assert.Equal(default(DateTimeOffset), converter.Convert(-1L, typeof(DateTimeOffset)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void LongToNullableDateTimeOffset()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)), converter.Convert(new DateTimeOffset(new DateTime(2000, 1, 1)).Ticks, typeof(DateTimeOffset?)));
            Assert.Null(converter.Convert(-1L, typeof(DateTimeOffset?)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }

        [Fact]
        public void ShortDateTimeOffset()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(DateTimeOffset.MinValue, converter.Convert((short)DateTimeOffset.MinValue.Ticks, typeof(DateTimeOffset)));
            Assert.Equal(default(DateTimeOffset), converter.Convert((short)-1, typeof(DateTimeOffset)));
            Assert.True(converter.UsedIn(typeof(DateTimeConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void ShortToNullableDateTimeOffset()
        {
            var converter = new TestObjectConverter();
            Assert.Null(converter.Convert((short)-1, typeof(DateTimeOffset?)));
            Assert.True(converter.UsedIn(typeof(DateTimeConverterFactory), typeof(NumericCastConverterFactory)));
        }
    }
}
