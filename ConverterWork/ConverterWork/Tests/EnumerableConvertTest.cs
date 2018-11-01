namespace Smart.Tests
{
    using System.Collections.Generic;

    using Smart.Converter2.Converters;

    using Xunit;

    public class EnumerableConvertTest
    {
        // TODO IEとしてはint[]を採用

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

        //[Fact]
        //public void ListToSameElementArray()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ListToOtherElementArray()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ArrayListToSameElementArray()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ArrayListToOtherElementArray()
        //{
        //    // TODO
        //}

        ////--------------------------------------------------------------------------------
        //// To List
        ////--------------------------------------------------------------------------------

        //[Fact]
        //public void ListToSameElementList()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ListToOtherElementList()
        //{
        //    var converter = new TestObjectConverter();
        //    var source = new List<int> { 0, 1 };
        //    var destination = (List<string>)converter.Convert(source, typeof(List<string>));
        //    Assert.Equal(2, destination.Count);
        //    Assert.Equal("0", destination[0]);
        //    Assert.Equal("1", destination[1]);
        //    Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        //}

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

        //[Fact]
        //public void ArrayListToSameElementList()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ArrayListToOtherElementList()
        //{
        //    // TODO
        //}

        // IList ?, ICol, IE<>...

        ////--------------------------------------------------------------------------------
        //// To Set
        ////--------------------------------------------------------------------------------

        // TODO
    }
}
