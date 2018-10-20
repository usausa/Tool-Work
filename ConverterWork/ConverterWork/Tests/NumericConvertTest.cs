namespace Smart.Tests
{
    using System;

    using Smart.Converter2.Converters;

    using Xunit;

    public class NumericConvertTest
    {
        //--------------------------------------------------------------------------------
        // ByteTo
        //--------------------------------------------------------------------------------

        [Fact]
        public void ByteToByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((sbyte)0, converter.Convert(Byte.MinValue, typeof(sbyte)));
            Assert.Equal((sbyte)-1, converter.Convert(Byte.MaxValue, typeof(sbyte)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void ByteToNullableByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((sbyte)0, converter.Convert(Byte.MinValue, typeof(sbyte?)));
            Assert.Equal((sbyte)-1, converter.Convert(Byte.MaxValue, typeof(sbyte?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void ByteToShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((short)0, converter.Convert(Byte.MinValue, typeof(short)));
            Assert.Equal((short)255, converter.Convert(Byte.MaxValue, typeof(short)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void ByteToNullableShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((short)0, converter.Convert(Byte.MinValue, typeof(short?)));
            Assert.Equal((short)255, converter.Convert(Byte.MaxValue, typeof(short?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void ByteToUShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((ushort)0, converter.Convert(Byte.MinValue, typeof(ushort)));
            Assert.Equal((ushort)255, converter.Convert(Byte.MaxValue, typeof(ushort)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void ByteToNullableUShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((ushort)0, converter.Convert(Byte.MinValue, typeof(ushort?)));
            Assert.Equal((ushort)255, converter.Convert(Byte.MaxValue, typeof(ushort?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void ByteToInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(Byte.MinValue, typeof(int)));
            Assert.Equal(255, converter.Convert(Byte.MaxValue, typeof(int)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void ByteToNullableInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(Byte.MinValue, typeof(int?)));
            Assert.Equal(255, converter.Convert(Byte.MaxValue, typeof(int?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void ByteToUInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0U, converter.Convert(Byte.MinValue, typeof(uint)));
            Assert.Equal(255U, converter.Convert(Byte.MaxValue, typeof(uint)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void ByteToNullableUInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0U, converter.Convert(Byte.MinValue, typeof(uint?)));
            Assert.Equal(255U, converter.Convert(Byte.MaxValue, typeof(uint?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }
    }
}
