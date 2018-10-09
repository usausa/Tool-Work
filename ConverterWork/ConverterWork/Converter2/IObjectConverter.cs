namespace Smart.Converter2
{
    using System;

    public interface IObjectConverter
    {
        Func<TSource, TTarget> GetConverter<TSource, TTarget>();

        Func<object, object> GetConverter(Type sourceType, Type targetType);

        T Convert<T>(object value);

        object Convert(object value, Type targetType);
    }
}
