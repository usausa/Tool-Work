namespace Smart.Benchmarks
{
    using System;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class DynamicConvertBenchmark
    {
        private static readonly Type IntType = typeof(int);

        private static readonly Type LongType = typeof(long);

        private static readonly Type NullableIntType = typeof(int?);

        private static readonly Type CustomValueType = typeof(MyStruct);

        private static readonly Type StringType = typeof(string);

        private readonly Smart.Converter.ObjectConverter converter2 = Smart.Converter.ObjectConverter.Default;

        // null ->

        [Benchmark]
        public void NullToIntType() => converter2.Convert(null, IntType);

        [Benchmark]
        public void NullToNullableIntType() => converter2.Convert(null, NullableIntType);

        [Benchmark]
        public void NullToCustomValueType() => converter2.Convert(null, CustomValueType);

        [Benchmark]
        public void NullToStringType() => converter2.Convert(null, StringType);

        // int ->

        [Benchmark]
        public void IntToSameType() => converter2.Convert(0, IntType);

        [Benchmark]
        public void IntToSameNullableType() => converter2.Convert(0, NullableIntType);

        [Benchmark]
        public void IntToLongType() => converter2.Convert(Int32.MaxValue, LongType);

        // string

        [Benchmark]
        public void StringToSameType() => converter2.Convert(string.Empty, StringType);
    }
}
