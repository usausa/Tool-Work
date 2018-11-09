namespace Smart.Benchmarks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class ConvertArrayToQueueBenchmark
    {
        private readonly Func<object, object> converter = x => x;

        private readonly int[] array = new int[100];

        [Benchmark]
        public Queue<int> ConstructorLinq()
        {
            return new Queue<int>(array.Select(x => (int)converter(x)));
        }

        [Benchmark]
        public Queue<int> ForLoopAdd()
        {
            var list = new Queue<int>();
            for (var i = 0; i < array.Length; i++)
            {
                list.Enqueue((int)converter(array[i]));
            }

            return list;
        }

        [Benchmark]
        public Queue<int> ForLoopAddWithCapacity()
        {
            var list = new Queue<int>(array.Length);
            for (var i = 0; i < array.Length; i++)
            {
                list.Enqueue((int)converter(array[i]));
            }

            return list;
        }

        [Benchmark]
        public Queue<int> ForEachAdd()
        {
            var list = new Queue<int>();
            foreach (var value in array)
            {
                list.Enqueue((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public Queue<int> ForEachAddWithCapacity()
        {
            var list = new Queue<int>(array.Length);
            foreach (var value in array)
            {
                list.Enqueue((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public Queue<int> ConstructorStructEnumerable()
        {
            return new Queue<int>(new ArrayConvertStructEnumerable<int>(array, converter));
        }

        [Benchmark]
        public Queue<int> ConstructorStructCollection()
        {
            return new Queue<int>(new ArrayConvertStructCollection<int>(array, converter));
        }

        [Benchmark]
        public Queue<int> ConstructorClassEnumerable()
        {
            return new Queue<int>(new ArrayConvertClassEnumerable<int>(array, converter));
        }

        [Benchmark]
        public Queue<int> ConstructorClassCollection()
        {
            return new Queue<int>(new ArrayConvertClassCollection<int>(array, converter));
        }
    }
}
