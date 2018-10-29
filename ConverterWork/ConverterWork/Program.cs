using Smart.Benchmarks;

namespace Smart
{
    using System.Reflection;

    using BenchmarkDotNet.Running;

    public static class Program
    {
        public static void Main(string[] args)
        {
            new MakeGenericBenchmark().Setup();
            BenchmarkSwitcher.FromAssembly(typeof(Program).GetTypeInfo().Assembly).Run(args);
        }
    }
}
