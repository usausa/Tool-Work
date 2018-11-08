namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed partial class EnumerableConverterFactory
    {
        private sealed class SameTypeObservableCollectionBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new SameTypeObservableCollectionBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                return typeof(SameTypeObservableCollectionFromEnumerableBuilder<>);
            }
        }

        private sealed class OtherTypeObservableCollectionBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new OtherTypeObservableCollectionBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                switch (sourceEnumerableType)
                {
                    case SourceEnumerableType.Array:
                        return typeof(OtherTypeObservableCollectionFromArrayBuilder<,>);
                    case SourceEnumerableType.List:
                        return typeof(OtherTypeObservableCollectionFromListBuilder<,>);
                    case SourceEnumerableType.Collection:
                        return typeof(OtherTypeObservableCollectionFromCollectionBuilder<,>);
                    default:
                        return typeof(OtherTypeObservableCollectionFromEnumerableBuilder<,>);
                }
            }
        }

        //--------------------------------------------------------------------------------
        // Same type
        //--------------------------------------------------------------------------------

        private sealed class SameTypeObservableCollectionFromEnumerableBuilder<TDestination> : IConverterBuilder
        {
            public object Create(object source)
            {
                return new ObservableCollection<TDestination>((IEnumerable<TDestination>)source);
            }
        }

        //--------------------------------------------------------------------------------
        // Other type
        //--------------------------------------------------------------------------------

        private sealed class OtherTypeObservableCollectionFromArrayBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeObservableCollectionFromArrayBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new ObservableCollection<TDestination>(new ArrayConvertStructList<TSource, TDestination>((TSource[])source, converter));
            }
        }

        private sealed class OtherTypeObservableCollectionFromListBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeObservableCollectionFromListBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new ObservableCollection<TDestination>(new ListConvertStructList<TSource, TDestination>((IList<TSource>)source, converter));
            }
        }

        private sealed class OtherTypeObservableCollectionFromCollectionBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeObservableCollectionFromCollectionBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new ObservableCollection<TDestination>(new CollectionConvertStructCollection<TSource, TDestination>((ICollection<TSource>)source, converter));
            }
        }

        private sealed class OtherTypeObservableCollectionFromEnumerableBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeObservableCollectionFromEnumerableBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new ObservableCollection<TDestination>(new EnumerableConvertStructEnumerable<TSource, TDestination>((IEnumerable<TSource>)source, converter));
            }
        }
    }
}
