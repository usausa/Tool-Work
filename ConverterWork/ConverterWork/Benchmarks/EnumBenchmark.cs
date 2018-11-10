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

        private Func<object, object> toStringByMethod;

        private Func<object, object> fromStringByMethod;

        private Func<object, object> toStringByClass;

        private Func<object, object> fromStringByClass;

        [GlobalSetup]
        public void Setup()
        {
            toStringByMethod = ToStringBuilderMethod();
            fromStringByMethod = FromStringBuilderMethod();
            toStringByClass = new ToStringClass().Convert;
            fromStringByClass = new FromStringClass().Convert;
        }

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
        public object EnumToStringByMethod()
        {
            return toStringByMethod(EnumValue);
        }

        [Benchmark]
        public object EnumToStringByClass()
        {
            return toStringByClass(EnumValue);
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

        [Benchmark]
        public object EnumFromStringByMethod()
        {
            return (MyEnum)fromStringByMethod("Two");
        }

        [Benchmark]
        public object EnumFromStringByClass()
        {
            return (MyEnum)fromStringByClass("Two");
        }

        private Func<object, object> ToStringBuilderMethod()
        {
            return source => EnumHelper<MyEnum>.GetName((MyEnum)source);
        }

        private Func<object, object> FromStringBuilderMethod()
        {
            return source => EnumHelper<MyEnum>.TryParseValue((string)source, out var value) ? value : default;
        }

        private sealed class ToStringClass
        {
            public object Convert(object source)
            {
                return EnumHelper<MyEnum>.GetName((MyEnum)source);
            }
        }

        private sealed class FromStringClass
        {
            public object Convert(object source)
            {
                return EnumHelper<MyEnum>.TryParseValue((string)source, out var value) ? value : default;
            }
        }
    }
}
