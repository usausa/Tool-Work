namespace Smart.Benchmarks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class ConvertArrayToHashSetBenchmark
    {
        private readonly Func<object, object> converter = x => x;

        private readonly int[] array = new int[100];

        [Benchmark]
        public HashSet<int> ConstructorLinq()
        {
            return new HashSet<int>(array.Select(x => (int)converter(x)));
        }

        [Benchmark]
        public HashSet<int> ForLoopAdd()
        {
            var list = new HashSet<int>();
            for (var i = 0; i < array.Length; i++)
            {
                list.Add((int)converter(array[i]));
            }

            return list;
        }

        //[Benchmark]
        //public HashSet<int> ForLoopAddWithCapacity()
        //{
        //    var list = new HashSet<int>(array.Length);
        //    for (var i = 0; i < array.Length; i++)
        //    {
        //        list.Add((int)converter(array[i]));
        //    }

        //    return list;
        //}

        [Benchmark]
        public HashSet<int> ForEachAdd()
        {
            var list = new HashSet<int>();
            foreach (var value in array)
            {
                list.Add((int)converter(value));
            }

            return list;
        }

        //[Benchmark]
        //public HashSet<int> ForEachAddWithCapacity()
        //{
        //    var list = new HashSet<int>(array.Length);
        //    foreach (var value in array)
        //    {
        //        list.Add((int)converter(value));
        //    }

        //    return list;
        //}

        [Benchmark]
        public HashSet<int> ConstructorStructEnumerable()
        {
            return new HashSet<int>(new ArrayConvertStructEnumerable<int>(array, converter));
        }

        [Benchmark]
        public HashSet<int> ConstructorStructCollection()
        {
            return new HashSet<int>(new ArrayConvertStructCollection<int>(array, converter));
        }

        [Benchmark]
        public HashSet<int> ConstructorClassEnumerable()
        {
            return new HashSet<int>(new ArrayConvertClassEnumerable<int>(array, converter));
        }

        [Benchmark]
        public HashSet<int> ConstructorClassCollection()
        {
            return new HashSet<int>(new ArrayConvertClassCollection<int>(array, converter));
        }
    }
}
