namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed partial class EnumerableConverterFactory
    {
        private sealed class SameTypeStackBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new SameTypeStackBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                return typeof(SameTypeStackFromEnumerableBuilder<>);
            }
        }

        private sealed class OtherTypeStackBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new OtherTypeStackBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                switch (sourceEnumerableType)
                {
                    case SourceEnumerableType.Array:
                        return typeof(OtherTypeStackFromArrayBuilder<,>);
                    case SourceEnumerableType.List:
                        return typeof(OtherTypeStackFromListBuilder<,>);
                    case SourceEnumerableType.Collection:
                        return typeof(OtherTypeStackFromCollectionBuilder<,>);
                    default:
                        return typeof(OtherTypeStackFromEnumerableBuilder<,>);
                }
            }
        }

        //--------------------------------------------------------------------------------
        // Same type
        //--------------------------------------------------------------------------------

        private sealed class SameTypeStackFromEnumerableBuilder<TDestination> : IConverterBuilder
        {
            public object Create(object source)
            {
                return new Stack<TDestination>((IEnumerable<TDestination>)source);
            }
        }

        //--------------------------------------------------------------------------------
        // Other type
        //--------------------------------------------------------------------------------

        private sealed class OtherTypeStackFromArrayBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeStackFromArrayBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new Stack<TDestination>(new ArrayConvertStructList<TSource, TDestination>((TSource[])source, converter));
            }
        }

        private sealed class OtherTypeStackFromListBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeStackFromListBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new Stack<TDestination>(new ListConvertStructList<TSource, TDestination>((IList<TSource>)source, converter));
            }
        }

        private sealed class OtherTypeStackFromCollectionBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeStackFromCollectionBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new Stack<TDestination>(new CollectionConvertStructCollection<TSource, TDestination>((ICollection<TSource>)source, converter));
            }
        }

        private sealed class OtherTypeStackFromEnumerableBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeStackFromEnumerableBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new Stack<TDestination>(new EnumerableConvertStructEnumerable<TSource, TDestination>((IEnumerable<TSource>)source, converter));
            }
        }
    }
}
