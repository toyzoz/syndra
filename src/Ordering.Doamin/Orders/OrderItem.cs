
namespace Ordering.Doamin.Orders
{
    public class OrderItem
    {
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

        public int ProductId { get; private set;}
        public string ProductName { get;private set; }
        public string PictureUrl { get;private set; }
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