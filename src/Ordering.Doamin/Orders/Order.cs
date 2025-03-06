using Ordering.Domain.Events;
using Ordering.Domain.SeedWork;

namespace Ordering.Domain.Orders
{
    public class Order : AggregateRoot
    {
        private readonly List<OrderItem> _orderItems = [];

        private Order() { }

        public Order(string description)
        {
            Description = description;
        }

        public string Description { get; private set; } = default!;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();


        public static Order Create(string description)
        {
            var order = new Order { Description = description };

            order.AddDomainEvent(new OrderCreatedDomainEvent(order));

            return order;
        }

        public void AddItem(int productId,
            string productName,
            string productPicUrl,
            decimal unitPrice,
            int units)
        {
            OrderItem? existingProduct = _orderItems.SingleOrDefault(oi => oi.ProductId == productId);

            if (existingProduct is null)
            {
                OrderItem orderItem = new(productId,
                    productName,
                    productPicUrl,
                    unitPrice,
                    units);
                _orderItems.Add(orderItem);
            }
            else
            {
                existingProduct.AddUnits(units);
            }
        }
    }
}