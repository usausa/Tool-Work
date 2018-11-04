namespace Smart.Benchmarks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

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
