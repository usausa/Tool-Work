namespace Smart.Benchmarks
{
    using System;

    public class MakeGenericBenchmark
    {
        private interface IBuilder
        {
            object Build(object source);
        }

        private sealed class ConvertBuilder<T> : IBuilder
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
        }

        private static object ConvertBuild<T>(Func<object, object> converter, object source)
        {
            return (T)converter(source);
        }
    }
}
