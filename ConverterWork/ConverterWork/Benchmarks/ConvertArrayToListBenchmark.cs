namespace Smart.Benchmarks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class ConvertArrayToListBenchmark
    {
        private readonly Func<object, object> converter = x => x;

        private readonly int[] array = new int[100];

        [Benchmark]
        public List<int> ConstructorLinq()
        {
            return new List<int>(array.Select(x => (int)converter(x)));
        }

        [Benchmark]
        public List<int> ForLoopAdd()
        {
            var list = new List<int>();
            for (var i = 0; i < array.Length; i++)
            {
                list.Add((int)converter(array[i]));
            }

            return list;
        }

        [Benchmark]
        public List<int> ForLoopAddWithCapacity()
        {
            var list = new List<int>(array.Length);
            for (var i = 0; i < array.Length; i++)
            {
                list.Add((int)converter(array[i]));
            }

            return list;
        }

        [Benchmark]
        public List<int> ForEachAdd()
        {
            var list = new List<int>();
            foreach (var value in array)
            {
                list.Add((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public List<int> ForEachAddWithCapacity()
        {
            var list = new List<int>(array.Length);
            foreach (var value in array)
            {
                list.Add((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public List<int> ConstructorStructEnumerable()
        {
            return new List<int>(new ArrayConvertStructEnumerable<int>(array, converter));
        }

        [Benchmark]
        public List<int> ConstructorStructCollection()
        {
            return new List<int>(new ArrayConvertStructCollection<int>(array, converter));
        }

        [Benchmark]
        public List<int> ConstructorClassEnumerable()
        {
            return new List<int>(new ArrayConvertClassEnumerable<int>(array, converter));
        }

        [Benchmark]
        public List<int> ConstructorClassCollection()
        {
            return new List<int>(new ArrayConvertClassCollection<int>(array, converter));
        }

        [Benchmark]
        public List<int> ConstructorMixEnumerable()
        {
            return new List<int>(new ArrayConvertMixEnumerable<int>(array, converter));
        }

        [Benchmark]
        public List<int> ConstructorMixCollection()
        {
            return new List<int>(new ArrayConvertMixCollection<int>(array, converter));
        }
    }
}
