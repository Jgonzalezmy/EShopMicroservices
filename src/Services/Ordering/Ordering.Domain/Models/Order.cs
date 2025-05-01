namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _ordenItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _ordenItems.AsReadOnly();

        public CustomerId CustomerId { get; private set; } = default!;
        public OrderName OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = default!;
        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(OrderId id, CustomerId customer, OrderName orderName, Address shippingAdrees, Address billingAddress, Payment payment)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customer,
                OrderName = orderName,
                ShippingAddress = shippingAdrees,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };

            order.AddDomainEvents(new OrderCreateEvent(order));

            return order;
        }

        public void Update(OrderName orderName, Address shippingAdrees, Address billingAddress, Payment payment, OrderStatus status)
        {

            OrderName = orderName;
            ShippingAddress = shippingAdrees;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;

            AddDomainEvents(new OrderUpdateEvent(this));

        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id, productId, quantity, price);

            _ordenItems.Add(orderItem);
        }

        public void Remove(ProductId productId)
        {
            var orderItem = _ordenItems.FirstOrDefault(o => o.ProductId == productId);

            if (orderItem is not null)
                _ordenItems.Remove(orderItem);
        }
    }
}
