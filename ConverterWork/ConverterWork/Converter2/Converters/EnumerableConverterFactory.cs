namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class EnumerableConverterFactory : IConverterFactory
    {
        private static readonly Type ObjectEnumerableType = typeof(IEnumerable<object>);

        private static readonly Type EnumerableType = typeof(IEnumerable);

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            // TODO 他と統合して効率よく
            var sourceElementType = sourceType.GetCollectionElementType();
            var targetElementType = targetType.GetCollectionElementType();
            if ((sourceElementType == null) || (targetElementType == null))
            {
                return null;
            }

            // To Array
            if (targetType.IsArray)
            {
                // TODO Assignable
                // T[] to T[]
                if (sourceType.IsArray && (targetElementType == sourceElementType))
                {
                    // TODO コピーを作る？
                    return source => source;
                }

                // TODO Array canConvert特殊か？

                if (ObjectEnumerableType.IsAssignableFrom(sourceType))
                {
                    //return source => ((IEnumerable<object>))
                }
            }

            // TODO Listが代入可能

            // TODO Setが代入可能

            // TODO
            return null;
        }

        private interface IEnumerableBuilder
        {
            object Create(object source);
        }

        private sealed class ObjectEnumerableToArrayBuilder<T> : IEnumerableBuilder
        {
            private readonly Func<object, object> converter;

            public ObjectEnumerableToArrayBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return ((IEnumerable<object>)source).Select(x => (T)converter(x)).ToArray();
            }
        }

        private sealed class EnumerableToArrayBuilder<T> : IEnumerableBuilder
        {
            private readonly Func<object, object> converter;

            public EnumerableToArrayBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return ((IEnumerable)source).Cast<object>().Select(x => (T)converter(x)).ToArray();
            }
        }

        private sealed class TypedEnumerableToListBuilder<T> : IEnumerableBuilder
        {
            public object Create(object source)
            {
                return new List<T>(((IEnumerable<T>)source));
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
                return new HashSet<T>(((IEnumerable)source).Cast<object>().Select(x => (T)converter(x)));
            }
        }

        private sealed class TypedEnumerableToSetBuilder<T> : IEnumerableBuilder
        {
            private readonly Func<object, object> converter;

            public TypedEnumerableToSetBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                return new HashSet<T>(((IEnumerable<T>)source));
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
                return new HashSet<T>(((IEnumerable)source).Cast<object>().Select(x => (T)converter(x)));
            }
        }
    }
}
