namespace Smart.Tests
{
    public struct TestExplicit
    {
        public int Value { get; set; }

        public static explicit operator int(TestExplicit value)
        {
            return value.Value;
        }

        public static explicit operator TestExplicit(int value)
        {
            return new TestExplicit { Value = value };
        }
    }
}
