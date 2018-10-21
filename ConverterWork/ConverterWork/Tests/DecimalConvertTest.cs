namespace Smart.Tests
{
    using System;

    using Smart.Converter2.Converters;

    using Xunit;

    public class DecimalConvertTest
    {
        //--------------------------------------------------------------------------------
        // DecimalTo
        //--------------------------------------------------------------------------------

        [Fact]
        public void DecimalToByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)0, converter.Convert(Decimal.Zero, typeof(byte)));
            Assert.Equal((byte)1, converter.Convert(Decimal.One, typeof(byte)));
            Assert.Equal(default(byte), converter.Convert(Decimal.MaxValue, typeof(byte)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)0, converter.Convert(Decimal.Zero, typeof(byte?)));
            Assert.Equal((byte)1, converter.Convert(Decimal.One, typeof(byte?)));
            Assert.Equal(default(byte?), converter.Convert(Decimal.MaxValue, typeof(byte?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToSByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((sbyte)0, converter.Convert(Decimal.Zero, typeof(sbyte)));
            Assert.Equal((sbyte)1, converter.Convert(Decimal.One, typeof(sbyte)));
            Assert.Equal(default(sbyte), converter.Convert(Decimal.MaxValue, typeof(sbyte)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableSByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((sbyte)0, converter.Convert(Decimal.Zero, typeof(sbyte?)));
            Assert.Equal((sbyte)1, converter.Convert(Decimal.One, typeof(sbyte?)));
            Assert.Equal(default(sbyte?), converter.Convert(Decimal.MaxValue, typeof(sbyte?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((short)0, converter.Convert(Decimal.Zero, typeof(short)));
            Assert.Equal((short)1, converter.Convert(Decimal.One, typeof(short)));
            Assert.Equal(default(short), converter.Convert(Decimal.MaxValue, typeof(short)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((short)0, converter.Convert(Decimal.Zero, typeof(short?)));
            Assert.Equal((short)1, converter.Convert(Decimal.One, typeof(short?)));
            Assert.Equal(default(short?), converter.Convert(Decimal.MaxValue, typeof(short?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToUShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((ushort)0, converter.Convert(Decimal.Zero, typeof(ushort)));
            Assert.Equal((ushort)1, converter.Convert(Decimal.One, typeof(ushort)));
            Assert.Equal(default(ushort), converter.Convert(Decimal.MaxValue, typeof(ushort)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableUShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((ushort)0, converter.Convert(Decimal.Zero, typeof(ushort?)));
            Assert.Equal((ushort)1, converter.Convert(Decimal.One, typeof(ushort?)));
            Assert.Equal(default(ushort?), converter.Convert(Decimal.MaxValue, typeof(ushort?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(Decimal.Zero, typeof(int)));
            Assert.Equal(1, converter.Convert(Decimal.One, typeof(int)));
            Assert.Equal(default(int), converter.Convert(Decimal.MaxValue, typeof(int)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(Decimal.Zero, typeof(int?)));
            Assert.Equal(1, converter.Convert(Decimal.One, typeof(int?)));
            Assert.Equal(default(int?), converter.Convert(Decimal.MaxValue, typeof(int?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToUInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0U, converter.Convert(Decimal.Zero, typeof(uint)));
            Assert.Equal(1U, converter.Convert(Decimal.One, typeof(uint)));
            Assert.Equal(default(uint), converter.Convert(Decimal.MaxValue, typeof(uint)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableUInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0U, converter.Convert(Decimal.Zero, typeof(uint?)));
            Assert.Equal(1U, converter.Convert(Decimal.One, typeof(uint?)));
            Assert.Equal(default(uint?), converter.Convert(Decimal.MaxValue, typeof(uint?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToLong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0L, converter.Convert(Decimal.Zero, typeof(long)));
            Assert.Equal(1L, converter.Convert(Decimal.One, typeof(long)));
            Assert.Equal(default(long), converter.Convert(Decimal.MaxValue, typeof(long)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableLong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0L, converter.Convert(Decimal.Zero, typeof(long?)));
            Assert.Equal(1L, converter.Convert(Decimal.One, typeof(long?)));
            Assert.Equal(default(long?), converter.Convert(Decimal.MaxValue, typeof(long?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToULong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0UL, converter.Convert(Decimal.Zero, typeof(ulong)));
            Assert.Equal(1UL, converter.Convert(Decimal.One, typeof(ulong)));
            Assert.Equal(default(ulong), converter.Convert(Decimal.MaxValue, typeof(ulong)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableULong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0UL, converter.Convert(Decimal.Zero, typeof(ulong?)));
            Assert.Equal(1UL, converter.Convert(Decimal.One, typeof(ulong?)));
            Assert.Equal(default(ulong?), converter.Convert(Decimal.MaxValue, typeof(ulong?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToChar()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((char)0, converter.Convert(Decimal.Zero, typeof(char)));
            Assert.Equal((char)1, converter.Convert(Decimal.One, typeof(char)));
            Assert.Equal(default(char), converter.Convert(Decimal.MaxValue, typeof(char)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableChar()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((char)0, converter.Convert(Decimal.Zero, typeof(char?)));
            Assert.Equal((char)1, converter.Convert(Decimal.One, typeof(char?)));
            Assert.Equal(default(char?), converter.Convert(Decimal.MaxValue, typeof(char?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToDouble()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0d, converter.Convert(Decimal.Zero, typeof(double)));
            Assert.Equal(1d, converter.Convert(Decimal.One, typeof(double)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableDouble()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0d, converter.Convert(Decimal.Zero, typeof(double?)));
            Assert.Equal(1d, converter.Convert(Decimal.One, typeof(double?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToFloat()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0f, converter.Convert(Decimal.Zero, typeof(float)));
            Assert.Equal(1f, converter.Convert(Decimal.One, typeof(float)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }

        [Fact]
        public void DecimalToNullableFloat()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0f, converter.Convert(Decimal.Zero, typeof(float?)));
            Assert.Equal(1f, converter.Convert(Decimal.One, typeof(float?)));
            Assert.True(converter.UsedOnly<DecimalConverterFactory>());
        }
    }
}
