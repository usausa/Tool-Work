﻿using System.Collections.Generic;
using Smart.Converter2.Converters;

namespace Smart.Tests
{
    using Xunit;

    public class EnumerableConvertTest
    {
        //--------------------------------------------------------------------------------
        // To Array
        //--------------------------------------------------------------------------------

        // TODO Assignable/Enumerable

        //[Fact]
        //public void ArrayToSameElementArray()
        //{
        //    // TODO copy?
        //}

        //[Fact]
        //public void ArrayToOtherElementArray()
        //{
        //}

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

        [Fact]
        public void ListToOtherElementList()
        {
            var converter = new TestObjectConverter();
            var source = new List<int> { 0, 1 };
            var destination = (List<string>)converter.Convert(source, typeof(List<string>));
            Assert.Equal(2, destination.Count);
            Assert.Equal("0", destination[0]);
            Assert.Equal("1", destination[1]);
            Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
        }

        // IList ?, ICol, IE<>...

        //[Fact]
        //public void ArrayToSameElementList()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ArrayToOtherElementList()
        //{
        //    // TODO
        //}

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

        ////--------------------------------------------------------------------------------
        //// To Set
        ////--------------------------------------------------------------------------------

        //[Fact]
        //public void SetToSameElementSet()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void SetToOtherElementSet()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ArrayToSameElementSet()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ArrayToOtherElementSet()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ListToSameElementSet()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ListToOtherElementSet()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ArrayListToSameElementSet()
        //{
        //    // TODO
        //}

        //[Fact]
        //public void ArrayListToOtherElementSet()
        //{
        //    // TODO
        //}
    }
}
