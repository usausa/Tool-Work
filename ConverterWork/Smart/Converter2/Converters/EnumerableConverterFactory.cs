namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.Collections.Generics;

    public sealed class EnumerableConverterFactory : IConverterFactory
    {
        private enum SourceType
        {
            Array,
            Collection,
            Enumerable
        }

        private enum FactoryType
        {
            Default,
            Capacity,
            Enumerable
        }

        private interface ICollectionProvider
        {
            FactoryType ResolveFactoryType(SourceType sourceType, bool withConvert, out object factory);
        }

        private class ListCollectionProvider<T> : ICollectionProvider
        {
            public FactoryType ResolveFactoryType(SourceType sourceType, bool withConvert, out object factory)
            {
                switch (sourceType)
                {
                    case SourceType.Array:
                        factory = (Func<IEnumerable<T>, ICollection<T>>)(x => new List<T>(x));
                        return FactoryType.Enumerable;
                    case SourceType.Collection:
                        factory = (Func<int, ICollection<T>>)(x => new List<T>(x));
                        return FactoryType.Capacity;
                    default:
                        factory = (Func<ICollection<T>>)(() => new List<T>());
                        return FactoryType.Default;
                }
            }
        }

        //private class HashSetCollectionProvider<T> : ICollectionProvider
        //{
        //    public object CreateDefaultFactory() => (Func<ICollection<T>>)(() => new HashSet<T>());

        //    // .NET Standard is not support capacity argument constructor
        //    public object CreateCapacityFactory() => null;  // (Func<int, ICollection<T>>)(x => new HashSet<T>(x));

        //    public object CreateEnumerableFactory() => (Func<IEnumerable<T>, ICollection<T>>)(x => new HashSet<T>(x));
        //}

        private static readonly Dictionary<Type, Type> OpenTypeProviderTypes = new Dictionary<Type, Type>
        {
            { typeof(List<>), typeof(ListCollectionProvider<>) },
            //{ typeof(HashSet<>), typeof(HashSetCollectionProvider<>) },
            { typeof(IEnumerable<>), typeof(ListCollectionProvider<>) },
            { typeof(ICollection<>), typeof(ListCollectionProvider<>) },
            { typeof(IList<>), typeof(ListCollectionProvider<>) },
            //{ typeof(ISet<>), typeof(HashSetCollectionProvider<>) },
        };

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            // To Array
            if (targetType.IsArray)
            {
                var targetElementType = targetType.GetElementType();

                // From Array
                if (sourceType.IsArray)
                {
                    var sourceElementType = sourceType.GetElementType();

                    if (!GetConverter(context, sourceElementType, targetElementType, out var converter))
                    {
                        return null;
                    }

                    // T1[] to T2[]
                    return ((IConverterBuilder)Activator.CreateInstance(
                        typeof(ArrayToOtherTypeArrayConverter<,>).MakeGenericType(sourceElementType, targetElementType),
                        converter)).Create;
                }

                // From IE<>
                var sourceEnumerableType = GetGenericEnumerableType(sourceType);
                if (sourceEnumerableType != null)
                {
                    var sourceElementType = sourceEnumerableType.GenericTypeArguments[0];

                    if (!GetConverter(context, sourceElementType, targetElementType, out var converter))
                    {
                        return null;
                    }

                    var getSize = ResolveSizeFunction(sourceType, sourceElementType);
                    if (getSize != null)
                    {
                        if (converter == null)
                        {
                            // IC<T> to T[]
                            return ((IConverterBuilder)Activator.CreateInstance(
                                typeof(CollectionToSameTypeArrayBuilder<>).MakeGenericType(targetElementType),
                                getSize)).Create;
                        }

                        // IC<T1> to T2[]
                        return ((IConverterBuilder)Activator.CreateInstance(
                            typeof(CollectionToOtherTypeArrayBuilder<>).MakeGenericType(targetElementType),
                            getSize,
                            converter)).Create;
                    }

                    if (converter == null)
                    {
                        // IE<T> to T[]
                        return ((IConverterBuilder)Activator.CreateInstance(
                            typeof(EnumerableToSameTypeArrayBuilder<>).MakeGenericType(targetElementType))).Create;
                    }

                    // IE<T1> to T2[]
                    return ((IConverterBuilder)Activator.CreateInstance(
                        typeof(EnumerableToOtherTypeArrayBuilder<>).MakeGenericType(targetElementType),
                        converter)).Create;
                }

                return null;
            }

            // To IE<T>
            if (targetType.IsGenericType &&
                OpenTypeProviderTypes.TryGetValue(targetType.GetGenericTypeDefinition(), out var collectionProviderType))
            {
                var targetElementType = targetType.GenericTypeArguments[0];
                var collectionProvider = (ICollectionProvider)Activator.CreateInstance(collectionProviderType.MakeGenericType(targetElementType));

                // From Array
                if (sourceType.IsArray)
                {
                    var sourceElementType = sourceType.GetElementType();

                    if (!GetConverter(context, sourceElementType, targetElementType, out var converter))
                    {
                        return null;
                    }

                    var factoryType = collectionProvider.ResolveFactoryType(SourceType.Array, converter != null, out var factory);

                    if (converter == null)
                    {
                        // T[] to IE<T>
                        switch (factoryType)
                        {
                            case FactoryType.Enumerable:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToSameTypeCollectionByEnumerableBuilder<>).MakeGenericType(targetElementType),
                                    factory)).Create;
                            case FactoryType.Capacity:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToSameTypeCollectionByCapacityBuilder<>).MakeGenericType(targetElementType),
                                    factory)).Create;
                            default:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToSameTypeCollectionByDefaultBuilder<>).MakeGenericType(targetElementType),
                                    factory)).Create;
                        }
                    }
                    else
                    {
                        // T1[] to IE<T2>
                        switch (factoryType)
                        {
                            case FactoryType.Enumerable:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToOtherTypeCollectionByEnumerableBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    factory,
                                    converter)).Create;
                            case FactoryType.Capacity:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToOtherTypeCollectionByCapacityBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    factory,
                                    converter)).Create;
                            default:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToOtherTypeCollectionByDefaultBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    factory,
                                    converter)).Create;
                        }
                    }
                }

                // From IE<>
                var sourceEnumerableType = GetGenericEnumerableType(sourceType);
                if (sourceEnumerableType != null)
                {
                    var sourceElementType = sourceEnumerableType.GenericTypeArguments[0];

                    if (!GetConverter(context, sourceElementType, targetElementType, out var converter))
                    {
                        return null;
                    }

                    var getSize = ResolveSizeFunction(sourceType, sourceElementType);

                    // TODO collection factory

                    //if ((converter == null) && (getSize != null))
                    //{
                    //     TODO
                    //    return ((IConverterBuilder)Activator.CreateInstance(
                    //        typeof(CollectionToSameTypeArrayBuilder<>).MakeGenericType(targetElementType),
                    //        getSize)).Create;
                    //}

                    //if (getSize != null)
                    //{
                    //     TODO
                    //    return ((IConverterBuilder)Activator.CreateInstance(
                    //        typeof(CollectionToOtherTypeArrayBuilder<>).MakeGenericType(targetElementType),
                    //        getSize,
                    //        converter)).Create;
                    //}

                    //if (converter == null)
                    //{
                    //     TODO
                    //    return ((IConverterBuilder)Activator.CreateInstance(
                    //        typeof(EnumerableToSameTypeArrayBuilder<>).MakeGenericType(targetElementType))).Create;
                    //}

                    // TODO
                    //return ((IConverterBuilder)Activator.CreateInstance(
                    //    typeof(EnumerableToOtherTypeArrayBuilder<>).MakeGenericType(targetElementType),
                    //    converter)).Create;
                }

                return null;
            }

            return null;
        }

        //--------------------------------------------------------------------------------
        // Helper
        //--------------------------------------------------------------------------------

        private static Type GetGenericEnumerableType(Type type)
        {
            return type.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
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
        // Builder to Array from Array
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

        //--------------------------------------------------------------------------------
        // Builder to Array from Collection
        //--------------------------------------------------------------------------------

        private sealed class CollectionToSameTypeArrayBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<object, int> getSize;

            public CollectionToSameTypeArrayBuilder(Func<object, int> getSize)
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

        private sealed class CollectionToOtherTypeArrayBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<object, int> getSize;

            private readonly Func<object, object> converter;

            public CollectionToOtherTypeArrayBuilder(Func<object, int> getSize, Func<object, object> converter)
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

        private sealed class EnumerableToSameTypeArrayBuilder<TDestination> : IConverterBuilder
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

        private sealed class EnumerableToOtherTypeArrayBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public EnumerableToOtherTypeArrayBuilder(Func<object, object> converter)
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
        // Builder to Collection from Array
        //--------------------------------------------------------------------------------

        private sealed class ArrayToSameTypeCollectionByEnumerableBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<IEnumerable<TDestination>, ICollection<TDestination>> factory;

            public ArrayToSameTypeCollectionByEnumerableBuilder(object factory)
            {
                this.factory = (Func<IEnumerable<TDestination>, ICollection<TDestination>>)factory;
            }

            public object Create(object source)
            {
                return factory((TDestination[])source);
            }
        }

        private sealed class ArrayToSameTypeCollectionByCapacityBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<int, ICollection<TDestination>> factory;

            public ArrayToSameTypeCollectionByCapacityBuilder(object factory)
            {
                this.factory = (Func<int, ICollection<TDestination>>)factory;
            }

            public object Create(object source)
            {
                var sourceArray = (TDestination[])source;
                var collection = factory(sourceArray.Length);
                for (var i = 0; i < sourceArray.Length; i++)
                {
                    collection.Add(sourceArray[i]);
                }

                return collection;
            }
        }

        private sealed class ArrayToSameTypeCollectionByDefaultBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<ICollection<TDestination>> factory;

            public ArrayToSameTypeCollectionByDefaultBuilder(object factory)
            {
                this.factory = (Func<ICollection<TDestination>>)factory;
            }

            public object Create(object source)
            {
                var sourceArray = (TDestination[])source;
                var collection = factory();
                for (var i = 0; i < sourceArray.Length; i++)
                {
                    collection.Add(sourceArray[i]);
                }

                return collection;
            }
        }

        private sealed class ArrayToOtherTypeCollectionByEnumerableBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<IEnumerable<TDestination>, ICollection<TDestination>> factory;

            private readonly Func<object, object> converter;

            public ArrayToOtherTypeCollectionByEnumerableBuilder(object factory, Func<object, object> converter)
            {
                this.factory = (Func<IEnumerable<TDestination>, ICollection<TDestination>>)factory;
                this.converter = converter;
            }

            public object Create(object source)
            {
                var sourceArray = (TSource[])source;
                return factory(new ArrayConvertCollection(sourceArray, converter));
            }

            private struct ArrayConvertEnumerator : IEnumerator<TDestination>
            {
                private readonly TSource[] source;

                private readonly Func<object, object> converter;

                private int index;

                public ArrayConvertEnumerator(TSource[] source, Func<object, object> converter)
                {
                    this.source = source;
                    this.converter = converter;
                    index = -1;
                }

                public bool MoveNext()
                {
                    index++;
                    return index < source.Length;
                }

                public void Reset()
                {
                    index = -1;
                }

                public TDestination Current => (TDestination)converter(source[index]);

                object IEnumerator.Current => Current;

                public void Dispose()
                {
                }
            }

            private readonly struct ArrayConvertCollection : ICollection<TDestination>
            {
                private readonly TSource[] source;

                private readonly Func<object, object> converter;

                public ArrayConvertCollection(TSource[] source, Func<object, object> converter)
                {
                    this.source = source;
                    this.converter = converter;
                }

                public IEnumerator<TDestination> GetEnumerator()
                {
                    return new ArrayConvertEnumerator(source, converter);
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }

                public void Add(TDestination item) => throw new NotSupportedException();

                public void Clear() => throw new NotSupportedException();

                public bool Contains(TDestination item) => throw new NotSupportedException();

                public void CopyTo(TDestination[] array, int arrayIndex)
                {
                    for (var i = 0; i < source.Length; i++)
                    {
                        array[arrayIndex + i] = (TDestination)converter(source[i]);
                    }
                }

                public bool Remove(TDestination item) => throw new NotSupportedException();

                public int Count => source.Length;

                public bool IsReadOnly => true;
            }
        }

        private sealed class ArrayToOtherTypeCollectionByCapacityBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<int, ICollection<TDestination>> factory;

            private readonly Func<object, object> converter;

            public ArrayToOtherTypeCollectionByCapacityBuilder(object factory, Func<object, object> converter)
            {
                this.factory = (Func<int, ICollection<TDestination>>)factory;
                this.converter = converter;
            }

            public object Create(object source)
            {
                var sourceArray = (TSource[])source;
                var collection = factory(sourceArray.Length);
                for (var i = 0; i < sourceArray.Length; i++)
                {
                    collection.Add((TDestination)converter(sourceArray[i]));
                }

                return collection;
            }
        }

        private sealed class ArrayToOtherTypeCollectionByDefaultBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<ICollection<TDestination>> factory;

            private readonly Func<object, object> converter;

            public ArrayToOtherTypeCollectionByDefaultBuilder(object factory, Func<object, object> converter)
            {
                this.factory = (Func<ICollection<TDestination>>)factory;
                this.converter = converter;
            }

            public object Create(object source)
            {
                var sourceArray = (TSource[])source;
                var collection = factory();
                for (var i = 0; i < sourceArray.Length; i++)
                {
                    collection.Add((TDestination)converter(sourceArray[i]));
                }

                return collection;
            }
        }

        //--------------------------------------------------------------------------------
        // Builder to Collection from Enumerable
        //--------------------------------------------------------------------------------

        // CollectionToSameTypeCollectionByCapacityBuilder
        // CollectionToSameTypeCollectionByCapacityBuilder

        // TODO size, convert, factory = 8(12?) AddAll?

        // convert size  : AddAll?
        //
        //
        //
        //

        //private sealed class KnownSizeEnumerableToSameTypeListBuilder<TDestination> : IConverterBuilder
        //{
        //    private readonly Func<object, int> getSize;

        //    public KnownSizeEnumerableToSameTypeListBuilder(Func<object, int> getSize)
        //    {
        //        this.getSize = getSize;
        //    }

        //    public object Create(object source)
        //    {
        //        var size = getSize(source);
        //        var list = new List<TDestination>(size);
        //        foreach (var value in (IEnumerable)source)
        //        {
        //            list.Add((TDestination)value);
        //        }

        //        return list;
        //    }
        //}

        //private sealed class KnownSizeEnumerableToOtherTypeListBuilder<TDestination> : IConverterBuilder
        //{
        //    private readonly Func<object, int> getSize;

        //    private readonly Func<object, object> converter;

        //    public KnownSizeEnumerableToOtherTypeListBuilder(Func<object, int> getSize, Func<object, object> converter)
        //    {
        //        this.getSize = getSize;
        //        this.converter = converter;
        //    }

        //    public object Create(object source)
        //    {
        //        var size = getSize(source);
        //        var list = new List<TDestination>(size);
        //        foreach (var value in (IEnumerable)source)
        //        {
        //            list.Add((TDestination)converter(value));
        //        }

        //        return list;
        //    }
        //}

        //private sealed class UnknownSizeEnumerableToSameTypeListBuilder<TDestination> : IConverterBuilder
        //{
        //    public object Create(object source)
        //    {
        //        var list = new List<TDestination>();
        //        foreach (var value in (IEnumerable)source)
        //        {
        //            list.Add((TDestination)value);
        //        }

        //        return list;
        //    }
        //}

        //private sealed class UnknownSizeEnumerableToOtherTypeListBuilder<TDestination> : IConverterBuilder
        //{
        //    private readonly Func<object, object> converter;

        //    public UnknownSizeEnumerableToOtherTypeListBuilder(Func<object, object> converter)
        //    {
        //        this.converter = converter;
        //    }

        //    public object Create(object source)
        //    {
        //        var list = new List<TDestination>();
        //        foreach (var value in (IEnumerable)source)
        //        {
        //            list.Add((TDestination)converter(value));
        //        }

        //        return list;
        //    }
        //}

        //--------------------------------------------------------------------------------
        // Collection provider
        //--------------------------------------------------------------------------------
    }
}
