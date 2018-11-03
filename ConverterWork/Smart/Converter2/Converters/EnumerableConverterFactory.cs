namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.Collections.Generics;

    public sealed class EnumerableConverterFactory : IConverterFactory
    {
        // TODO
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
                var sourceEnumerableType = ResolveEnumerableType(sourceType, out var enumerableType);
                if (sourceEnumerableType != null)
                {
                    var sourceElementType = sourceEnumerableType.GenericTypeArguments[0];

                    if (!GetConverter(context, sourceElementType, targetElementType, out var converter))
                    {
                        return null;
                    }

                    if (converter != null)
                    {
                        switch (enumerableType)
                        {
                            case EnumerableType.List:
                                // IL<T1> to T2[]
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ListToOtherTypeArrayBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    converter)).Create;
                            case EnumerableType.Collection:
                                // IC<T1> to T2[]
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(CollectionToOtherTypeArrayBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    converter)).Create;
                            case EnumerableType.Enumerable:
                                // IE<T1> to T2[]
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(EnumerableToOtherTypeArrayBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    converter)).Create;
                        }
                    }
                    else
                    {
                        switch (enumerableType)
                        {
                            case EnumerableType.List:
                                // IL<T> to T[]
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ListToSameTypeArrayBuilder<>).MakeGenericType(targetElementType))).Create;
                            case EnumerableType.Collection:
                                // IC<T> to T[]
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(CollectionToSameTypeArrayBuilder<>).MakeGenericType(targetElementType))).Create;
                            case EnumerableType.Enumerable:
                                // IE<T> to T[]
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(EnumerableToSameTypeArrayBuilder<>).MakeGenericType(targetElementType))).Create;
                        }
                    }
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

                    var useConverter = converter != null;
                    var builderMethod = collectionProvider.ResolveArrayBuilderMethod(useConverter, out object factory);
                    if (useConverter)
                    {
                        // T1[] to IE<T2>
                        switch (builderMethod)
                        {
                            case ArrayBuilder.Constructor:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToOtherTypeCollectionByConstructorBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    factory,
                                    converter)).Create;
                            case ArrayBuilder.InitializeAdd:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToOtherTypeCollectionByInitializeAddBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    factory,
                                    converter)).Create;
                            case ArrayBuilder.Add:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToOtherTypeCollectionByAddBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    factory,
                                    converter)).Create;
                        }
                    }
                    else
                    {
                        // T[] to IE<T>
                        switch (builderMethod)
                        {
                            case ArrayBuilder.Constructor:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToSameTypeCollectionByConstructorBuilder<>).MakeGenericType(targetElementType),
                                    factory)).Create;
                            case ArrayBuilder.InitializeAdd:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToSameTypeCollectionByInitializeAddBuilder<>).MakeGenericType(targetElementType),
                                    factory)).Create;
                            case ArrayBuilder.Add:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ArrayToSameTypeCollectionByAddBuilder<>).MakeGenericType(targetElementType),
                                    factory)).Create;
                        }
                    }
                }

                // From IE<>
                var sourceEnumerableType = ResolveEnumerableType(sourceType, out var enumerableType);
                if (sourceEnumerableType != null)
                {
                    var sourceElementType = sourceEnumerableType.GenericTypeArguments[0];

                    if (!GetConverter(context, sourceElementType, targetElementType, out var converter))
                    {
                        return null;
                    }

                    var useConverter = converter != null;
                    var builderMethod = collectionProvider.ResolveBuilderMethod(enumerableType, useConverter, out object factory);
                    if (useConverter)
                    {
                        // IE<T1> to IE<T2>
                        switch (builderMethod)
                        {
                            case EnumerableBuilder.Constructor:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(EnumerableToOtherTypeCollectionByConstructorBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    factory,
                                    converter)).Create;
                            case EnumerableBuilder.ListInitializeAdd:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ListToOtherTypeCollectionByInitializeAddBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    factory,
                                    converter)).Create;
                            case EnumerableBuilder.CollectionInitializeAdd:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(CollectionToOtherTypeCollectionByInitializeAddBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    factory,
                                    converter)).Create;
                            case EnumerableBuilder.Add:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(EnumerableToOtherTypeCollectionByAddBuilder<,>).MakeGenericType(sourceElementType, targetElementType),
                                    factory,
                                    converter)).Create;
                        }
                    }
                    else
                    {
                        // IE<T> to IE<T>
                        switch (builderMethod)
                        {
                            case EnumerableBuilder.Constructor:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(EnumerableToSameTypeCollectionByConstructorBuilder<>).MakeGenericType(targetElementType),
                                    factory)).Create;
                            case EnumerableBuilder.ListInitializeAdd:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(ListToSameTypeCollectionByInitializeAddBuilder<>).MakeGenericType(targetElementType),
                                    factory)).Create;
                            case EnumerableBuilder.CollectionInitializeAdd:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(CollectionToSameTypeCollectionByInitializeAddBuilder<>).MakeGenericType(targetElementType),
                                    factory)).Create;
                            case EnumerableBuilder.Add:
                                return ((IConverterBuilder)Activator.CreateInstance(
                                    typeof(EnumerableToSameTypeCollectionByAddBuilder<>).MakeGenericType(targetElementType),
                                    factory)).Create;
                        }
                    }
                }

                return null;
            }

            return null;
        }

        //--------------------------------------------------------------------------------
        // Helper
        //--------------------------------------------------------------------------------

        private static Type ResolveEnumerableType(Type type, out EnumerableType enumerableType)
        {
            var interfaceType = type.GetInterfaces()
                .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>));
            if (interfaceType != null)
            {
                enumerableType = EnumerableType.List;
                return interfaceType;
            }

            interfaceType = type.GetInterfaces()
                .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>));
            if (interfaceType != null)
            {
                enumerableType = EnumerableType.Collection;
                return interfaceType;
            }

            interfaceType = type.GetInterfaces()
                .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (interfaceType != null)
            {
                enumerableType = EnumerableType.Enumerable;
                return interfaceType;
            }

            enumerableType = EnumerableType.Nothing;
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

        private sealed class ListToSameTypeArrayBuilder<TDestination> : IConverterBuilder
        {
            public object Create(object source)
            {
                var sourceList = (IList<TDestination>)source;
                var array = new TDestination[sourceList.Count];
                sourceList.CopyTo(array, 0);

                return array;
            }
        }

        private sealed class CollectionToSameTypeArrayBuilder<TDestination> : IConverterBuilder
        {
            public object Create(object source)
            {
                var sourceCollection = (ICollection<TDestination>)source;
                var array = new TDestination[sourceCollection.Count];
                var index = 0;
                foreach (var value in sourceCollection)
                {
                    array[index] = value;
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

        private sealed class ListToOtherTypeArrayBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public ListToOtherTypeArrayBuilder(Func<object, object> converter)
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

        private sealed class CollectionToOtherTypeArrayBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public CollectionToOtherTypeArrayBuilder(Func<object, object> converter)
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

        private sealed class EnumerableToOtherTypeArrayBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<object, object> converter;

            public EnumerableToOtherTypeArrayBuilder(Func<object, object> converter)
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

        //--------------------------------------------------------------------------------
        // Builder to Collection from Array
        //--------------------------------------------------------------------------------

        private sealed class ArrayToSameTypeCollectionByConstructorBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<IEnumerable<TDestination>, ICollection<TDestination>> factory;

            public ArrayToSameTypeCollectionByConstructorBuilder(object factory)
            {
                this.factory = (Func<IEnumerable<TDestination>, ICollection<TDestination>>)factory;
            }

            public object Create(object source)
            {
                return factory((TDestination[])source);
            }
        }

        private sealed class ArrayToSameTypeCollectionByInitializeAddBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<int, ICollection<TDestination>> factory;

            public ArrayToSameTypeCollectionByInitializeAddBuilder(object factory)
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

        private sealed class ArrayToSameTypeCollectionByAddBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<ICollection<TDestination>> factory;

            public ArrayToSameTypeCollectionByAddBuilder(object factory)
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

        private sealed class ArrayToOtherTypeCollectionByConstructorBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<IEnumerable<TDestination>, ICollection<TDestination>> factory;

            private readonly Func<object, object> converter;

            public ArrayToOtherTypeCollectionByConstructorBuilder(object factory, Func<object, object> converter)
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

                public IEnumerator<TDestination> GetEnumerator() => new ArrayConvertEnumerator(source, converter);

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

        private sealed class ArrayToOtherTypeCollectionByInitializeAddBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<int, ICollection<TDestination>> factory;

            private readonly Func<object, object> converter;

            public ArrayToOtherTypeCollectionByInitializeAddBuilder(object factory, Func<object, object> converter)
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

        private sealed class ArrayToOtherTypeCollectionByAddBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<ICollection<TDestination>> factory;

            private readonly Func<object, object> converter;

            public ArrayToOtherTypeCollectionByAddBuilder(object factory, Func<object, object> converter)
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

        private sealed class EnumerableToSameTypeCollectionByConstructorBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<IEnumerable<TDestination>, ICollection<TDestination>> factory;

            public EnumerableToSameTypeCollectionByConstructorBuilder(object factory)
            {
                this.factory = (Func<IEnumerable<TDestination>, ICollection<TDestination>>)factory;
            }

            public object Create(object source)
            {
                return factory((IEnumerable<TDestination>)source);
            }
        }

        private sealed class ListToSameTypeCollectionByInitializeAddBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<int, ICollection<TDestination>> factory;

            public ListToSameTypeCollectionByInitializeAddBuilder(object factory)
            {
                this.factory = (Func<int, ICollection<TDestination>>)factory;
            }

            public object Create(object source)
            {
                var listSource = (IList<TDestination>)source;
                var collection = factory(listSource.Count);
                for (var i = 0; i < listSource.Count; i++)
                {
                    collection.Add(listSource[i]);
                }

                return collection;
            }
        }

        private sealed class CollectionToSameTypeCollectionByInitializeAddBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<int, ICollection<TDestination>> factory;

            public CollectionToSameTypeCollectionByInitializeAddBuilder(object factory)
            {
                this.factory = (Func<int, ICollection<TDestination>>)factory;
            }

            public object Create(object source)
            {
                var collectionSource = (ICollection<TDestination>)source;
                var collection = factory(collectionSource.Count);
                foreach (var value in collectionSource)
                {
                    collection.Add(value);
                }

                return collection;
            }
        }

        private sealed class EnumerableToSameTypeCollectionByAddBuilder<TDestination> : IConverterBuilder
        {
            private readonly Func<ICollection<TDestination>> factory;

            public EnumerableToSameTypeCollectionByAddBuilder(object factory)
            {
                this.factory = (Func<ICollection<TDestination>>)factory;
            }

            public object Create(object source)
            {
                var collection = factory();
                foreach (var value in (IEnumerable<TDestination>)source)
                {
                    collection.Add(value);
                }

                return collection;
            }
        }

        private sealed class EnumerableToOtherTypeCollectionByConstructorBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<IEnumerable<TDestination>, ICollection<TDestination>> factory;

            public EnumerableToOtherTypeCollectionByConstructorBuilder(object factory)
            {
                this.factory = (Func<IEnumerable<TDestination>, ICollection<TDestination>>)factory;
            }

            public object Create(object source)
            {
                throw new NotImplementedException();
            }
        }

        private sealed class ListToOtherTypeCollectionByInitializeAddBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<int, ICollection<TDestination>> factory;

            private readonly Func<object, object> converter;

            public ListToOtherTypeCollectionByInitializeAddBuilder(object factory, Func<object, object> converter)
            {
                this.factory = (Func<int, ICollection<TDestination>>)factory;
                this.converter = converter;
            }

            public object Create(object source)
            {
                var listSource = (IList<TSource>)source;
                var collection = factory(listSource.Count);
                for (var i = 0; i < listSource.Count; i++)
                {
                    collection.Add((TDestination)converter(listSource[i]));
                }

                return collection;
            }
        }

        private sealed class CollectionToOtherTypeCollectionByInitializeAddBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<int, ICollection<TDestination>> factory;

            private readonly Func<object, object> converter;

            public CollectionToOtherTypeCollectionByInitializeAddBuilder(object factory, Func<object, object> converter)
            {
                this.factory = (Func<int, ICollection<TDestination>>)factory;
                this.converter = converter;
            }

            public object Create(object source)
            {
                var collectionSource = (ICollection<TSource>)source;
                var collection = factory(collectionSource.Count);
                foreach (var value in collectionSource)
                {
                    collection.Add((TDestination)converter(value));
                }

                return collection;
            }
        }

        private sealed class EnumerableToOtherTypeCollectionByAddBuilder<TSource, TDestination> : IConverterBuilder
        {
            private readonly Func<ICollection<TDestination>> factory;

            private readonly Func<object, object> converter;

            public EnumerableToOtherTypeCollectionByAddBuilder(object factory, Func<object, object> converter)
            {
                this.factory = (Func<ICollection<TDestination>>)factory;
                this.converter = converter;
            }

            public object Create(object source)
            {
                var collection = factory();
                foreach (var value in (IEnumerable<TSource>)source)
                {
                    collection.Add((TDestination)converter(value));
                }

                return collection;
            }
        }

        //--------------------------------------------------------------------------------
        // Collection provider
        //--------------------------------------------------------------------------------

        private enum ArrayBuilder
        {
            Constructor,
            InitializeAdd,
            Add
        }

        private enum EnumerableType
        {
            Nothing,
            Enumerable,
            Collection,
            List
        }

        private enum EnumerableBuilder
        {
            Constructor,
            CollectionInitializeAdd,
            ListInitializeAdd,
            Add
        }

        private interface ICollectionProvider
        {
            ArrayBuilder ResolveArrayBuilderMethod(bool withConvert, out object factory);

            EnumerableBuilder ResolveBuilderMethod(EnumerableType sourceType, bool withConvert, out object factory);
        }

        private class ListCollectionProvider<T> : ICollectionProvider
        {
            public ArrayBuilder ResolveArrayBuilderMethod(bool withConvert, out object factory)
            {
                factory = (Func<int, ICollection<T>>)(x => new List<T>(x));
                return ArrayBuilder.InitializeAdd;
            }

            public EnumerableBuilder ResolveBuilderMethod(EnumerableType sourceType, bool withConvert, out object factory)
            {
                switch (sourceType)
                {
                    case EnumerableType.Collection:
                        factory = (Func<int, ICollection<T>>)(x => new List<T>(x));
                        return EnumerableBuilder.CollectionInitializeAdd;
                    case EnumerableType.List:
                        factory = (Func<int, ICollection<T>>)(x => new List<T>(x));
                        return EnumerableBuilder.ListInitializeAdd;
                    default:
                        factory = (Func<IEnumerable<T>, ICollection<T>>)(x => new List<T>(x));
                        return EnumerableBuilder.Constructor;
                }
            }
        }

        // TODO
        //private class HashSetCollectionProvider<T> : ICollectionProvider
        //{
        //    public object CreateDefaultFactory() => (Func<ICollection<T>>)(() => new HashSet<T>());

        //    // .NET Standard is not support capacity argument constructor
        //    public object CreateCapacityFactory() => null;  // (Func<int, ICollection<T>>)(x => new HashSet<T>(x));

        //    public object CreateEnumerableFactory() => (Func<IEnumerable<T>, ICollection<T>>)(x => new HashSet<T>(x));
        //}
    }
}
