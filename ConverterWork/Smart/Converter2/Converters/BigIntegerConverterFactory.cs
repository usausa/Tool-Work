namespace Smart.Converter2.Converters
{
    using System;

    public sealed class BigIntegerConverterFactory : IConverterFactory
    {
        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            throw new NotImplementedException();
        }
    }

    /*
            public BigInteger(int value)
            public BigInteger(uint value)
            public BigInteger(long value)
            public BigInteger(ulong value)

            public BigInteger(float value) : this((double)value)
            public BigInteger(double value)
                        throw new OverflowException(SR.Overflow_BigIntInfinity);

            public BigInteger(decimal value)


            public static bool TryParse(string value, out BigInteger result)
            {
                return TryParse(value, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
            }

            public override string ToString()
            {
                return BigNumber.FormatBigInteger(this, null, NumberFormatInfo.CurrentInfo);
            }


            public static explicit operator byte(BigInteger value)
                return checked((byte)((int)value));
            }

            [CLSCompliant(false)]
            public static explicit operator sbyte(BigInteger value)
            {
                return checked((sbyte)((int)value));
            }

            public static explicit operator short(BigInteger value)
            {
                return checked((short)((int)value));
            }

            [CLSCompliant(false)]
            public static explicit operator ushort(BigInteger value)
            {
                return checked((ushort)((int)value));
            }

            public static explicit operator int(BigInteger value)
            {
                    throw new OverflowException(SR.Overflow_Int32);
                }
                return unchecked(-(int)value._bits[0]);
            }


            [CLSCompliant(false)]
            public static explicit operator uint(BigInteger value)
                    throw new OverflowException(SR.Overflow_UInt32);

            public static explicit operator long(BigInteger value)
            {
                value.AssertValid();

                long ll = value._sign > 0 ? unchecked((long)uu) : unchecked(-(long)uu);
                if ((ll > 0 && value._sign > 0) || (ll < 0 && value._sign < 0))
                {
                    // Signs match, no overflow
                    return ll;
                }
                throw new OverflowException(SR.Overflow_Int64);
            }

            [CLSCompliant(false)]
            public static explicit operator ulong(BigInteger value)
            }

            public static explicit operator float(BigInteger value)
            {
                return (float)((double)value);
            }

            public static explicit operator double(BigInteger value)
            {
                value.AssertValid();

            public static explicit operator decimal(BigInteger value)
           {
                if (length > 3) throw new OverflowException(SR.Overflow_Decimal);

            public static BigInteger operator &(BigInteger left, BigInteger right)
    */
}
