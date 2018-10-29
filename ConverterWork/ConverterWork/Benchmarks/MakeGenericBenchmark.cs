namespace Smart.Benchmarks
{
    using System;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class MakeGenericBenchmark
    {
        public interface IConvertBuilder
        {
            object Build(object source);

            Func<object, object> QueryBuildMethod2();

            Func<object, object> QueryBuildMethod3();
        }

        private sealed class ConvertBuilder<T> : IConvertBuilder
        {
            private readonly Func<object, object> converter;

            public ConvertBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Build(object source)
            {
                return (T)converter(source);
            }

            private object Build2(object source)
            {
                return (T)converter(source);
            }

            public object Build3(object source)
            {
                return (T)converter(source);
            }

            public Func<object, object> QueryBuildMethod2()
            {
                return Build2;
            }

            public Func<object, object> QueryBuildMethod3()
            {
                return Build3;
            }
        }

        public object ConvertBuild<T>(Func<object, object> converter, object source)
        {
            return (T)converter(source);
        }

        private Func<object, object> classBuilder1;

        private Func<object, object> classBuilder2;

        private Func<object, object> classBuilder3;

        //private Func<object, object> methodBuilder;

        [GlobalSetup]
        public void Setup()
        {
            var identify = (Func<object, object>)(x => x);

            var type = typeof(ConvertBuilder<>).MakeGenericType(typeof(string));
            var convertBuilder = (IConvertBuilder)Activator.CreateInstance(type, identify);
            classBuilder1 = convertBuilder.Build;
            classBuilder2 = convertBuilder.QueryBuildMethod2();
            classBuilder3 = convertBuilder.QueryBuildMethod3();

            //var mi = typeof(MakeGenericBenchmark).GetMethod(nameof(ConvertBuild)).MakeGenericMethod(typeof(string));
            //var func = (Func<Func<object, object>, object, object>)Delegate.CreateDelegate(typeof(Func<Func<object, object>, object, object>), this, mi);
            //methodBuilder = source => func(identify, source);
        }

        [Benchmark]
        public object ClassBuilder1()
        {
            return classBuilder1(string.Empty);
        }

        [Benchmark]
        public object ClassBuilder2()
        {
            return classBuilder2(string.Empty);
        }

        [Benchmark]
        public object ClassBuilder3()
        {
            return classBuilder3(string.Empty);
        }

        //[Benchmark]
        //public object MethodBuilder()
        //{
        //    return methodBuilder(string.Empty);
        //}
    }
}
