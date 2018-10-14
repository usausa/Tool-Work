namespace Smart.Benchmarks
{
    using System;

    using BenchmarkDotNet.Attributes;

    using Smart.Converter2;

    [Config(typeof(BenchmarkConfig))]
    public class CachedConvertBenchmark
    {
        private static readonly Type IntType = typeof(int);

        //private static readonly Type LongType = typeof(long);

        private static readonly Type NullableIntType = typeof(int?);

        //private static readonly Type CustomValueType = typeof(MyStruct);

        //private static readonly Type StringType = typeof(string);

        private readonly Func<object, object> intToIntConverter = ObjectConverter.Default.CreateConverter(IntType, IntType);

        private readonly Func<object, object> intToNullableIntConverter = ObjectConverter.Default.CreateConverter(IntType, NullableIntType);

        // null ->

        [Benchmark]
        public void NullToIntType() => intToIntConverter(null);

        [Benchmark]
        public void NullToNullableIntType() => intToNullableIntConverter(null);

        //[Benchmark]
        //public void NullToCustomValueType() => converter2.Convert(null, CustomValueType);

        //[Benchmark]
        //public void NullToStringType() => converter2.Convert(null, StringType);

        // int ->

        //[Benchmark]
        //public void IntToSameType() => converter2.Convert(0, IntType);

        //[Benchmark]
        //public void IntToSameNullableType() => converter2.Convert(0, NullableIntType);

        //[Benchmark]
        //public void IntToLongType() => converter2.Convert(Int32.MaxValue, LongType);

        // string

        //[Benchmark]
        //public void StringToSameType() => converter2.Convert(string.Empty, StringType);
    }
}
