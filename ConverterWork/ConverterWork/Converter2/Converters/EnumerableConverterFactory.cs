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

        private static readonly Type OpenArrayToArrayBuilderType = typeof(ArrayToArrayBuilder<,>);
        private static readonly Type OpenObjectEnumerableToArrayBuilderType = typeof(ObjectEnumerableToArrayBuilder<>);
        private static readonly Type OpenEnumerableToArrayBuilderType = typeof(EnumerableToArrayBuilder<>);

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
                if (GetConverter(context, sourceElementType, targetElementType, out var converter))
                {
                    return null;
                }

                // TODO sourceElementType targetElementType Nullableも考慮した上で同一ならconverter不要

                // T1[] to T2[]
                if (sourceType.IsArray)
                {
                    return CreateBuilderConverter(OpenArrayToArrayBuilderType, converter, sourceElementType, targetElementType);
                }

                // From IE<object>
                if (ObjectEnumerableType.IsAssignableFrom(sourceType))
                {
                    return CreateBuilderConverter(OpenObjectEnumerableToArrayBuilderType, converter, targetElementType);
                }

                // From IE
                return CreateBuilderConverter(OpenEnumerableToArrayBuilderType, converter, targetElementType);
            }

            // To List
            var listClosedTpe = OpenListType.MakeGenericType(targetElementType);
            if (listClosedTpe.IsAssignableFrom(targetType))
            {
                // TODO 順番
                // From IE<T>
                var closedEnumerableType = OpenEnumerableType.MakeGenericType(targetElementType);
                if (closedEnumerableType.IsAssignableFrom(sourceType))
                {
                    return CreateBuilderConverter(OpenTypedEnumerableToListBuilderType, null, targetElementType);
                }

                if (GetConverter(context, sourceElementType, targetElementType, out var converter))
                {
                    return null;
                }

                // TODO sourceElementType targetElementType Nullableも考慮した上で同一ならconverter不要

                // From IE<object>
                if (ObjectEnumerableType.IsAssignableFrom(sourceType))
                {
                    return CreateBuilderConverter(OpenObjectEnumerableToListBuilderType, converter, targetElementType);
                }

                // From IE
                return CreateBuilderConverter(OpenEnumerableToListBuilderType, converter, targetElementType);
            }

            var setClosedType = OpenHashSetType.MakeGenericType(targetElementType);
            if (setClosedType.IsAssignableFrom(targetType))
            {
                // TODO 順番
                // From IE<T>
                var closedEnumerableType = OpenEnumerableType.MakeGenericType(targetElementType);
                if (closedEnumerableType.IsAssignableFrom(sourceType))
                {
                    return CreateBuilderConverter(OpenTypedEnumerableToSetBuilderType, null, targetElementType);
                }

                // TODO sourceElementType targetElementType Nullableも考慮した上で同一ならconverter不要

                var converter = context.CreateConverter(sourceElementType, targetElementType);
                if (converter == null)
                {
                    return null;
                }

                // From IE<object>
                if (ObjectEnumerableType.IsAssignableFrom(sourceType))
                {
                    return CreateBuilderConverter(OpenObjectEnumerableToSetBuilderType, converter, targetElementType);
                }

                // From IE
                return CreateBuilderConverter(OpenEnumerableToSetBuilderType, converter, targetElementType);
            }

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

        private static Func<object, object> CreateBuilderConverter(Type openBuilderType, Func<object, object> converter, params Type[] types)
        {
            var builderType = openBuilderType.MakeGenericType(types);
            var builder = (IEnumerableBuilder)Activator.CreateInstance(builderType, converter);
            return builder.Create;
        }

        // TODO ランタイムではなく、生成時に元の型も決まるな… Funcで渡すか！
        //private static int? Count<T>(object source)
        //{
        //    if (source is IList l)
        //    {
        //        return l.Count;
        //    }

        //    if (source is ICollection<T> c)
        //    {
        //        return c.Count;
        //    }

        //    if (source is IReadOnlyCollection<T> roc)
        //    {
        //        return roc.Count;
        //    }

        //    return null;
        //}

        private interface IEnumerableBuilder
        {
            object Create(object source);
        }

        // ArrayBuilder

        private sealed class ArrayToArrayBuilder<TSource, TDestination> : IEnumerableBuilder
        {
            private readonly Func<object, object> converter;

            public ArrayToArrayBuilder(Func<object, object> converter)
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

        private sealed class ObjectEnumerableToArrayBuilder<T> : IEnumerableBuilder
        {
            private readonly Func<object, object> converter;

            public ObjectEnumerableToArrayBuilder(Func<object, object> converter)
            {
                this.converter = converter;
            }

            public object Create(object source)
            {
                // TODO tune count
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
                // TODO tune count IList?
                return ((IEnumerable)source).Cast<object>().Select(x => (T)converter(x)).ToArray();
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
