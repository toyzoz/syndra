namespace Catalog.API.Models;

public record CatalogOutput(int Id, string Name, string Description, string Brand, string Type)
{
    public int Id { get; set; } = Id;
    public string Name { get; set; } = Name;
    public string Description { get; set; } = Description;
    public string Brand { get; set; } = Brand;
    public string Type { get; set; } = Type;
}
