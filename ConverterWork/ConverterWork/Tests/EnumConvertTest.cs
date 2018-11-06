namespace Smart.Tests
{
    using Smart.Converter2.Converters;

    using Xunit;

    public class EnumConvertTest
    {
        //--------------------------------------------------------------------------------
        // EnumToEnum
        //--------------------------------------------------------------------------------

        [Fact]
        public void EnumToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum2.Zero, converter.Convert(TestEnum.Zero, typeof(TestEnum2)));
            Assert.True(converter.UsedOnly<EnumConverterFactory>());
        }

        //--------------------------------------------------------------------------------
        // NotEnumToEnum
        //--------------------------------------------------------------------------------

        [Fact]
        public void BooleanToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(false, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert(true, typeof(TestEnum)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(BooleanConverterFactory)));
        }

        [Fact]
        public void BooleanToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(false, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert(true, typeof(TestEnum?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(BooleanConverterFactory)));
        }

        [Fact]
        public void ByteToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert((byte)0, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert((byte)1, typeof(TestEnum)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void ByteToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert((byte)0, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert((byte)1, typeof(TestEnum?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void SByteToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert((sbyte)0, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert((sbyte)1, typeof(TestEnum)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void SByteToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert((sbyte)0, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert((sbyte)1, typeof(TestEnum?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void ShortToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert((short)0, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert((short)1, typeof(TestEnum)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void ShortToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert((short)0, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert((short)1, typeof(TestEnum?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void UShortToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert((ushort)0, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert((ushort)1, typeof(TestEnum)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void UShortToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert((ushort)0, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert((ushort)1, typeof(TestEnum?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void IntToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(0, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert(1, typeof(TestEnum)));
            Assert.True(converter.UsedOnly<EnumConverterFactory>());
        }

        [Fact]
        public void IntToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(0, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert(1, typeof(TestEnum?)));
            Assert.True(converter.UsedOnly<EnumConverterFactory>());
        }

        [Fact]
        public void UIntToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(0U, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert(1U, typeof(TestEnum)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void UIntToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(0U, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert(1U, typeof(TestEnum?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void CharToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert((char)0, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert((char)1, typeof(TestEnum)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void CharToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert((char)0, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert((char)1, typeof(TestEnum?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void DoubleToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(0d, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert(1d, typeof(TestEnum)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void DoubleToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(0d, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert(1d, typeof(TestEnum?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void FloatToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(0f, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert(1f, typeof(TestEnum)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void FloatToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(0f, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert(1f, typeof(TestEnum?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void DecimalToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(0m, typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert(1m, typeof(TestEnum)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(DecimalConverterFactory)));
        }

        [Fact]
        public void DecimalToNullableEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert(0m, typeof(TestEnum?)));
            Assert.Equal(TestEnum.One, converter.Convert(1m, typeof(TestEnum?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(DecimalConverterFactory)));
        }

        [Fact]
        public void StringToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(TestEnum.Zero, converter.Convert("Zero", typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert("One", typeof(TestEnum)));
            Assert.Equal(TestEnum.Zero, converter.Convert("0", typeof(TestEnum)));
            Assert.Equal(TestEnum.One, converter.Convert("1", typeof(TestEnum)));
            Assert.True(converter.UsedOnly<EnumConverterFactory>());
        }

        [Fact]
        public void CanNotConvertToEnum()
        {
            var converter = new TestObjectConverter();
            Assert.False(converter.CanConvert(typeof(TestStruct), typeof(TestEnum)));
        }

        //--------------------------------------------------------------------------------
        // EnumToNotEnum
        //--------------------------------------------------------------------------------

        [Fact]
        public void EnumToBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(TestEnum.Zero, typeof(bool)));
            Assert.True((bool)converter.Convert(TestEnum.One, typeof(bool)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(BooleanConverterFactory)));
        }

        [Fact]
        public void EnumToNullableBoolean()
        {
            var converter = new TestObjectConverter();
            Assert.False((bool)converter.Convert(TestEnum.Zero, typeof(bool?)));
            Assert.True((bool)converter.Convert(TestEnum.One, typeof(bool?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(BooleanConverterFactory)));
        }

        [Fact]
        public void EnumToByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)0, converter.Convert(TestEnum.Zero, typeof(byte)));
            Assert.Equal((byte)1, converter.Convert(TestEnum.One, typeof(byte)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToNullableByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)0, converter.Convert(TestEnum.Zero, typeof(byte?)));
            Assert.Equal((byte)1, converter.Convert(TestEnum.One, typeof(byte?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToSByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((sbyte)0, converter.Convert(TestEnum.Zero, typeof(sbyte)));
            Assert.Equal((sbyte)1, converter.Convert(TestEnum.One, typeof(sbyte)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToNullableSByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((sbyte)0, converter.Convert(TestEnum.Zero, typeof(sbyte?)));
            Assert.Equal((sbyte)1, converter.Convert(TestEnum.One, typeof(sbyte?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((short)0, converter.Convert(TestEnum.Zero, typeof(short)));
            Assert.Equal((short)1, converter.Convert(TestEnum.One, typeof(short)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToNullableShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((short)0, converter.Convert(TestEnum.Zero, typeof(short?)));
            Assert.Equal((short)1, converter.Convert(TestEnum.One, typeof(short?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToUShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((ushort)0, converter.Convert(TestEnum.Zero, typeof(ushort)));
            Assert.Equal((ushort)1, converter.Convert(TestEnum.One, typeof(ushort)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToNullableUShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((ushort)0, converter.Convert(TestEnum.Zero, typeof(ushort?)));
            Assert.Equal((ushort)1, converter.Convert(TestEnum.One, typeof(ushort?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(TestEnum.Zero, typeof(int)));
            Assert.Equal(1, converter.Convert(TestEnum.One, typeof(int)));
            Assert.True(converter.UsedOnly<EnumConverterFactory>());
        }

        [Fact]
        public void EnumToNullableInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(TestEnum.Zero, typeof(int?)));
            Assert.Equal(1, converter.Convert(TestEnum.One, typeof(int?)));
            Assert.True(converter.UsedOnly<EnumConverterFactory>());
        }

        [Fact]
        public void EnumToLong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0L, converter.Convert(TestEnum.Zero, typeof(long)));
            Assert.Equal(1L, converter.Convert(TestEnum.One, typeof(long)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToNullableLong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0L, converter.Convert(TestEnum.Zero, typeof(long?)));
            Assert.Equal(1L, converter.Convert(TestEnum.One, typeof(long?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToULong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0UL, converter.Convert(TestEnum.Zero, typeof(ulong)));
            Assert.Equal(1UL, converter.Convert(TestEnum.One, typeof(ulong)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToNullableULong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0UL, converter.Convert(TestEnum.Zero, typeof(ulong?)));
            Assert.Equal(1UL, converter.Convert(TestEnum.One, typeof(ulong?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToChar()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((char)0, converter.Convert(TestEnum.Zero, typeof(char)));
            Assert.Equal((char)1, converter.Convert(TestEnum.One, typeof(char)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToNullableChar()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((char)0, converter.Convert(TestEnum.Zero, typeof(char?)));
            Assert.Equal((char)1, converter.Convert(TestEnum.One, typeof(char?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToDouble()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0d, converter.Convert(TestEnum.Zero, typeof(double)));
            Assert.Equal(1d, converter.Convert(TestEnum.One, typeof(double)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToNullableDouble()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0d, converter.Convert(TestEnum.Zero, typeof(double?)));
            Assert.Equal(1d, converter.Convert(TestEnum.One, typeof(double?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToFloat()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0f, converter.Convert(TestEnum.Zero, typeof(float)));
            Assert.Equal(1f, converter.Convert(TestEnum.One, typeof(float)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToNullableFloat()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0f, converter.Convert(TestEnum.Zero, typeof(float?)));
            Assert.Equal(1f, converter.Convert(TestEnum.One, typeof(float?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(NumericCastConverterFactory)));
        }

        [Fact]
        public void EnumToDecimal()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0m, converter.Convert(TestEnum.Zero, typeof(decimal)));
            Assert.Equal(1m, converter.Convert(TestEnum.One, typeof(decimal)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(DecimalConverterFactory)));
        }

        [Fact]
        public void EnumToNullableDecimal()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0m, converter.Convert(TestEnum.Zero, typeof(decimal?)));
            Assert.Equal(1m, converter.Convert(TestEnum.One, typeof(decimal?)));
            Assert.True(converter.UsedIn(typeof(EnumConverterFactory), typeof(DecimalConverterFactory)));
        }

        [Fact]
        public void EnumToiString()
        {
            var converter = new TestObjectConverter();
            Assert.Equal("Zero", converter.Convert(TestEnum.Zero, typeof(string)));
            Assert.Equal("One", converter.Convert(TestEnum.One, typeof(string)));
            Assert.True(converter.UsedOnly<EnumConverterFactory>());
        }

        [Fact]
        public void CanNotConvertFromEnum()
        {
            var converter = new TestObjectConverter();
            Assert.False(converter.CanConvert(typeof(TestEnum), typeof(TestStruct)));
        }
    }
}
