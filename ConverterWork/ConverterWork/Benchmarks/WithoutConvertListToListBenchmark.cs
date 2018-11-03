namespace Smart.Benchmarks
{
    using System.Collections.Generic;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class WithoutConvertListToListBenchmark
    {
        private readonly List<int> source = new List<int>(new int[100]);

        [Benchmark]
        public List<int> ConstructorLinq()
        {
            return new List<int>(source);
        }

        [Benchmark]
        public List<int> ForLoopAdd()
        {
            var list = new List<int>();
            for (var i = 0; i < source.Count; i++)
            {
                list.Add(source[i]);
            }

            return list;
        }

        [Benchmark]
        public List<int> ForLoopAddWithCapacity()
        {
            var list = new List<int>(source.Count);
            for (var i = 0; i < source.Count; i++)
            {
                list.Add(source[i]);
            }

            return list;
        }

        [Benchmark]
        public List<int> ForEachAdd()
        {
            var list = new List<int>();
            foreach (var value in source)
            {
                list.Add(value);
            }

            return list;
        }

        [Benchmark]
        public List<int> ForEachAddWithCapacity()
        {
            var list = new List<int>(source.Count);
            foreach (var value in source)
            {
                list.Add(value);
            }

            return list;
        }
    }
}
