namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers => new List<Customer>
        {
            Customer.Create(CustomerId.Of(Guid.NewGuid()), "juan", "jgonzalezmy@gmail.com"),
            Customer.Create(CustomerId.Of(Guid.NewGuid()), "david", "juandmy@hotmail.com"),
        };
    }
}
