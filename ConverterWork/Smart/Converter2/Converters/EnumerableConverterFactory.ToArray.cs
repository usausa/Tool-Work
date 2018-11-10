namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Smart.Collections.Generics;

    public sealed partial class EnumerableConverterFactory
    {
        private sealed class SameTypeArrayBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new SameTypeArrayBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                switch (sourceEnumerableType)
                {
                    case SourceEnumerableType.Array:
                        // Used assignable
                        throw new NotSupportedException();
                    case SourceEnumerableType.List:
                    case SourceEnumerableType.Collection:
                        return typeof(SameTypeArrayFromCollectionBuilder<>);
                    default:
                        return typeof(SameTypeArrayFromEnumerableBuilder<>);
                }
            }
        }

        private sealed class OtherTypeArrayBuilderProvider : IConverterBuilderProvider
        {
            public static IConverterBuilderProvider Default { get; } = new OtherTypeArrayBuilderProvider();

            public Type GetBuilderType(SourceEnumerableType sourceEnumerableType)
            {
                switch (sourceEnumerableType)
                {
                    case SourceEnumerableType.Array:
                        return typeof(OtherTypeArrayFromArrayBuilder<,>);
                    case SourceEnumerableType.List:
                        return typeof(OtherTypeArrayFromListBuilder<,>);
                    case SourceEnumerableType.Collection:
                        return typeof(OtherTypeArrayFromCollectionBuilder<,>);
                    default:
                        return typeof(OtherTypeArrayFromEnumerableBuilder<,>);
                }
            }
        }

        //--------------------------------------------------------------------------------
        // Same type
        //--------------------------------------------------------------------------------

        private sealed class SameTypeArrayFromCollectionBuilder<TDestination> : IConverterBuilder
        {
            public object Create(object source)
            {
                var sourceCollection = (ICollection<TDestination>)source;
                var array = new TDestination[sourceCollection.Count];
                sourceCollection.CopyTo(array, 0);

                return array;
            }
        }

        private sealed class SameTypeArrayFromEnumerableBuilder<TDestination> : IConverterBuilder
        {
            public object Create(object source)
            {
                var buffer = new ArrayBuffer<TDestination>(0);
                foreach (var value in (IEnumerable)source)
                {
                    buffer.Add((TDestination)value);
                }

                return buffer.ToArray();
            }
        }

        //--------------------------------------------------------------------------------
        // Builder to other type Array from Collection
        //--------------------------------------------------------------------------------

        public sealed class OtherTypeArrayFromArrayBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeArrayFromArrayBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                var sourceArray = (TSource[])source;
                var array = new TDestination[sourceArray.Length];
                for (var i = 0; i < sourceArray.Length; i++)
                {
                    array[i] = (TDestination)converter(sourceArray[i]);
                }

                return array;
            }
        }

        private sealed class OtherTypeArrayFromListBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeArrayFromListBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                var sourceList = (IList<TSource>)source;
                var array = new TDestination[sourceList.Count];
                for (var i = 0; i < sourceList.Count; i++)
                {
                    array[i] = (TDestination)converter(sourceList[i]);
                }

                return array;
            }
        }

        private sealed class OtherTypeArrayFromCollectionBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeArrayFromCollectionBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                var sourceCollection = (ICollection<TSource>)source;
                var array = new TDestination[sourceCollection.Count];
                var index = 0;
                foreach (var value in sourceCollection)
                {
                    array[index] = (TDestination)converter(value);
                    index++;
                }

                return array;
            }
        }

        private sealed class OtherTypeArrayFromEnumerableBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public OtherTypeArrayFromEnumerableBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                var buffer = new ArrayBuffer<TDestination>(0);
                foreach (var value in (IEnumerable<TSource>)source)
                {
                    buffer.Add((TDestination)converter(value));
                }

                return buffer.ToArray();
            }
        }
    }
}
