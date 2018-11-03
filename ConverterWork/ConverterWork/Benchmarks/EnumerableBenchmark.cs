namespace Smart.Benchmarks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class EnumerableBenchmark
    {
        private readonly List<int> list = new List<int>();

        [GlobalSetup]
        public void Setup()
        {
            for (var i = 0; i < 1000; i++)
            {
                list.Add(i);
            }
        }

        [Benchmark]
        public int For()
        {
            var total = 0;
            for (var i = 0; i < list.Count; i++)
            {
                total += list[i];
            }

            return total;
        }

        [Benchmark]
        public int ForEach()
        {
            var total = 0;
            foreach (var i in list)
            {
                total += i;
            }

            return total;
        }

        [Benchmark]
        public int Enumerator()
        {
            var total = 0;
            using (var ie = list.GetEnumerator())
            {
                while (ie.MoveNext())
                {
                    total += ie.Current;
                }
            }

            return total;
        }
    }
}
