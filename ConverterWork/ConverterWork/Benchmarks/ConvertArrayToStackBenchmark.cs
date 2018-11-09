namespace Smart.Benchmarks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class ConvertArrayToStackBenchmark
    {
        private readonly Func<object, object> converter = x => x;

        private readonly int[] array = new int[100];

        [Benchmark]
        public Stack<int> ConstructorLinq()
        {
            return new Stack<int>(array.Select(x => (int)converter(x)));
        }

        [Benchmark]
        public Stack<int> ForLoopAdd()
        {
            var list = new Stack<int>();
            for (var i = 0; i < array.Length; i++)
            {
                list.Push((int)converter(array[i]));
            }

            return list;
        }

        [Benchmark]
        public Stack<int> ForLoopAddWithCapacity()
        {
            var list = new Stack<int>(array.Length);
            for (var i = 0; i < array.Length; i++)
            {
                list.Push((int)converter(array[i]));
            }

            return list;
        }

        [Benchmark]
        public Stack<int> ForEachAdd()
        {
            var list = new Stack<int>();
            foreach (var value in array)
            {
                list.Push((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public Stack<int> ForEachAddWithCapacity()
        {
            var list = new Stack<int>(array.Length);
            foreach (var value in array)
            {
                list.Push((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public Stack<int> ConstructorStructEnumerable()
        {
            return new Stack<int>(new ArrayConvertStructEnumerable<int>(array, converter));
        }

        [Benchmark]
        public Stack<int> ConstructorStructCollection()
        {
            return new Stack<int>(new ArrayConvertStructCollection<int>(array, converter));
        }

        [Benchmark]
        public Stack<int> ConstructorClassEnumerable()
        {
            return new Stack<int>(new ArrayConvertClassEnumerable<int>(array, converter));
        }

        [Benchmark]
        public Stack<int> ConstructorClassCollection()
        {
            return new Stack<int>(new ArrayConvertClassCollection<int>(array, converter));
        }
    }
}
