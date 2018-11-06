namespace Smart.Benchmarks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class ConvertArrayToLinkedListBenchmark
    {
        private readonly Func<object, object> converter = x => x;

        private readonly int[] array = new int[100];

        [Benchmark]
        public LinkedList<int> ConstructorLinq()
        {
            return new LinkedList<int>(array.Select(x => (int)converter(x)));
        }

        [Benchmark]
        public LinkedList<int> ForLoopAdd()
        {
            var list = new LinkedList<int>();
            for (var i = 0; i < array.Length; i++)
            {
                list.AddLast((int)converter(array[i]));
            }

            return list;
        }

        [Benchmark]
        public LinkedList<int> ForEachAdd()
        {
            var list = new LinkedList<int>();
            foreach (var value in array)
            {
                list.AddLast((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public LinkedList<int> ConstructorStructEnumerable()
        {
            return new LinkedList<int>(new ArrayConvertStructEnumerable<int>(array, converter));
        }

        [Benchmark]
        public LinkedList<int> ConstructorStructCollection()
        {
            return new LinkedList<int>(new ArrayConvertStructCollection<int>(array, converter));
        }
    }
}
