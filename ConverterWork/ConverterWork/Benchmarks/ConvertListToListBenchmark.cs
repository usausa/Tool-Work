namespace Smart.Benchmarks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    [Config(typeof(BenchmarkConfig))]
    public class ConvertListToListBenchmark
    {
        private readonly Func<object, object> converter = x => x;

        private readonly List<int> source = new List<int>(new int[100]);

        [Benchmark]
        public List<int> ConstructorLinq()
        {
            return new List<int>(source.Select(x => (int)converter(x)));
        }

        [Benchmark]
        public List<int> ForLoopAdd()
        {
            var list = new List<int>();
            for (var i = 0; i < source.Count; i++)
            {
                list.Add((int)converter(source[i]));
            }

            return list;
        }

        [Benchmark]
        public List<int> ForLoopAddWithCapacity()
        {
            var list = new List<int>(source.Count);
            for (var i = 0; i < source.Count; i++)
            {
                list.Add((int)converter(source[i]));
            }

            return list;
        }

        [Benchmark]
        public List<int> ForEachAdd()
        {
            var list = new List<int>();
            foreach (var value in source)
            {
                list.Add((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public List<int> ForEachAddWithCapacity()
        {
            var list = new List<int>(source.Count);
            foreach (var value in source)
            {
                list.Add((int)converter(value));
            }

            return list;
        }

        [Benchmark]
        public List<int> ConstructorStructEnumerable()
        {
            return new List<int>(new ListConvertStructEnumerable<int>(source, converter));
        }

        [Benchmark]
        public List<int> ConstructorStructCollection()
        {
            return new List<int>(new ListConvertStructCollection<int>(source, converter));
        }
    }

    public readonly struct ListConvertStructEnumerable<T> : IEnumerable<T>
    {
        private readonly IList<T> source;

        private readonly Func<object, object> converter;

        public ListConvertStructEnumerable(IList<T> source, Func<object, object> converter)
        {
            this.source = source;
            this.converter = converter;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListConvertStructEnumerator<T>(source, converter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct ListConvertStructEnumerator<T> : IEnumerator<T>
    {
        private readonly IList<T> source;

        private readonly Func<object, object> converter;

        private int index;

        public ListConvertStructEnumerator(IList<T> source, Func<object, object> converter)
        {
            this.source = source;
            this.converter = converter;
            index = -1;
        }

        public bool MoveNext()
        {
            index++;
            return index < source.Count;
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

    public readonly struct ListConvertStructCollection<T> : IList<T>
    {
        private readonly IList<T> source;

        private readonly Func<object, object> converter;

        public ListConvertStructCollection(IList<T> source, Func<object, object> converter)
        {
            this.source = source;
            this.converter = converter;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListConvertStructEnumerator<T>(source, converter);
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
            for (var i = 0; i < source.Count; i++)
            {
                array[arrayIndex + i] = (T)converter(source[i]);
            }
        }

        public bool Remove(T item) => throw new NotSupportedException();

        public int Count => source.Count;

        public bool IsReadOnly => true;

        public int IndexOf(T item) => throw new NotSupportedException();

        public void Insert(int index, T item) => throw new NotSupportedException();

        public void RemoveAt(int index) => throw new NotSupportedException();

        public T this[int index]
        {
            get => source[index];
            set => source[index] = value;
        }
    }
}
