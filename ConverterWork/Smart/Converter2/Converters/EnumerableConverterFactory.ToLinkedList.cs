namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed partial class EnumerableConverterFactory
    {
        private sealed class SameTypeLinkedListBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new SameTypeLinkedListBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                return typeof(SameTypeLinkedListFromEnumerableBuilder<>);
            }
        }

        private sealed class OtherTypeLinkedListBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new OtherTypeLinkedListBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                switch (sourceEnumerableType)
                {
                    case SourceEnumerableType.Array:
                        return typeof(OtherTypeLinkedListFromArrayBuilder<,>);
                    case SourceEnumerableType.List:
                        return typeof(OtherTypeLinkedListFromListBuilder<,>);
                    default:
                        return typeof(OtherTypeLinkedListFromEnumerableBuilder<,>);
                }
            }
        }

        //--------------------------------------------------------------------------------
        // Same type
        //--------------------------------------------------------------------------------

        private sealed class SameTypeLinkedListFromEnumerableBuilder<TDestination> : IConverterBuilder
        {
            public object Create(object source)
            {
                return new LinkedList<TDestination>((IEnumerable<TDestination>)source);
            }
        }

        //--------------------------------------------------------------------------------
        // Other type
        //--------------------------------------------------------------------------------

        private sealed class OtherTypeLinkedListFromArrayBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeLinkedListFromArrayBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                var arraySource = (TSource[])source;
                var collection = new LinkedList<TDestination>();
                for (var i = 0; i < arraySource.Length; i++)
                {
                    collection.AddLast((TDestination)converter(arraySource[i]));
                }

                return collection;
            }
        }

        private sealed class OtherTypeLinkedListFromListBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeLinkedListFromListBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                var listSource = (IList<TSource>)source;
                var collection = new LinkedList<TDestination>();
                for (var i = 0; i < listSource.Count; i++)
                {
                    collection.AddLast((TDestination)converter(listSource[i]));
                }

                return collection;
            }
        }

        private sealed class OtherTypeLinkedListFromEnumerableBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeLinkedListFromEnumerableBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                var collection = new LinkedList<TDestination>();
                foreach (var value in (IEnumerable<TSource>)source)
                {
                    collection.AddLast((TDestination)converter(value));
                }

                return collection;
            }
        }
    }
}
