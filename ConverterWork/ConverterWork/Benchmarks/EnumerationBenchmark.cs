namespace Smart.Benchmarks
{
    using System.Collections;
    using System.Collections.Generic;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class EnumerationBenchmark
    {
        private readonly int[] values = new int[100];

        [Benchmark]
        public int ForEachClass()
        {
            var ret = 0;
            foreach (var value in new ClassEnumerable<int>(values))
            {
                ret += value;
            }

            return ret;
        }

        [Benchmark]
        public int ForEachStruct()
        {
            var ret = 0;
            foreach (var value in new StructEnumerable<int>(values))
            {
                ret += value;
            }

            return ret;
        }

        [Benchmark]
        public int WhileClass()
        {
            var ret = 0;
            using (var en = new ClassEnumerable<int>(values).GetEnumerator())
            {
                while (en.MoveNext())
                {
                    ret += en.Current;
                }
            }

            return ret;
        }

        [Benchmark]
        public int WhileStruct()
        {
            var ret = 0;
            using (var en = new StructEnumerable<int>(values).GetEnumerator())
            {
                while (en.MoveNext())
                {
                    ret += en.Current;
                }
            }

            return ret;
        }

        private sealed class ClassEnumerator<T> : IEnumerator<T>
        {
            private readonly T[] source;

            private int index;

            public ClassEnumerator(T[] source)
            {
                this.source = source;
                index = -1;
            }

            public bool MoveNext()
            {
                index++;
                return index < source.Length;
            }

            public void Reset()
            {
                index = -1;
            }

            public T Current => source[index];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        private sealed class ClassEnumerable<T> : IEnumerable<T>
        {
            private readonly T[] source;

            public ClassEnumerable(T[] source)
            {
                this.source = source;
            }

            public IEnumerator<T> GetEnumerator() => new ClassEnumerator<T>(source);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private struct StructEnumerator<T> : IEnumerator<T>
        {
            private readonly T[] source;

            private int index;

            public StructEnumerator(T[] source)
            {
                this.source = source;
                index = -1;
            }

            public bool MoveNext()
            {
                index++;
                return index < source.Length;
            }

            public void Reset()
            {
                index = -1;
            }

            public T Current => source[index];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        private readonly struct StructEnumerable<T> : IEnumerable<T>
        {
            private readonly T[] source;

            public StructEnumerable(T[] source)
            {
                this.source = source;
            }

            public IEnumerator<T> GetEnumerator() => new StructEnumerator<T>(source);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
