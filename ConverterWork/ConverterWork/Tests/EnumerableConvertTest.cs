namespace Smart.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.Converter2.Converters;

    using Xunit;

    public class EnumerableConvertTest
    {
        //--------------------------------------------------------------------------------
        // To Array
        //--------------------------------------------------------------------------------

        // MEMO same array type is not converted

        [Fact]
        public void ArrayToOtherElementArray()
        {
            var converter = new TestObjectConverter();
            var source = new[] { 0, 1 };
            var destination = (string[])converter.Convert(source, typeof(string[]));
            Assert.Equal(2, destination.Length);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        }

        [Fact]
        public void CollectionToSameElementArray()
        {
            var converter = new TestObjectConverter();
            var source = new WrapperCollection<string>(new[] { "0", "1" });
            var destination = (string[])converter.Convert(source, typeof(string[]));
            Assert.Equal(2, destination.Length);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
        }

        [Fact]
        public void CollectionToOtherElementArray()
        {
            var converter = new TestObjectConverter();
            var source = new WrapperCollection<int>(new[] { 0, 1 });
            var destination = (string[])converter.Convert(source, typeof(string[]));
            Assert.Equal(2, destination.Length);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        }

        [Fact]
        public void EnumerableToSameElementArray()
        {
            var converter = new TestObjectConverter();
            var source = new[] { "0", "1" }.Select(x => x);
            var destination = (string[])converter.Convert(source, typeof(string[]));
            Assert.Equal(2, destination.Length);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
        }

        [Fact]
        public void EnumerableToOtherElementArray()
        {
            var converter = new TestObjectConverter();
            var source = new[] { 0, 1 }.Select(x => x);
            var destination = (string[])converter.Convert(source, typeof(string[]));
            Assert.Equal(2, destination.Length);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        }

        //--------------------------------------------------------------------------------
        // To List
        //--------------------------------------------------------------------------------

        [Fact]
        public void ArrayToSameElementList()
        {
            var converter = new TestObjectConverter();
            var source = new[] { 0, 1 };
            var destination = (List<int>)converter.Convert(source, typeof(List<int>));
            Assert.Equal(2, destination.Count);
            Assert.Equal(0, destination[0]);
            Assert.Equal(1, destination[1]);
            Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
        }

        [Fact]
        public void ArrayToOtherElementList()
        {
            var converter = new TestObjectConverter();
            var source = new[] { 0, 1 };
            var destination = (List<string>)converter.Convert(source, typeof(List<string>));
            Assert.Equal(2, destination.Count);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        }

        [Fact]
        public void CollectionToSameElementList()
        {
            var converter = new TestObjectConverter();
            var source = new WrapperCollection<string>(new[] { "0", "1" });
            var destination = (List<string>)converter.Convert(source, typeof(List<string>));
            Assert.Equal(2, destination.Count);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
        }

        [Fact]
        public void CollectionToOtherElementList()
        {
            var converter = new TestObjectConverter();
            var source = new List<int> { 0, 1 };
            var destination = (List<string>)converter.Convert(source, typeof(List<string>));
            Assert.Equal(2, destination.Count);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        }

        [Fact]
        public void EnumerableToSameElementList()
        {
            var converter = new TestObjectConverter();
            var source = new[] { "0", "1" }.Select(x => x);
            var destination = (List<string>)converter.Convert(source, typeof(List<string>));
            Assert.Equal(2, destination.Count);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
        }

        [Fact]
        public void EnumerableToOtherElementList()
        {
            var converter = new TestObjectConverter();
            var source = new[] { 0, 1 }.Select(x => x);
            var destination = (List<string>)converter.Convert(source, typeof(List<string>));
            Assert.Equal(2, destination.Count);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        }

        //--------------------------------------------------------------------------------
        // To Set
        //--------------------------------------------------------------------------------

        [Fact]
        public void ArrayToSameElementHashSet()
        {
            var converter = new TestObjectConverter();
            var source = new[] { 0, 1 };
            var destination = (HashSet<int>)converter.Convert(source, typeof(HashSet<int>));
            Assert.Equal(2, destination.Count);
            Assert.Contains(0, destination);
            Assert.Contains(1, destination);
            Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
        }

        [Fact]
        public void ArrayToOtherElementHashSet()
        {
            var converter = new TestObjectConverter();
            var source = new[] { 0, 1 };
            var destination = (HashSet<string>)converter.Convert(source, typeof(HashSet<string>));
            Assert.Equal(2, destination.Count);
            Assert.Contains("0", destination);
            Assert.Contains("1", destination);
            Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        }

        [Fact]
        public void CollectionToSameElementHashSet()
        {
            var converter = new TestObjectConverter();
            var source = new WrapperCollection<string>(new[] { "0", "1" });
            var destination = (HashSet<string>)converter.Convert(source, typeof(HashSet<string>));
            Assert.Equal(2, destination.Count);
            Assert.Contains("0", destination);
            Assert.Contains("1", destination);
            Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
        }

        [Fact]
        public void CollectionToOtherElementHashSet()
        {
            var converter = new TestObjectConverter();
            var source = new HashSet<int> { 0, 1 };
            var destination = (HashSet<string>)converter.Convert(source, typeof(HashSet<string>));
            Assert.Equal(2, destination.Count);
            Assert.Contains("0", destination);
            Assert.Contains("1", destination);
            Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        }

        [Fact]
        public void EnumerableToSameElementHashSet()
        {
            var converter = new TestObjectConverter();
            var source = new[] { "0", "1" }.Select(x => x);
            var destination = (HashSet<string>)converter.Convert(source, typeof(HashSet<string>));
            Assert.Equal(2, destination.Count);
            Assert.Contains("0", destination);
            Assert.Contains("1", destination);
            Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
        }

        [Fact]
        public void EnumerableToOtherElementHashSet()
        {
            var converter = new TestObjectConverter();
            var source = new[] { 0, 1 }.Select(x => x);
            var destination = (HashSet<string>)converter.Convert(source, typeof(HashSet<string>));
            Assert.Equal(2, destination.Count);
            Assert.Contains("0", destination);
            Assert.Contains("1", destination);
            Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        }

        //--------------------------------------------------------------------------------
        // Helper
        //--------------------------------------------------------------------------------

        public class WrapperCollection<T> : ICollection<T>
        {
            private readonly List<T> list;

            public WrapperCollection(IEnumerable<T> source)
            {
                list = source.ToList();
            }

            public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public void Add(T item) => list.Add(item);

            public void Clear() => list.Clear();

            public bool Contains(T item) => throw new NotSupportedException();

            public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

            public bool Remove(T item) => throw new NotSupportedException();

            public int Count => list.Count;

            public bool IsReadOnly => false;
        }
    }
}
