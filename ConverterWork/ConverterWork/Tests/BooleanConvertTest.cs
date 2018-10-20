namespace Smart.Tests
{
    using Smart.Converter2.Converters;

    using Xunit;

    public class BooleanConvertTest
    {
        //--------------------------------------------------------------------------------
        // BooleanTo
        //--------------------------------------------------------------------------------

        [Fact]
        public void ConvertBooleanToByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)0, converter.Convert(false, typeof(byte)));
            Assert.Equal((byte)1, converter.Convert(true, typeof(byte)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)0, converter.Convert(false, typeof(byte?)));
            Assert.Equal((byte)1, converter.Convert(true, typeof(byte?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToSByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((sbyte)0, converter.Convert(false, typeof(sbyte)));
            Assert.Equal((sbyte)1, converter.Convert(true, typeof(sbyte)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableSByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((sbyte)0, converter.Convert(false, typeof(sbyte?)));
            Assert.Equal((sbyte)1, converter.Convert(true, typeof(sbyte?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((short)0, converter.Convert(false, typeof(short)));
            Assert.Equal((short)1, converter.Convert(true, typeof(short)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((short)0, converter.Convert(false, typeof(short?)));
            Assert.Equal((short)1, converter.Convert(true, typeof(short?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToUShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((ushort)0, converter.Convert(false, typeof(ushort)));
            Assert.Equal((ushort)1, converter.Convert(true, typeof(ushort)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableUShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((ushort)0, converter.Convert(false, typeof(ushort?)));
            Assert.Equal((ushort)1, converter.Convert(true, typeof(ushort?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(false, typeof(int)));
            Assert.Equal(1, converter.Convert(true, typeof(int)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(false, typeof(int?)));
            Assert.Equal(1, converter.Convert(true, typeof(int?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToUInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0U, converter.Convert(false, typeof(uint)));
            Assert.Equal(1U, converter.Convert(true, typeof(uint)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableUInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0U, converter.Convert(false, typeof(uint?)));
            Assert.Equal(1U, converter.Convert(true, typeof(uint?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToLong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0L, converter.Convert(false, typeof(long)));
            Assert.Equal(1L, converter.Convert(true, typeof(long)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableLong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0L, converter.Convert(false, typeof(long?)));
            Assert.Equal(1L, converter.Convert(true, typeof(long?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToULong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0UL, converter.Convert(false, typeof(ulong)));
            Assert.Equal(1UL, converter.Convert(true, typeof(ulong)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableULong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0UL, converter.Convert(false, typeof(ulong?)));
            Assert.Equal(1UL, converter.Convert(true, typeof(ulong?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToChar()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((char)0, converter.Convert(false, typeof(char)));
            Assert.Equal((char)1, converter.Convert(true, typeof(char)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableChar()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((char)0, converter.Convert(false, typeof(char?)));
            Assert.Equal((char)1, converter.Convert(true, typeof(char?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToDouble()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0d, converter.Convert(false, typeof(double)));
            Assert.Equal(1d, converter.Convert(true, typeof(double)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableDouble()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0d, converter.Convert(false, typeof(double?)));
            Assert.Equal(1d, converter.Convert(true, typeof(double?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToFloat()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0f, converter.Convert(false, typeof(float)));
            Assert.Equal(1f, converter.Convert(true, typeof(float)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableFloat()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0f, converter.Convert(false, typeof(float?)));
            Assert.Equal(1f, converter.Convert(true, typeof(float?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToDecimal()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0m, converter.Convert(false, typeof(decimal)));
            Assert.Equal(1m, converter.Convert(true, typeof(decimal)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertBooleanToNullableDecimal()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0m, converter.Convert(false, typeof(decimal?)));
            Assert.Equal(1m, converter.Convert(true, typeof(decimal?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        //--------------------------------------------------------------------------------
        // ToBoolean
        //--------------------------------------------------------------------------------

        [Fact]
        public void ConvertByteToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert((byte)0, typeof(bool)));
            Assert.True((bool)converter.Convert((byte)1, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertByteToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert((byte)0, typeof(bool?)));
            Assert.True((bool)converter.Convert((byte)1, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertSByteToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert((sbyte)0, typeof(bool)));
            Assert.True((bool)converter.Convert((sbyte)1, typeof(bool)));
            Assert.True((bool)converter.Convert((sbyte)-1, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertSByteToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert((sbyte)0, typeof(bool?)));
            Assert.True((bool)converter.Convert((sbyte)1, typeof(bool?)));
            Assert.True((bool)converter.Convert((sbyte)-1, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertShortToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert((short)0, typeof(bool)));
            Assert.True((bool)converter.Convert((short)1, typeof(bool)));
            Assert.True((bool)converter.Convert((short)-1, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertShortToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert((short)0, typeof(bool?)));
            Assert.True((bool)converter.Convert((short)1, typeof(bool?)));
            Assert.True((bool)converter.Convert((short)-1, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertUShortToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert((short)0, typeof(bool)));
            Assert.True((bool)converter.Convert((short)1, typeof(bool)));
            Assert.True((bool)converter.Convert((short)-1, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertUShortToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert((short)0, typeof(bool?)));
            Assert.True((bool)converter.Convert((short)1, typeof(bool?)));
            Assert.True((bool)converter.Convert((short)-1, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertIntToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0, typeof(bool)));
            Assert.True((bool)converter.Convert(1, typeof(bool)));
            Assert.True((bool)converter.Convert(-1, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertIntToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0, typeof(bool?)));
            Assert.True((bool)converter.Convert(1, typeof(bool?)));
            Assert.True((bool)converter.Convert(-1, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertUIntToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0U, typeof(bool)));
            Assert.True((bool)converter.Convert(1U, typeof(bool)));
            Assert.True((bool)converter.Convert(-1U, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertUIntToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0U, typeof(bool?)));
            Assert.True((bool)converter.Convert(1U, typeof(bool?)));
            Assert.True((bool)converter.Convert(-1U, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertLongToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0L, typeof(bool)));
            Assert.True((bool)converter.Convert(1L, typeof(bool)));
            Assert.True((bool)converter.Convert(-1L, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertLongToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0L, typeof(bool?)));
            Assert.True((bool)converter.Convert(1L, typeof(bool?)));
            Assert.True((bool)converter.Convert(-1L, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertULongToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0L, typeof(bool)));
            Assert.True((bool)converter.Convert(1L, typeof(bool)));
            Assert.True((bool)converter.Convert(-1L, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertULongToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0L, typeof(bool?)));
            Assert.True((bool)converter.Convert(1L, typeof(bool?)));
            Assert.True((bool)converter.Convert(-1L, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertCharToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert((char)0, typeof(bool)));
            Assert.True((bool)converter.Convert((char)1, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertCharToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert((char)0, typeof(bool?)));
            Assert.True((bool)converter.Convert((char)1, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertDoubleToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0d, typeof(bool)));
            Assert.True((bool)converter.Convert(1d, typeof(bool)));
            Assert.True((bool)converter.Convert(-1d, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertDoubleToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0d, typeof(bool?)));
            Assert.True((bool)converter.Convert(1d, typeof(bool?)));
            Assert.True((bool)converter.Convert(-1d, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertFloatToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0f, typeof(bool)));
            Assert.True((bool)converter.Convert(1f, typeof(bool)));
            Assert.True((bool)converter.Convert(-1f, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertFloatToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0f, typeof(bool?)));
            Assert.True((bool)converter.Convert(1f, typeof(bool?)));
            Assert.True((bool)converter.Convert(-1f, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertDecimalToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0m, typeof(bool)));
            Assert.True((bool)converter.Convert(1m, typeof(bool)));
            Assert.True((bool)converter.Convert(-1m, typeof(bool)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }

        [Fact]
        public void ConvertDecimalToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(0m, typeof(bool?)));
            Assert.True((bool)converter.Convert(1m, typeof(bool?)));
            Assert.True((bool)converter.Convert(-1m, typeof(bool?)));
            Assert.True(converter.UsedOnly<BooleanConverterFactory>());
        }
    }
}
