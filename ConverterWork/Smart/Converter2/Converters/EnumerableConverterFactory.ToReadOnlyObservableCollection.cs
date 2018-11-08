namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed partial class EnumerableConverterFactory
    {
        private sealed class SameTypeReadOnlyObservableCollectionBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new SameTypeReadOnlyObservableCollectionBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                return typeof(SameTypeReadOnlyObservableCollectionFromEnumerableBuilder<>);
            }
        }

        private sealed class OtherTypeReadOnlyObservableCollectionBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new OtherTypeReadOnlyObservableCollectionBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                switch (sourceEnumerableType)
                {
                    case SourceEnumerableType.Array:
                        return typeof(OtherTypeReadOnlyObservableCollectionFromArrayBuilder<,>);
                    case SourceEnumerableType.List:
                        return typeof(OtherTypeReadOnlyObservableCollectionFromListBuilder<,>);
                    case SourceEnumerableType.Collection:
                        return typeof(OtherTypeReadOnlyObservableCollectionFromCollectionBuilder<,>);
                    default:
                        return typeof(OtherTypeReadOnlyObservableCollectionFromEnumerableBuilder<,>);
                }
            }
        }

        //--------------------------------------------------------------------------------
        // Same type
        //--------------------------------------------------------------------------------

        private sealed class SameTypeReadOnlyObservableCollectionFromEnumerableBuilder<TDestination> : IConverterBuilder
        {
            public object Create(object source)
            {
                return new ReadOnlyObservableCollection<TDestination>(new ObservableCollection<TDestination>((IEnumerable<TDestination>)source));
            }
        }

        //--------------------------------------------------------------------------------
        // Other type
        //--------------------------------------------------------------------------------

        private sealed class OtherTypeReadOnlyObservableCollectionFromArrayBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeReadOnlyObservableCollectionFromArrayBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new ReadOnlyObservableCollection<TDestination>(new ObservableCollection<TDestination>(new ArrayConvertStructList<TSource, TDestination>((TSource[])source, converter)));
            }
        }

        private sealed class OtherTypeReadOnlyObservableCollectionFromListBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeReadOnlyObservableCollectionFromListBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new ReadOnlyObservableCollection<TDestination>(new ObservableCollection<TDestination>(new ListConvertStructList<TSource, TDestination>((IList<TSource>)source, converter)));
            }
        }

        private sealed class OtherTypeReadOnlyObservableCollectionFromCollectionBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeReadOnlyObservableCollectionFromCollectionBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new ReadOnlyObservableCollection<TDestination>(new ObservableCollection<TDestination>(new CollectionConvertStructCollection<TSource, TDestination>((ICollection<TSource>)source, converter)));
            }
        }

        private sealed class OtherTypeReadOnlyObservableCollectionFromEnumerableBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeReadOnlyObservableCollectionFromEnumerableBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new ReadOnlyObservableCollection<TDestination>(new ObservableCollection<TDestination>(new EnumerableConvertStructEnumerable<TSource, TDestination>((IEnumerable<TSource>)source, converter)));
            }
        }
    }
}
