using Ordering.Doamin.Events;
using Ordering.Doamin.SeedWork;
using System.Runtime.CompilerServices;

namespace Ordering.Doamin.Orders
{
    public class Order: AggregateRoot
    {
        public string Description { get; private set; } = default!;

        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public int Id { get; set; }

        private Order() { }
        public static Order Create(string description)
        {
             var newOrder= new Order
            {
                Description = description
            };

            newOrder.AddDomainEvent(new OrderCreatedDomainEvent(newOrder));

            return newOrder;
        }

        public void AddItem(int productId,
            string productName,
            string productPicUrl,
            decimal unitPrice,
            int units)
        {

            OrderItem exectingProduct = _orderItems.Single(oi => oi.ProductId == productId);

            if (exectingProduct is null)
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
                exectingProduct.AddUnits(units);
            }
        }
    }
}
