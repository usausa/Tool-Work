namespace Smart.Benchmarks
{
    using System;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class ConvertBenchmark
    {
        private const int N = 1000;

        private static readonly Type IntType = typeof(int);

        private static readonly Type LongType = typeof(long);

        private static readonly Type NullableIntType = typeof(int?);

        private static readonly Type CustomValueType = typeof(MyStruct);

        private static readonly Type StringType = typeof(string);

        private readonly Smart.Converter.ObjectConverter converter = Smart.Converter.ObjectConverter.Default;

        private readonly Smart.Converter2.ObjectConverter converter2 = Smart.Converter2.ObjectConverter.Default;

        [Benchmark(OperationsPerInvoke = N)]
        public void NullToValueType()
        {
            for (var i = 0; i < N; i++)
            {
                converter.Convert(null, IntType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NullToValueType2()
        {
            for (var i = 0; i < N; i++)
            {
                converter2.Convert(null, IntType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NullToNullableValueType()
        {
            for (var i = 0; i < N; i++)
            {
                converter.Convert(null, NullableIntType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NullToNullableValueType2()
        {
            for (var i = 0; i < N; i++)
            {
                converter2.Convert(null, NullableIntType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NullToCustomValueType()
        {
            for (var i = 0; i < N; i++)
            {
                converter.Convert(null, CustomValueType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NullToCustomValueType2()
        {
            for (var i = 0; i < N; i++)
            {
                converter2.Convert(null, CustomValueType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NullToClassType()
        {
            for (var i = 0; i < N; i++)
            {
                converter.Convert(null, StringType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NullToClassType2()
        {
            for (var i = 0; i < N; i++)
            {
                converter2.Convert(null, StringType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NonNullToSameType()
        {
            for (var i = 0; i < N; i++)
            {
                converter.Convert(0, IntType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NonNullToSameType2()
        {
            for (var i = 0; i < N; i++)
            {
                converter2.Convert(0, IntType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NonNullToSameNullableType()
        {
            for (var i = 0; i < N; i++)
            {
                converter.Convert(0, NullableIntType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NonNullToSameNullableType2()
        {
            for (var i = 0; i < N; i++)
            {
                converter2.Convert(0, NullableIntType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NonNullToBigValueType()
        {
            for (var i = 0; i < N; i++)
            {
                converter.Convert(Int32.MaxValue, LongType);
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void NonNullToBigValueType2()
        {
            for (var i = 0; i < N; i++)
            {
                converter2.Convert(Int32.MaxValue, LongType);
            }
        }
    }
}
