namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultNumber = 5;
        public string Value { get; }

        private OrderName(string value) => this.Value = value;

        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultNumber);

            return new OrderName(value);
        }

    }
}
