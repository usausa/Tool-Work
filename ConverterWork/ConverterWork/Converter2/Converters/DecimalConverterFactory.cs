namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed class DecimalConverterFactory : IConverterFactory
    {
        private static readonly Type DecimalType = typeof(decimal);

        private static readonly Type NullableDecimalType = typeof(decimal?);

        private static readonly Dictionary<Type, Func<object, object>> FromDecimalConverters = new Dictionary<Type, Func<object, object>>
        {
            { typeof(byte), x => { try { return Decimal.ToByte((decimal)x); } catch (OverflowException) { return default(byte); } } },
            { typeof(byte?), x => { try { return Decimal.ToByte((decimal)x); } catch (OverflowException) { return default(byte?); } } },
            { typeof(sbyte), x => { try { return Decimal.ToSByte((decimal)x); } catch (OverflowException) { return default(sbyte); } } },
            { typeof(sbyte?), x => { try { return Decimal.ToSByte((decimal)x); } catch (OverflowException) { return default(sbyte?); } } },
            { typeof(short), x => { try { return Decimal.ToInt16((decimal)x); } catch (OverflowException) { return default(short); } } },
            { typeof(short?), x => { try { return Decimal.ToInt16((decimal)x); } catch (OverflowException) { return default(short?); } } },
            { typeof(ushort), x => { try { return Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(ushort); } } },
            { typeof(ushort?), x => { try { return Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(ushort?); } } },
            { typeof(int), x => { try { return Decimal.ToInt32((decimal)x); } catch (OverflowException) { return default(int); } } },
            { typeof(int?), x => { try { return Decimal.ToInt32((decimal)x); } catch (OverflowException) { return default(int?); } } },
            { typeof(uint), x => { try { return Decimal.ToUInt32((decimal)x); } catch (OverflowException) { return default(uint); } } },
            { typeof(uint?), x => { try { return Decimal.ToUInt32((decimal)x); } catch (OverflowException) { return default(uint?); } } },
            { typeof(long), x => { try { return Decimal.ToInt64((decimal)x); } catch (OverflowException) { return default(long); } } },
            { typeof(long?), x => { try { return Decimal.ToInt64((decimal)x); } catch (OverflowException) { return default(long?); } } },
            { typeof(ulong), x => { try { return Decimal.ToUInt64((decimal)x); } catch (OverflowException) { return default(ulong); } } },
            { typeof(ulong?), x => { try { return Decimal.ToUInt64((decimal)x); } catch (OverflowException) { return default(ulong?); } } },
            { typeof(char), x => { try { return (char)Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(char); } } },
            { typeof(char?), x => { try { return (char)Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(char?); } } },
            { typeof(double), x => { try { return Decimal.ToDouble((decimal)x); } catch (OverflowException) { return default(double); } } },
            { typeof(double?), x => { try { return Decimal.ToDouble((decimal)x); } catch (OverflowException) { return default(double?); } } },
            { typeof(float), x => { try { return Decimal.ToSingle((decimal)x); } catch (OverflowException) { return default(float); } } },
            { typeof(float?), x => { try { return Decimal.ToSingle((decimal)x); } catch (OverflowException) { return default(float?); } } }
        };

        private static readonly Dictionary<Type, Func<object, object>> ToDecimalConverters = new Dictionary<Type, Func<object, object>>
        {
            { typeof(byte), x => new Decimal((byte)x) },
            { typeof(sbyte), x => new Decimal((sbyte)x) },
            { typeof(short), x => new Decimal((short)x) },
            { typeof(ushort), x => new Decimal((ushort)x) },
            { typeof(int), x => new Decimal((int)x) },
            { typeof(uint), x => new Decimal((uint)x) },
            { typeof(long), x => new Decimal((long)x) },
            { typeof(ulong), x => new Decimal((ulong)x) },
            { typeof(char), x => new Decimal((char)x) },
            { typeof(double), x => { try { return new Decimal((double)x); } catch (OverflowException) { return default(decimal); } } },
            { typeof(float), x => { try { return new Decimal((float)x); } catch (OverflowException) { return default(decimal); } } }
        };

        private static readonly Dictionary<Type, Func<object, object>> ToNullableDecimalConverters = new Dictionary<Type, Func<object, object>>
        {
            { typeof(double), x => { try { return new Decimal((double)x); } catch (OverflowException) { return default(decimal?); } } },
            { typeof(float), x => { try { return new Decimal((float)x); } catch (OverflowException) { return default(decimal?); } } }
        };

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            if (sourceType == DecimalType)
            {
                if (targetType.IsValueType)
                {
                    if (FromDecimalConverters.TryGetValue(targetType, out var converter))
                    {
                        return converter;
                    }
                }
            }
            else if ((targetType == DecimalType) || (targetType == NullableDecimalType))
            {
                if (sourceType.IsValueType)
                {
                    var type = sourceType.IsNullableType() ? Nullable.GetUnderlyingType(sourceType) : sourceType;
                    if ((targetType == NullableDecimalType) &&
                        ToNullableDecimalConverters.TryGetValue(type, out var converter))
                    {
                        return converter;
                    }

                    if (ToDecimalConverters.TryGetValue(type, out converter))
                    {
                        return converter;
                    }
                }
            }

            return null;
        }
    }
}
