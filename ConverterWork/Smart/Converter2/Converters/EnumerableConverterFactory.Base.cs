namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Smart.Collections.Generics;

    public sealed partial class EnumerableConverterFactory
    {
        //--------------------------------------------------------------------------------
        // Same type
        //--------------------------------------------------------------------------------

        private abstract class SameTypeEnumerableFromEnumerableByFactoryBuilderBase<TDestination> : IConverterBuilder
        {
            protected abstract IEnumerable<TDestination> CreateCollection(IEnumerable<TDestination> source);

            public object Create(object source)
            {
                return CreateCollection((IEnumerable<TDestination>)source);
            }
        }

        private abstract class SameTypeListFromArrayByInitializeAddBuilderBase<TDestination> : IConverterBuilder
        {
            protected abstract IList<TDestination> CreateCollection(int size);

            public object Create(object source)
            {
                var arraySource = (TDestination[])source;
                var list = CreateCollection(arraySource.Length);
                for (var i = 0; i < arraySource.Length; i++)
                {
                    list.Add(arraySource[i]);
                }

                return list;
            }
        }


        //--------------------------------------------------------------------------------
        // Other type
        //--------------------------------------------------------------------------------

        private abstract class OtherTypeListFromArrayByInitializeAddBuilderBase<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            protected OtherTypeListFromArrayByInitializeAddBuilderBase(Func<object, object> converter)
            {
                this.converter = converter;
            }

            protected abstract IList<TDestination> CreateCollection(int size);

            public object Create(object source)
            {
                var arraySource = (TSource[])source;
                var list = CreateCollection(arraySource.Length);
                for (var i = 0; i < arraySource.Length; i++)
                {
                    list.Add((TDestination)converter(arraySource[i]));
                }

                return list;
            }
        }

        private abstract class OtherTypeListFromListByInitializeAddBuilderBase<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            protected OtherTypeListFromListByInitializeAddBuilderBase(Func<object, object> converter)
            {
                this.converter = converter;
            }

            protected abstract IList<TDestination> CreateCollection(int size);

            public object Create(object source)
            {
                var listSource = (IList<TSource>)source;
                var list = CreateCollection(listSource.Count);
                for (var i = 0; i < listSource.Count; i++)
                {
                    list.Add((TDestination)converter(listSource[i]));
                }

                return list;
            }
        }

        private abstract class OtherTypeListFromCollectionByInitializeAddBuilderBase<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            protected OtherTypeListFromCollectionByInitializeAddBuilderBase(Func<object, object> converter)
            {
                this.converter = converter;
            }

            protected abstract IList<TDestination> CreateCollection(int size);

            public object Create(object source)
            {
                var collectionSource = (ICollection<TSource>)source;
                var list = CreateCollection(collectionSource.Count);
                foreach (var value in collectionSource)
                {
                    list.Add((TDestination)converter(value));
                }

                return list;
            }
        }

        private abstract class OtherTypeListFromEnumerableByAddBuilderBase<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            protected OtherTypeListFromEnumerableByAddBuilderBase(Func<object, object> converter)
            {
                this.converter = converter;
            }

            protected abstract IList<TDestination> CreateCollection();

            public object Create(object source)
            {
                var collectionSource = (IEnumerable<TSource>)source;
                var list = CreateCollection();
                foreach (var value in collectionSource)
                {
                    list.Add((TDestination)converter(value));
                }

                return list;
            }
        }
    }
}
