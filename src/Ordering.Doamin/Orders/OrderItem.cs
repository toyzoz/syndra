using Ordering.Domain.SeedWork;

namespace Ordering.Domain.Orders
{
    public class OrderItem: Entity
    {
        private OrderItem() { }

        public OrderItem(int productId,
            string productName,
            string productPicUrl,
            decimal unitPrice,
            int units)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = productPicUrl;
            UnitPrice = unitPrice;
            Units = units;
        }

        public int ProductId { get; private set; }
        public string ProductName { get; private set; } = default!;
        public string PictureUrl { get; private set; } = default!;
        public decimal UnitPrice { get; private set; }
        public int Units { get; private set; }

        internal void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new OrderingDomainException("Invalid unit");
            }

            Units += units;
        }
    }
}