using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models;

public class CatalogBrand
{
    public int Id { get; init; }
    [property: Description("Brand name")] public required string Brand { get; set; }
}