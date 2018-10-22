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

        // TODO

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

        // TODO

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

        // TODO

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

        // TODO

        //public void ByteToByte()
        //public void SByteToSByte()
        //public void ShortToShort()
        //public void UShortToUShort()
        //public void IntToInt()
        //public void UIntToUInt()
        //public void LongToLong()
        //public void ULongToULong()
        //public void CharToChar()
        //public void DoubleToDouble()
        //public void FloatToFloat()
        //public void DecimalToDecimal()
        //public void StringToString()    half
    }
}
