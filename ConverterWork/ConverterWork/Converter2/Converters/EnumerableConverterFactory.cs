namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.Collections.Generics;

    public sealed class EnumerableConverterFactory : IConverterFactory
    {
        private static readonly Type OpenListType = typeof(List<>);
        //private static readonly Type OpenHashSetType = typeof(HashSet<>);

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            var sourceElementType = sourceType.GetCollectionElementType();
            var targetElementType = targetType.GetCollectionElementType();
            if ((sourceElementType == null) || (targetElementType == null))
            {
                return null;
            }

            // To Array
            if (targetType.IsArray)
            {
                return GetToArrayConverter(context, sourceType, sourceElementType, targetElementType);
            }

            // To List
            var listClosedTpe = OpenListType.MakeGenericType(targetElementType);
            if (targetType.IsAssignableFrom(listClosedTpe))
            {
                return GetToListConverter(context, sourceType, sourceElementType, targetElementType);
            }

            //var setClosedType = OpenHashSetType.MakeGenericType(targetElementType);
            //if (targetType.IsAssignableFrom(setClosedType))
            //{
            // TODO
            //}

            return null;
        }

        private static bool GetConverter(IObjectConverter context, Type sourceType, Type targetType, out Func<object, object> converter)
        {
            if (sourceType == targetType)
            {
                converter = null;
                return true;
            }

            converter = context.CreateConverter(sourceType, targetType);
            return converter != null;
        }

        private Func<object, object> GetToArrayConverter(IObjectConverter context, Type sourceType, Type sourceElementType, Type targetElementType)
        {
            if (!GetConverter(context, sourceElementType, targetElementType, out var converter))
            {
                return null;
            }

            // T1[] to T2[]
            if (sourceType.IsArray)
            {
                return ((IConverterBuilder)Activator.CreateInstance(
                    typeof(ArrayToOtherTypeArrayConverter<,>).MakeGenericType(sourceElementType, targetElementType),
                    converter)).Create;
            }

            var getSize = ResolveSizeFunction(sourceType, sourceElementType);

            if ((converter == null) && (getSize != null))
            {
                // KnownSize same
                return ((IConverterBuilder)Activator.CreateInstance(
                    typeof(KnownSizeEnumerableToSameTypeArrayBuilder<>).MakeGenericType(targetElementType),
                    getSize)).Create;
            }

            if (getSize != null)
            {
                // KnownSize other
                return ((IConverterBuilder)Activator.CreateInstance(
                    typeof(KnownSizeEnumerableToOtherTypeArrayBuilder<>).MakeGenericType(targetElementType),
                    getSize,
                    converter)).Create;
            }

            if (converter == null)
            {
                // UnknownSize same
                return ((IConverterBuilder)Activator.CreateInstance(
                    typeof(UnknownSizeEnumerableToSameTypeArrayBuilder<>).MakeGenericType(targetElementType))).Create;
            }

            // UnknownSize other
            return ((IConverterBuilder)Activator.CreateInstance(
                typeof(UnknownSizeEnumerableToOtherTypeArrayBuilder<>).MakeGenericType(targetElementType),
                converter)).Create;
        }

        private Func<object, object> GetToListConverter(IObjectConverter context, Type sourceType, Type sourceElementType, Type targetElementType)
        {
            if (!GetConverter(context, sourceElementType, targetElementType, out var converter))
            {
                return null;
            }

            if (sourceType.IsArray)
            {
                // T[] to List<T>
                if (converter == null)
                {
                    return ((IConverterBuilder)Activator.CreateInstance(
                        typeof(ArrayToSameTypeListConverter<>).MakeGenericType(targetElementType))).Create;
                }

                // T1[] to List<T2>
                return ((IConverterBuilder)Activator.CreateInstance(
                    typeof(ArrayToOtherTypeListConverter<,>).MakeGenericType(sourceElementType, targetElementType),
                    converter)).Create;
            }

            var getSize = ResolveSizeFunction(sourceType, sourceElementType);

            if ((converter == null) && (getSize != null))
            {
                // KnownSize same
                return ((IConverterBuilder)Activator.CreateInstance(
                    typeof(KnownSizeEnumerableToSameTypeListBuilder<>).MakeGenericType(targetElementType),
                    getSize)).Create;
            }

            if (getSize != null)
            {
                // KnownSize other
                return ((IConverterBuilder)Activator.CreateInstance(
                    typeof(KnownSizeEnumerableToOtherTypeListBuilder<>).MakeGenericType(targetElementType),
                    getSize,
                    converter)).Create;
            }

            if (converter == null)
            {
                // UnknownSize same
                return ((IConverterBuilder)Activator.CreateInstance(
                    typeof(UnknownSizeEnumerableToSameTypeListBuilder<>).MakeGenericType(targetElementType))).Create;
            }

            // UnknownSize other
            return ((IConverterBuilder)Activator.CreateInstance(
                typeof(UnknownSizeEnumerableToOtherTypeListBuilder<>).MakeGenericType(targetElementType),
                converter)).Create;
        }

        //--------------------------------------------------------------------------------
        // Size resolver
        //--------------------------------------------------------------------------------

        private static Func<object, int> ResolveSizeFunction(Type type, Type elementType)
        {
            var collectionType = type.GetInterfaces()
                .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>));
            if ((collectionType != null) && (collectionType.GenericTypeArguments[0] == elementType))
            {
                var genericResolverType = typeof(CollectionSizeResolver<>).MakeGenericType(elementType);
                var genericResolver = (ISizeResolver)Activator.CreateInstance(genericResolverType);
                return genericResolver.ResolveSizeFunction();
            }

            var readonlyCollectionType = type.GetInterfaces()
                .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>));
            if ((readonlyCollectionType != null) && (readonlyCollectionType.GenericTypeArguments[0] == elementType))
            {
                var genericResolverType = typeof(ReadOnlyCollectionSizeResolver<>).MakeGenericType(elementType);
                var genericResolver = (ISizeResolver)Activator.CreateInstance(genericResolverType);
                return genericResolver.ResolveSizeFunction();
            }

            if (typeof(ICollection).IsAssignableFrom(type))
            {
                return x => ((ICollection)x).Count;
            }

            return null;
        }

        private interface ISizeResolver
        {
            Func<object, int> ResolveSizeFunction();
        }

        private sealed class CollectionSizeResolver<T> : ISizeResolver
        {
            public Func<object, int> ResolveSizeFunction()
            {
                return x => ((ICollection<T>)x).Count;
            }
        }

        private sealed class ReadOnlyCollectionSizeResolver<T> : ISizeResolver
        {
            public Func<object, int> ResolveSizeFunction()
            {
                return x => ((IReadOnlyCollection<T>)x).Count;
            }
        }

        //--------------------------------------------------------------------------------
        // Builder
        //--------------------------------------------------------------------------------

        public interface IConverterBuilder
        {
            object Create(object source);
        }

        //--------------------------------------------------------------------------------
        // Builder To Array
        //--------------------------------------------------------------------------------

        public sealed class ArrayToOtherTypeArrayConverter<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public ArrayToOtherTypeArrayConverter(Func<object, object> converter)
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

        private sealed class KnownSizeEnumerableToSameTypeArrayBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<object, int> getSize;

            public KnownSizeEnumerableToSameTypeArrayBuilder(Func<object, int> getSize)
            {
                this.getSize = getSize;
            }

            public object Create(object source)
            {
                var size = getSize(source);
                var array = new TDestination[size];
                var index = 0;
                foreach (var value in (IEnumerable)source)
                {
                    array[index] = (TDestination)value;
                    index++;
                }

                return array;
            }
        }

        private sealed class KnownSizeEnumerableToOtherTypeArrayBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<object, int> getSize;

            private readonly Func<object, object> converter;

            public KnownSizeEnumerableToOtherTypeArrayBuilder(Func<object, int> getSize, Func<object, object> converter)
            {
                this.getSize = getSize;
                this.converter = converter;
            }

            public object Create(object source)
            {
                var size = getSize(source);
                var array = new TDestination[size];
                var index = 0;
                foreach (var value in (IEnumerable)source)
                {
                    array[index] = (TDestination)converter(value);
                    index++;
                }

                return array;
            }
        }

        private sealed class UnknownSizeEnumerableToSameTypeArrayBuilder<TDestination> : IConverterBuilder
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

        private sealed class UnknownSizeEnumerableToOtherTypeArrayBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public UnknownSizeEnumerableToOtherTypeArrayBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                var buffer = new ArrayBuffer<TDestination>(0);
                foreach (var value in (IEnumerable)source)
                {
                    buffer.Add((TDestination)converter(value));
                }

                return buffer.ToArray();
            }
        }

        //--------------------------------------------------------------------------------
        // Builder To List
        //--------------------------------------------------------------------------------

        public sealed class ArrayToSameTypeListConverter<TDestination> : IConverterBuilder
        {
            public object Create(object source)
            {
                var sourceArray = (TDestination[])source;
                var list = new List<TDestination>(sourceArray.Length);
                for (var i = 0; i < sourceArray.Length; i++)
                {
                    list.Add(sourceArray[i]);
                }

                return list;
            }
        }

        public sealed class ArrayToOtherTypeListConverter<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public ArrayToOtherTypeListConverter(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                var sourceArray = (TSource[])source;
                var list = new List<TDestination>(sourceArray.Length);
                for (var i = 0; i < sourceArray.Length; i++)
                {
                    list.Add((TDestination)converter(sourceArray[i]));
                }

                return list;
            }
        }

        private sealed class KnownSizeEnumerableToSameTypeListBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<object, int> getSize;

            public KnownSizeEnumerableToSameTypeListBuilder(Func<object, int> getSize)
            {
                this.getSize = getSize;
            }

            public object Create(object source)
            {
                var size = getSize(source);
                var list = new List<TDestination>(size);
                foreach (var value in (IEnumerable)source)
                {
                    list.Add((TDestination)value);
                }

                return list;
            }
        }

        private sealed class KnownSizeEnumerableToOtherTypeListBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<object, int> getSize;

            private readonly Func<object, object> converter;

            public KnownSizeEnumerableToOtherTypeListBuilder(Func<object, int> getSize, Func<object, object> converter)
            {
                this.getSize = getSize;
                this.converter = converter;
            }

            public object Create(object source)
            {
                var size = getSize(source);
                var list = new List<TDestination>(size);
                foreach (var value in (IEnumerable)source)
                {
                    list.Add((TDestination)converter(value));
                }

                return list;
            }
        }

        private sealed class UnknownSizeEnumerableToSameTypeListBuilder<TDestination> : IConverterBuilder
        {
            public object Create(object source)
            {
                var list = new List<TDestination>();
                foreach (var value in (IEnumerable)source)
                {
                    list.Add((TDestination)value);
                }

                return list;
            }
        }

        private sealed class UnknownSizeEnumerableToOtherTypeListBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public UnknownSizeEnumerableToOtherTypeListBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                var list = new List<TDestination>();
                foreach (var value in (IEnumerable)source)
                {
                    list.Add((TDestination)converter(value));
                }

                return list;
            }
        }
    }
}
