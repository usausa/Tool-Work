namespace Smart.Benchmarks
{
    using System;

    using BenchmarkDotNet.Attributes;

    using Smart.Collections.Concurrent;

    public struct Struct2
    {
        public int Member1 { get; set; }
        public int Member2 { get; set; }
    }

    public struct Struct10
    {
        public int Member1 { get; set; }
        public int Member2 { get; set; }
        public int Member3 { get; set; }
        public int Member4 { get; set; }
        public int Member5 { get; set; }
        public int Member6 { get; set; }
        public int Member7 { get; set; }
        public int Member8 { get; set; }
        public int Member9 { get; set; }
        public int Member10 { get; set; }
    }

    public struct Struct20L
    {
        public long Member01 { get; set; }
        public long Member02 { get; set; }
        public long Member03 { get; set; }
        public long Member04 { get; set; }
        public long Member05 { get; set; }
        public long Member06 { get; set; }
        public long Member07 { get; set; }
        public long Member08 { get; set; }
        public long Member09 { get; set; }
        public long Member10 { get; set; }
        public long Member11 { get; set; }
        public long Member12 { get; set; }
        public long Member13 { get; set; }
        public long Member14 { get; set; }
        public long Member15 { get; set; }
        public long Member16 { get; set; }
        public long Member17 { get; set; }
        public long Member18 { get; set; }
        public long Member19 { get; set; }
        public long Member20 { get; set; }
    }

    public class CreateStructBenchmark
    {
        private static readonly Type Type2 = typeof(Struct2);

        private static readonly Type Type10 = typeof(Struct10);

        private static readonly Type Type20L = typeof(Struct20L);

        private readonly ThreadsafeTypeHashArrayMap<object> cache = new ThreadsafeTypeHashArrayMap<object>();

        [GlobalSetup]
        public void Setup()
        {
            cache.AddIfNotExist(Type2, default(Struct2));
            cache.AddIfNotExist(Type10, default(Struct10));
            cache.AddIfNotExist(Type20L, default(Struct20L));
        }

        [Benchmark]
        public object CreateByBox2()
        {
            return cache.TryGetValue(Type2, out object value) ? value : null;
        }

        [Benchmark]
        public object CreateByBox10()
        {
            return cache.TryGetValue(Type10, out object value) ? value : null;
        }

        [Benchmark]
        public object CreateByBox20L()
        {
            return cache.TryGetValue(Type20L, out object value) ? value : null;
        }

        [Benchmark]
        public object CreateByReflection2()
        {
            return Activator.CreateInstance(Type2);
        }

        [Benchmark]
        public object CreateByReflection10()
        {
            return Activator.CreateInstance(Type10);
        }

        [Benchmark]
        public object CreateByReflection20L()
        {
            return Activator.CreateInstance(Type20L);
        }
    }
}
