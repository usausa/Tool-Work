namespace Smart.Converter2
{
    using System;

    public struct TypePair : IEquatable<TypePair>
    {
        private readonly int hashCode;

        public Type SourceType { get; }

        public Type TargetType { get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public TypePair(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;

            unchecked
            {
                hashCode = (sourceType.GetHashCode() * 379) ^ targetType.GetHashCode();
            }
        }

        public bool Equals(TypePair other)
        {
            return SourceType == other.SourceType && TargetType == other.TargetType;
        }

        public override bool Equals(object obj)
        {
            return obj is TypePair pair && SourceType == pair.SourceType && TargetType == pair.TargetType;
        }

        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}
