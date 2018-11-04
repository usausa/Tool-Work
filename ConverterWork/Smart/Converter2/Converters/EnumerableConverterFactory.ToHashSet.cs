namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed partial class EnumerableConverterFactory
    {
        private sealed class SameTypeHashSetBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new SameTypeHashSetBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                return typeof(SameTypeHashSetFromEnumerableBuilder<>);
            }
        }

        private sealed class OtherTypeHashSetBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new OtherTypeHashSetBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                return typeof(OtherTypeHashSetFromEnumerableBuilder<,>);
            }
        }

        //--------------------------------------------------------------------------------
        // Same type
        //--------------------------------------------------------------------------------

        private sealed class SameTypeHashSetFromEnumerableBuilder<TDestination> : SameTypeEnumerableFromEnumerableByFactoryBuilderBase<TDestination>
        {
            protected override IEnumerable<TDestination> CreateCollection(IEnumerable<TDestination> source)
            {
                return new HashSet<TDestination>(source);
            }
        }

        //--------------------------------------------------------------------------------
        // Other type
        //--------------------------------------------------------------------------------

        private sealed class OtherTypeHashSetFromEnumerableBuilder<TSource, TDestination> : OtherTypeCollectionFromEnumerableByAddBuilderBase<TSource, TDestination>
        {
            public OtherTypeHashSetFromEnumerableBuilder(Func<object, object> converter)
                : base(converter)
            {
            }

            protected override ICollection<TDestination> CreateCollection() => new HashSet<TDestination>();
        }
    }
}
