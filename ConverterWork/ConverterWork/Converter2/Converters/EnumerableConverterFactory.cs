namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class EnumerableConverterFactory : IConverterFactory
    {
        private static readonly Type OpenListType = typeof(List<>);
        private static readonly Type OpenHashSetType = typeof(HashSet<>);
        private static readonly Type OpenEnumerableType = typeof(IEnumerable<>);

        private static readonly Type ObjectEnumerableType = typeof(IEnumerable<object>);

        private static readonly Type OpenObjectEnumerableToArrayBuilderType = typeof(ObjectEnumerableToArrayBuilder<>);
        private static readonly Type OpenTypedEnumerableToListBuilderType = typeof(TypedEnumerableToListBuilder<>);
        private static readonly Type OpenObjectEnumerableToListBuilderType = typeof(ObjectEnumerableToListBuilder<>);
        private static readonly Type OpenEnumerableToListBuilderType = typeof(EnumerableToListBuilder<>);
        private static readonly Type OpenTypedEnumerableToSetBuilderType = typeof(TypedEnumerableToSetBuilder<>);
        private static readonly Type OpenObjectEnumerableToSetBuilderType = typeof(ObjectEnumerableToSetBuilder<>);
        private static readonly Type OpenEnumerableToSetBuilderType = typeof(EnumerableToSetBuilder<>);

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
                // T[] to T[]
                if (sourceType.IsArray && targetElementType.IsAssignableFrom(sourceElementType))
                {
                    // TODO copy?
                    return source => source;
                }

                var converter = context.CreateConverter(sourceElementType, targetElementType);
                if (converter == null)
                {
                    return null;
                }

                return CreateBuilderConverter(OpenObjectEnumerableToArrayBuilderType, targetElementType, converter);
            }

            // To List
            var listClosedTpe = OpenListType.MakeGenericType(targetElementType);
            if (listClosedTpe.IsAssignableFrom(targetType))
            {
                // From IE<T>
                var closedEnumerableType = OpenEnumerableType.MakeGenericType(targetElementType);
                if (closedEnumerableType.IsAssignableFrom(sourceType))
                {
                    return CreateBuilderConverter(OpenTypedEnumerableToListBuilderType, targetElementType, null);
                }

                var converter = context.CreateConverter(sourceElementType, targetElementType);
                if (converter == null)
                {
                    return null;
                }

                // From IE<object>
                if (ObjectEnumerableType.IsAssignableFrom(sourceType))
                {
                    return CreateBuilderConverter(OpenObjectEnumerableToListBuilderType, targetElementType, converter);
                }

                // From IE
                return CreateBuilderConverter(OpenEnumerableToListBuilderType, targetElementType, converter);
            }

            var setClosedType = OpenHashSetType.MakeGenericType(targetElementType);
            if (setClosedType.IsAssignableFrom(targetType))
            {
                // From IE<T>
                var closedEnumerableType = OpenEnumerableType.MakeGenericType(targetElementType);
                if (closedEnumerableType.IsAssignableFrom(sourceType))
                {
                    return CreateBuilderConverter(OpenTypedEnumerableToSetBuilderType, targetElementType, null);
                }

                var converter = context.CreateConverter(sourceElementType, targetElementType);
                if (converter == null)
                {
                    return null;
                }

                // From IE<object>
                if (ObjectEnumerableType.IsAssignableFrom(sourceType))
                {
                    return CreateBuilderConverter(OpenObjectEnumerableToSetBuilderType, targetElementType, converter);
                }

                // From IE
                return CreateBuilderConverter(OpenEnumerableToSetBuilderType, targetElementType, converter);
            }

            return null;
        }

        private static Func<object, object> CreateBuilderConverter(Type openBuilderType, Type elementType, Func<object, object> converter)
        {
            var builderType = openBuilderType.MakeGenericType(elementType);
            var builder = (IEnumerableBuilder)Activator.CreateInstance(builderType, converter);
            return builder.Create;
        }

        private interface IEnumerableBuilder
        {
            object Create(object source);
        }

        // ArrayBuilder

        // TODO array copy?

        private sealed class ObjectEnumerableToArrayBuilder<T> : IEnumerableBuilder
        {
            private readonly Func<object, object> converter;

            public ObjectEnumerableToArrayBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                // TODO tune new T[]
                return ((IEnumerable<object>)source).Select(x => (T)converter(x)).ToArray();
            }
        }

        // ListBuilder

        private sealed class TypedEnumerableToListBuilder<T> : IEnumerableBuilder
        {
            public object Create(object source)
            {
                return new List<T>((IEnumerable<T>)source);
            }
        }

        private sealed class ObjectEnumerableToListBuilder<T> : IEnumerableBuilder
        {
            private readonly Func<object, object> converter;

            public ObjectEnumerableToListBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                // TODO tune
                return new List<T>(((IEnumerable<object>)source).Select(x => (T)converter(x)));
            }
        }

        private sealed class EnumerableToListBuilder<T> : IEnumerableBuilder
        {
            private readonly Func<object, object> converter;

            public EnumerableToListBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                // TODO tune
                return new List<T>(((IEnumerable)source).Cast<object>().Select(x => (T)converter(x)));
            }
        }

        // Set

        private sealed class TypedEnumerableToSetBuilder<T> : IEnumerableBuilder
        {
            public object Create(object source)
            {
                return new HashSet<T>((IEnumerable<T>)source);
            }
        }

        private sealed class ObjectEnumerableToSetBuilder<T> : IEnumerableBuilder
        {
            private readonly Func<object, object> converter;

            public ObjectEnumerableToSetBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                // TODO tune
                return new HashSet<T>(((IEnumerable<object>)source).Select(x => (T)converter(x)));
            }
        }

        private sealed class EnumerableToSetBuilder<T> : IEnumerableBuilder
        {
            private readonly Func<object, object> converter;

            public EnumerableToSetBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                // TODO tune
                return new HashSet<T>(((IEnumerable)source).Cast<object>().Select(x => (T)converter(x)));
            }
        }
    }
}
