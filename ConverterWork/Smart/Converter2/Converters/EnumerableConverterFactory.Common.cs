﻿namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public sealed partial class EnumerableConverterFactory
    {
        //--------------------------------------------------------------------------------
        // ArrayConvertStruct
        //--------------------------------------------------------------------------------

        public struct ArrayConvertStructEnumerator<TSource, TDestination> : IEnumerator<TDestination>
        {
            private readonly TSource[] source;

            private readonly Func<object, object> converter;

            private int index;

            public ArrayConvertStructEnumerator(TSource[] source, Func<object, object> converter)
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

            public TDestination Current => (TDestination)converter(source[index]);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        public readonly struct ArrayConvertStructList<TSource, TDestination> : IList<TDestination>
        {
            private readonly TSource[] source;

            private readonly Func<object, object> converter;

            public ArrayConvertStructList(TSource[] source, Func<object, object> converter)
            {
                this.source = source;
                this.converter = converter;
            }

            public IEnumerator<TDestination> GetEnumerator() => new ArrayConvertStructEnumerator<TSource, TDestination>(source, converter);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public void Add(TDestination item) => throw new NotSupportedException();

            public void Clear() => throw new NotSupportedException();

            public bool Contains(TDestination item) => throw new NotSupportedException();

            public void CopyTo(TDestination[] array, int arrayIndex)
            {
                for (var i = 0; i < source.Length; i++)
                {
                    array[arrayIndex + i] = (TDestination)converter(source[i]);
                }
            }

            public bool Remove(TDestination item) => throw new NotSupportedException();

            public int Count => source.Length;

            public bool IsReadOnly => true;

            public int IndexOf(TDestination item) => throw new NotSupportedException();

            public void Insert(int index, TDestination item) => throw new NotSupportedException();

            public void RemoveAt(int index) => throw new NotSupportedException();

            public TDestination this[int index]
            {
                get => (TDestination)converter(source[index]);
                set => throw new NotSupportedException();
            }
        }

        //--------------------------------------------------------------------------------
        // ListConvertStruct
        //--------------------------------------------------------------------------------

        public struct ListConvertStructEnumerator<TSource, TDestination> : IEnumerator<TDestination>
        {
            private readonly IList<TSource> source;

            private readonly Func<object, object> converter;

            private int index;

            public ListConvertStructEnumerator(IList<TSource> source, Func<object, object> converter)
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

            public TDestination Current => (TDestination)converter(source[index]);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        public readonly struct ListConvertStructList<TSource, TDestination> : IList<TDestination>
        {
            private readonly IList<TSource> source;

            private readonly Func<object, object> converter;

            public ListConvertStructList(IList<TSource> source, Func<object, object> converter)
            {
                this.source = source;
                this.converter = converter;
            }

            public IEnumerator<TDestination> GetEnumerator() => new ListConvertStructEnumerator<TSource, TDestination>(source, converter);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public void Add(TDestination item) => throw new NotSupportedException();

            public void Clear() => throw new NotSupportedException();

            public bool Contains(TDestination item) => throw new NotSupportedException();

            public void CopyTo(TDestination[] array, int arrayIndex)
            {
                for (var i = 0; i < source.Count; i++)
                {
                    array[arrayIndex + i] = (TDestination)converter(source[i]);
                }
            }

            public bool Remove(TDestination item) => throw new NotSupportedException();

            public int Count => source.Count;

            public bool IsReadOnly => true;

            public int IndexOf(TDestination item) => throw new NotSupportedException();

            public void Insert(int index, TDestination item) => throw new NotSupportedException();

            public void RemoveAt(int index) => throw new NotSupportedException();

            public TDestination this[int index]
            {
                get => (TDestination)converter(source[index]);
                set => throw new NotSupportedException();
        }
        }

        //--------------------------------------------------------------------------------
        // EnumerableConvertStruct
        //--------------------------------------------------------------------------------

        public struct EnumerableConvertStructEnumerator<TSource, TDestination> : IEnumerator<TDestination>
        {
            private readonly IEnumerator<TSource> source;

            private readonly Func<object, object> converter;

            public EnumerableConvertStructEnumerator(IEnumerator<TSource> source, Func<object, object> converter)
            {
                this.source = source;
                this.converter = converter;
            }

            public bool MoveNext() => source.MoveNext();

            public void Reset() => source.Reset();

            public TDestination Current => (TDestination)converter(source.Current);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}
