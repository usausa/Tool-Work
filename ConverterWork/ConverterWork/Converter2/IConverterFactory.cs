namespace Smart.Converter2
{
    using System;

    public interface IConverterFactory
    {
        Func<TypePair, object, object> GetConverter(TypePair typePair);
    }
}
