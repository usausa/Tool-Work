namespace Smart.Benchmarks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class ConvertEnumerableBenchmark
    {
        private readonly Func<object, object> converter = x => x;

        private readonly int[] array = new int[100];

        [Benchmark]
        public List<int> ConstructorLinq()
        {
            return new List<int>(array.Select(x => (int)converter(x)));
        }

        [Benchmark]
        public List<int> ForLoopAdd()
        {
            var list = new List<int>();
            for (var i = 0; i < array.Length; i++)
            {
                list.Add((int)converter(array[i]));
            }

            return list;
        }

        [Benchmark]
        public List<int> ForLoopAddWithCapacity()
        {
            var list = new List<int>(array.Length);
            for (var i = 0; i < array.Length; i++)
            {
                list.Add((int)converter(array[i]));
            }

            return list;
        }

        [Benchmark]
        public List<int> ForEachAdd()
        {
            var list = new List<int>();
            foreach (var value in array)
            {
                list.Add((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public List<int> ForEachAddWithCapacity()
        {
            var list = new List<int>(array.Length);
            foreach (var value in array)
            {
                list.Add((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public List<int> ConstructorStructEnumerable()
        {
            return new List<int>(new ArrayConvertStructEnumerable<int>(array, converter));
        }

        [Benchmark]
        public List<int> ConstructorStructCollection()
        {
            return new List<int>(new ArrayConvertStructCollection<int>(array, converter));
        }
    }

    public readonly struct ArrayConvertStructEnumerable<T> : IEnumerable<T>
    {
        private readonly T[] array;

        private readonly Func<object, object> converter;

        public ArrayConvertStructEnumerable(T[] array, Func<object, object> converter)
        {
            this.array = array;
            this.converter = converter;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayConvertStructEnumerator<T>(array, converter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct ArrayConvertStructEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] source;

        private readonly Func<object, object> converter;

        private int index;

        public ArrayConvertStructEnumerator(T[] source, Func<object, object> converter)
        {
            this.source = source;
            this.converter = converter;
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

        public T Current => (T)converter(source[index]);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }

    public readonly struct ArrayConvertStructCollection<T> : ICollection<T>
    {
        private readonly T[] source;

        private readonly Func<object, object> converter;

        public ArrayConvertStructCollection(T[] source, Func<object, object> converter)
        {
            this.source = source;
            this.converter = converter;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayConvertStructEnumerator<T>(source, converter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item) => throw new NotSupportedException();

        public void Clear() => throw new NotSupportedException();

        public bool Contains(T item) => throw new NotSupportedException();

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (var i = 0; i < source.Length; i++)
            {
                array[arrayIndex + i] = (T)converter(source[i]);
            }
        }

        public bool Remove(T item) => throw new NotSupportedException();

        public int Count => source.Length;

        public bool IsReadOnly => true;
    }
}
