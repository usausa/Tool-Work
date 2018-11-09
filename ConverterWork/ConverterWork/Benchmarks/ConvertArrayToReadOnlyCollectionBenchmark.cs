namespace Smart.Benchmarks
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class ConvertArrayToReadOnlyCollectionBenchmark
    {
        private readonly Func<object, object> converter = x => x;

        private readonly int[] array = new int[100];

        [Benchmark]
        public ReadOnlyCollection<int> ConstructorLinq()
        {
            return new ReadOnlyCollection<int>(array.Select(x => (int)converter(x)).ToList());
        }

        [Benchmark]
        public ReadOnlyCollection<int> ConstructorStructList()
        {
            return new ReadOnlyCollection<int>(new ArrayConvertStructList<int>(array, converter));
        }

        [Benchmark]
        public ReadOnlyCollection<int> ConstructorClassList()
        {
            return new ReadOnlyCollection<int>(new ArrayConvertClassList<int>(array, converter));
        }
    }
}
