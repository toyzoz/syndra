namespace Catalog.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}