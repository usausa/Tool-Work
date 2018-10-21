﻿namespace Smart.Tests
{
    using System;

    using Smart.Converter2.Converters;

    using Xunit;

    public class NumericLongToConvertTest
    {
        [Fact]
        public void LongToByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)0, converter.Convert(Int64.MinValue, typeof(byte)));
            Assert.Equal((byte)255, converter.Convert(Int64.MaxValue, typeof(byte)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToNullableByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((byte)0, converter.Convert(Int64.MinValue, typeof(byte?)));
            Assert.Equal((byte)255, converter.Convert(Int64.MaxValue, typeof(byte?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToSByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((sbyte)0, converter.Convert(Int64.MinValue, typeof(sbyte)));
            Assert.Equal((sbyte)-1, converter.Convert(Int64.MaxValue, typeof(sbyte)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToNullableSByte()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((sbyte)0, converter.Convert(Int64.MinValue, typeof(sbyte?)));
            Assert.Equal((sbyte)-1, converter.Convert(Int64.MaxValue, typeof(sbyte?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((short)0, converter.Convert(Int64.MinValue, typeof(short)));
            Assert.Equal((short)-1, converter.Convert(Int64.MaxValue, typeof(short)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToNullableShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((short)0, converter.Convert(Int64.MinValue, typeof(short?)));
            Assert.Equal((short)-1, converter.Convert(Int64.MaxValue, typeof(short?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToUShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((ushort)0, converter.Convert(Int64.MinValue, typeof(ushort)));
            Assert.Equal((ushort)65535, converter.Convert(Int64.MaxValue, typeof(ushort)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToNullableUShort()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((ushort)0, converter.Convert(Int64.MinValue, typeof(ushort?)));
            Assert.Equal((ushort)65535, converter.Convert(Int64.MaxValue, typeof(ushort?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(Int64.MinValue, typeof(int)));
            Assert.Equal(-1, converter.Convert(Int64.MaxValue, typeof(int)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToNullableInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0, converter.Convert(Int64.MinValue, typeof(int?)));
            Assert.Equal(-1, converter.Convert(Int64.MaxValue, typeof(int?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToUInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0U, converter.Convert(Int64.MinValue, typeof(uint)));
            Assert.Equal(4294967295U, converter.Convert(Int64.MaxValue, typeof(uint)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToNullableUInt()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(0U, converter.Convert(Int64.MinValue, typeof(uint?)));
            Assert.Equal(4294967295U, converter.Convert(Int64.MaxValue, typeof(uint?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToULong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(9223372036854775808UL, converter.Convert(Int64.MinValue, typeof(ulong)));
            Assert.Equal(9223372036854775807UL, converter.Convert(Int64.MaxValue, typeof(ulong)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToNullableULong()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(9223372036854775808UL, converter.Convert(Int64.MinValue, typeof(ulong?)));
            Assert.Equal(9223372036854775807UL, converter.Convert(Int64.MaxValue, typeof(ulong?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToChar()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((char)0, converter.Convert(Int64.MinValue, typeof(char)));
            Assert.Equal((char)65535, converter.Convert(Int64.MaxValue, typeof(char)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToNullableChar()
        {
            var converter = new TestObjectConverter();
            Assert.Equal((char)0, converter.Convert(Int64.MinValue, typeof(char?)));
            Assert.Equal((char)65535, converter.Convert(Int64.MaxValue, typeof(char?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToDouble()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(-9223372036854775808d, converter.Convert(Int64.MinValue, typeof(double)));
            Assert.Equal(9223372036854775807d, converter.Convert(Int64.MaxValue, typeof(double)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToNullableDouble()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(-9223372036854775808d, converter.Convert(Int64.MinValue, typeof(double?)));
            Assert.Equal(9223372036854775807d, converter.Convert(Int64.MaxValue, typeof(double?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToFloat()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(-9223372036854775808f, converter.Convert(Int64.MinValue, typeof(float)));
            Assert.Equal(9223372036854775807f, converter.Convert(Int64.MaxValue, typeof(float)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        [Fact]
        public void LongToNullableFloat()
        {
            var converter = new TestObjectConverter();
            Assert.Equal(-9223372036854775808f, converter.Convert(Int64.MinValue, typeof(float?)));
            Assert.Equal(9223372036854775807f, converter.Convert(Int64.MaxValue, typeof(float?)));
            Assert.True(converter.UsedOnly<NumericConverterFactory>());
        }

        //[Fact]
        //public void LongToDecimal()
        //{
        //    var converter = new TestObjectConverter();
        //    Assert.Equal(-9223372036854775808m, converter.Convert(Int64.MinValue, typeof(decimal)));
        //    Assert.Equal(9223372036854775807m, converter.Convert(Int64.MaxValue, typeof(decimal)));
        //    Assert.True(converter.UsedOnly<NumericConverterFactory>());
        //}

        //[Fact]
        //public void LongToNullableDecimal()
        //{
        //    var converter = new TestObjectConverter();
        //    Assert.Equal(-9223372036854775808m, converter.Convert(Int64.MinValue, typeof(decimal?)));
        //    Assert.Equal(9223372036854775807m, converter.Convert(Int64.MaxValue, typeof(decimal?)));
        //    Assert.True(converter.UsedOnly<NumericConverterFactory>());
        //}
    }
}
