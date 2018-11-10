namespace Smart.Converter2.Converters
{
    using System;

    internal interface IConverterBuilder
    {
        Func<object, object> Build();
    }
}
