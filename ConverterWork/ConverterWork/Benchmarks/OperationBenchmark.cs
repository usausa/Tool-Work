namespace Smart.Benchmarks
{
    using System;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class OperationBenchmark
    {
        private const int N = 1000;

        private static readonly Type IntType = typeof(int);

        private static readonly Type NullableIntType = typeof(int?);

        [Benchmark(OperationsPerInvoke = N)]
        public void IsNullableTypeInt()
        {
            for (var i = 0; i < N; i++)
            {
                IntType.IsNullableType();
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void IsNullableTypeNullableInt()
        {
            for (var i = 0; i < N; i++)
            {
                NullableIntType.IsNullableType();
            }
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void GetUnderlyingType()
        {
            for (var i = 0; i < N; i++)
            {
                Nullable.GetUnderlyingType(NullableIntType);
            }
        }
    }
}
