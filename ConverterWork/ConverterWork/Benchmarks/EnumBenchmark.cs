namespace Smart.Benchmarks
{
    using System;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class EnumBenchmark
    {
        private enum MyEnum
        {
            One,
            Two,
            Three,
            Four,
            Five
        }

        private static readonly MyEnum EnumValue = MyEnum.Three;

        private static readonly Type EnumType = typeof(MyEnum);

        [Benchmark]
        public object EnumToString()
        {
            return EnumValue.ToString();
        }

        [Benchmark]
        public object EnumToStringByHelper()
        {
            return EnumHelper<MyEnum>.GetName(EnumValue);
        }

        [Benchmark]
        public object EnumFromString()
        {
            return (MyEnum)Enum.Parse(EnumType, "Two");
        }

        [Benchmark]
        public object EnumFromStringByHelper()
        {
            return EnumHelper<MyEnum>.ParseValue("Two");
        }
    }
}
