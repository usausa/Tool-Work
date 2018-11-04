namespace Smart.Converter2.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class EnumerableConverterFactory : IConverterFactory
    {
        private enum SourceEnumerableType
        {
            Nothing,
            Array,
            Enumerable,
            Collection,
            List
        }

        public interface IConverterBuilder
        {
            object Create(object source);
        }

        private interface IConverterBuilderProvider
        {
            Type GetBuilderType(SourceEnumerableType sourceEnumerableType);
        }

        private sealed class ProviderPair
        {
            public IConverterBuilderProvider SameTypeProvider { get; }

            public IConverterBuilderProvider OtherTypeProvider { get; }

            public ProviderPair(IConverterBuilderProvider sameTypeProvider, IConverterBuilderProvider otherTypeProvider)
            {
                SameTypeProvider = sameTypeProvider;
                OtherTypeProvider = otherTypeProvider;
            }
        }

        // TODO
        private static readonly Dictionary<Type, ProviderPair> Providers = new Dictionary<Type, ProviderPair>
        {
            { typeof(IEnumerable<>), new ProviderPair(SameTypeListBuilderProvider.Default, OtherTypeListBuilderProvider.Default) },
            { typeof(ICollection<>), new ProviderPair(SameTypeListBuilderProvider.Default, OtherTypeListBuilderProvider.Default) },
            { typeof(IList<>), new ProviderPair(SameTypeListBuilderProvider.Default, OtherTypeListBuilderProvider.Default) },
            { typeof(List<>), new ProviderPair(SameTypeListBuilderProvider.Default, OtherTypeListBuilderProvider.Default) },
        //    { typeof(ISet<>), typeof(HashSetCollectionProvider<>) },
        //    { typeof(HashSet<>), typeof(HashSetCollectionProvider<>) }
        };

        public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
        {
            // To Array
            if (targetType.IsArray)
            {
                var targetElementType = targetType.GetElementType();
                var sourceElementType = ResolveEnumerableType(sourceType, out var enumerableType);
                if (sourceElementType != null)
                {
                    if (sourceElementType == targetElementType)
                    {
                        // IE<T> to T[]
                        return ((IConverterBuilder)Activator.CreateInstance(
                            SameTypeArrayBuilderProvider.Default.GetBuilderType(enumerableType).MakeGenericType(targetElementType))).Create;
                    }

                    var converter = context.CreateConverter(sourceElementType, targetElementType);
                    if (converter != null)
                    {
                        // IE<T1> to T2[]
                        return ((IConverterBuilder)Activator.CreateInstance(
                            OtherTypeArrayBuilderProvider.Default.GetBuilderType(enumerableType).MakeGenericType(sourceElementType, targetElementType),
                            converter)).Create;
                    }
                }

                return null;
            }

            // To IE<T>
            if (targetType.IsGenericType &&
                Providers.TryGetValue(targetType.GetGenericTypeDefinition(), out var providerPair))
            {
                var targetElementType = targetType.GenericTypeArguments[0];
                var sourceElementType = ResolveEnumerableType(sourceType, out var enumerableType);
                if (sourceElementType != null)
                {
                    if (sourceElementType == targetElementType)
                    {
                        // IE<T> to IE<T>
                        return ((IConverterBuilder)Activator.CreateInstance(
                            providerPair.SameTypeProvider.GetBuilderType(enumerableType).MakeGenericType(targetElementType))).Create;
                    }

                    var converter = context.CreateConverter(sourceElementType, targetElementType);
                    if (converter != null)
                    {
                        // IE<T1> to IE<T2>
                        return ((IConverterBuilder)Activator.CreateInstance(
                            providerPair.OtherTypeProvider.GetBuilderType(enumerableType).MakeGenericType(sourceElementType, targetElementType),
                            converter)).Create;
                    }
                }

                return null;
            }

            return null;
        }

        //--------------------------------------------------------------------------------
        // Helper
        //--------------------------------------------------------------------------------

        private static Type ResolveEnumerableType(Type type, out SourceEnumerableType sourceEnumerableType)
        {
            if (type.IsArray)
            {
                sourceEnumerableType = SourceEnumerableType.Array;
                return type.GetElementType();
            }

            var interfaceType = type.GetInterfaces()
                .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>));
            if (interfaceType != null)
            {
                sourceEnumerableType = SourceEnumerableType.List;
                return interfaceType.GenericTypeArguments[0];
            }

            interfaceType = type.GetInterfaces()
                .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>));
            if (interfaceType != null)
            {
                sourceEnumerableType = SourceEnumerableType.Collection;
                return interfaceType.GenericTypeArguments[0];
            }

            interfaceType = type.GetInterfaces()
                .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (interfaceType != null)
            {
                sourceEnumerableType = SourceEnumerableType.Enumerable;
                return interfaceType.GenericTypeArguments[0];
            }

            sourceEnumerableType = SourceEnumerableType.Nothing;
            return null;
        }
    }
}
