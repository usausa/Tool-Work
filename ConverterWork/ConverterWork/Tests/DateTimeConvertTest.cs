namespace Smart.Tests
{
    using System;

    using Smart.Converter2.Converters;

    using Xunit;

    public class DateTimeConvertTest
    {
        [Fact]
        public void DateTimeToString()
        {
            var converter = new TestObjectConverter();
            Assert.Equal("2000/01/01 0:00:00", converter.Convert(new DateTime(2000, 1, 1), typeof(string)));
            Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
        }
    }
}
