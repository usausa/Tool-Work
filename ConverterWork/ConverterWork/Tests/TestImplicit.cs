namespace Smart.Tests
{
    public struct TestImplicit
    {
        public int Value { get; set; }

        public static implicit operator int(TestImplicit value)
        {
            return value.Value;
        }

        public static implicit operator TestImplicit(int value)
        {
            return new TestImplicit { Value = value };
        }
    }
}
