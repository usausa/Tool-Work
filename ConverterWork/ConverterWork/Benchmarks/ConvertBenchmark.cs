namespace Smart.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class ConvertBenchmark
    {
        [Benchmark]
        public void Simple()
        {
            // TODO
        }
    }
}
