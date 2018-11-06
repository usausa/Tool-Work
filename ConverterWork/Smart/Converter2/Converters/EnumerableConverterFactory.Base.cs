namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;

    public sealed partial class EnumerableConverterFactory
    {
        //--------------------------------------------------------------------------------
        // Other type by Initialize add
        //--------------------------------------------------------------------------------

        private abstract class OtherTypeCollectionFromArrayByInitializeAddBuilderBase<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            protected OtherTypeCollectionFromArrayByInitializeAddBuilderBase(Func<object, object> converter)
            {
                this.converter = converter;
            }

            protected abstract ICollection<TDestination> CreateCollection(int size);

            public object Create(object source)
            {
                var arraySource = (TSource[])source;
                var collection = CreateCollection(arraySource.Length);
                for (var i = 0; i < arraySource.Length; i++)
                {
                    collection.Add((TDestination)converter(arraySource[i]));
                }

                return collection;
            }
        }

        private abstract class OtherTypeCollectionFromListByInitializeAddBuilderBase<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            protected OtherTypeCollectionFromListByInitializeAddBuilderBase(Func<object, object> converter)
            {
                this.converter = converter;
            }

            protected abstract ICollection<TDestination> CreateCollection(int size);

            public object Create(object source)
            {
                var listSource = (IList<TSource>)source;
                var collection = CreateCollection(listSource.Count);
                for (var i = 0; i < listSource.Count; i++)
                {
                    collection.Add((TDestination)converter(listSource[i]));
                }

                return collection;
            }
        }

        private abstract class OtherTypeCollectionFromCollectionByInitializeAddBuilderBase<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            protected OtherTypeCollectionFromCollectionByInitializeAddBuilderBase(Func<object, object> converter)
            {
                this.converter = converter;
            }

            protected abstract ICollection<TDestination> CreateCollection(int size);

            public object Create(object source)
            {
                var collectionSource = (ICollection<TSource>)source;
                var collection = CreateCollection(collectionSource.Count);
                foreach (var value in collectionSource)
                {
                    collection.Add((TDestination)converter(value));
                }

                return collection;
            }
        }

        //--------------------------------------------------------------------------------
        // Other type by Add
        //--------------------------------------------------------------------------------

        private abstract class OtherTypeCollectionFromEnumerableByAddBuilderBase<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            protected OtherTypeCollectionFromEnumerableByAddBuilderBase(Func<object, object> converter)
            {
                this.converter = converter;
            }

            protected abstract ICollection<TDestination> CreateCollection();

            public object Create(object source)
            {
                var collection = CreateCollection();
                foreach (var value in (IEnumerable<TSource>)source)
                {
                    collection.Add((TDestination)converter(value));
                }

                return collection;
            }
        }
    }
}
