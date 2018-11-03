namespace Smart.Benchmarks
{
    using System.Collections.Generic;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class WithoutConvertArrayToListBenchmark
    {
        private readonly int[] array = new int[100];

        [Benchmark]
        public List<int> ConstructorLinq()
        {
            return new List<int>(array);
        }

        [Benchmark]
        public List<int> ForLoopAdd()
        {
            var list = new List<int>();
            for (var i = 0; i < array.Length; i++)
            {
                list.Add(array[i]);
            }

            return list;
        }

        [Benchmark]
        public List<int> ForLoopAddWithCapacity()
        {
            var list = new List<int>(array.Length);
            for (var i = 0; i < array.Length; i++)
            {
                list.Add(array[i]);
            }

            return list;
        }

        [Benchmark]
        public List<int> ForEachAdd()
        {
            var list = new List<int>();
            foreach (var value in array)
            {
                list.Add(value);
            }

            return list;
        }

        [Benchmark]
        public List<int> ForEachAddWithCapacity()
        {
            var list = new List<int>(array.Length);
            foreach (var value in array)
            {
                list.Add(value);
            }

            return list;
        }
    }
}
