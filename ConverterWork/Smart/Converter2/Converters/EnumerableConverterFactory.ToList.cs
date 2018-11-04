namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed partial class EnumerableConverterFactory
    {
        private sealed class SameTypeListBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new SameTypeListBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                return typeof(SameTypeListFromEnumerableBuilder<>);
            }
        }

        private sealed class OtherTypeListBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new OtherTypeListBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                switch (sourceEnumerableType)
                {
                    case SourceEnumerableType.Array:
                        return typeof(OtherTypeListFromArrayBuilder<,>);
                    case SourceEnumerableType.List:
                        return typeof(OtherTypeListFromListBuilder<,>);
                    case SourceEnumerableType.Collection:
                        return typeof(OtherTypeListFromCollectionBuilder<,>);
                    default:
                        return typeof(OtherTypeListFromEnumerableBuilder<,>);
                }
            }
        }

        //--------------------------------------------------------------------------------
        // Same type
        //--------------------------------------------------------------------------------

        private sealed class SameTypeListFromEnumerableBuilder<TDestination> : SameTypeEnumerableFromEnumerableByFactoryBuilderBase<TDestination>
        {
            protected override IEnumerable<TDestination> CreateCollection(IEnumerable<TDestination> source)
            {
                return new List<TDestination>(source);
            }
        }

        //--------------------------------------------------------------------------------
        // Other type
        //--------------------------------------------------------------------------------

        private sealed class OtherTypeListFromArrayBuilder<TSource, TDestination> : OtherTypeCollectionFromArrayByInitializeAddBuilderBase<TSource, TDestination>
        {
            public OtherTypeListFromArrayBuilder(Func<object, object> converter)
                : base(converter)
            {
            }

            protected override ICollection<TDestination> CreateCollection(int size) => new List<TDestination>(size);
        }

        private sealed class OtherTypeListFromListBuilder<TSource, TDestination> : OtherTypeCollectionFromListByInitializeAddBuilderBase<TSource, TDestination>
        {
            public OtherTypeListFromListBuilder(Func<object, object> converter)
                : base(converter)
            {
            }

            protected override ICollection<TDestination> CreateCollection(int size) => new List<TDestination>(size);
        }

        private sealed class OtherTypeListFromCollectionBuilder<TSource, TDestination> : OtherTypeCollectionFromCollectionByInitializeAddBuilderBase<TSource, TDestination>
        {
            public OtherTypeListFromCollectionBuilder(Func<object, object> converter)
                : base(converter)
            {
            }

            protected override ICollection<TDestination> CreateCollection(int size) => new List<TDestination>(size);
        }

        private sealed class OtherTypeListFromEnumerableBuilder<TSource, TDestination> : OtherTypeCollectionFromEnumerableByAddBuilderBase<TSource, TDestination>
        {
            public OtherTypeListFromEnumerableBuilder(Func<object, object> converter)
                : base(converter)
            {
            }

            protected override ICollection<TDestination> CreateCollection() => new List<TDestination>();
        }
    }
}
