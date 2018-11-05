namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public sealed partial class EnumerableConverterFactory
    {
        //--------------------------------------------------------------------------------
        // ArrayConvertStruct
        //--------------------------------------------------------------------------------

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

            public void Reset() => index = -1;

            public T Current => (T)converter(source[index]);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        public readonly struct ArrayConvertStructList<T> : IList<T>
        {
            private readonly T[] source;

            private readonly Func<object, object> converter;

            public ArrayConvertStructList(T[] source, Func<object, object> converter)
            {
                this.source = source;
                this.converter = converter;
            }

            public IEnumerator<T> GetEnumerator() => new ArrayConvertStructEnumerator<T>(source, converter);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

            public int IndexOf(T item) => throw new NotSupportedException();

            public void Insert(int index, T item) => throw new NotSupportedException();

            public void RemoveAt(int index) => throw new NotSupportedException();

            public T this[int index]
            {
                get => source[index];
                set => source[index] = value;
            }
        }

        //--------------------------------------------------------------------------------
        // ListConvertStruct
        //--------------------------------------------------------------------------------

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

            public void Reset() => index = -1;

            public T Current => (T)converter(source[index]);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        public readonly struct ListConvertStructList<T> : IList<T>
        {
            private readonly IList<T> source;

            private readonly Func<object, object> converter;

            public ListConvertStructList(IList<T> source, Func<object, object> converter)
            {
                this.source = source;
                this.converter = converter;
            }

            public IEnumerator<T> GetEnumerator() => new ListConvertStructEnumerator<T>(source, converter);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

        //--------------------------------------------------------------------------------
        // EnumerableConvertStruct
        //--------------------------------------------------------------------------------

        public struct EnumerableConvertStructEnumerator<T> : IEnumerator<T>
        {
            private readonly IEnumerator<T> source;

            private readonly Func<object, object> converter;

            public EnumerableConvertStructEnumerator(IEnumerator<T> source, Func<object, object> converter)
            {
                this.source = source;
                this.converter = converter;
            }

            public bool MoveNext() => source.MoveNext();

            public void Reset() => source.Reset();

            public T Current => (T)converter(source.Current);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        public readonly struct EnumerableConvertStructList<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> source;

            private readonly Func<object, object> converter;

            public EnumerableConvertStructList(IEnumerable<T> source, Func<object, object> converter)
            {
                this.source = source;
                this.converter = converter;
            }

            public IEnumerator<T> GetEnumerator() => new EnumerableConvertStructEnumerator<T>(source.GetEnumerator(), converter);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
