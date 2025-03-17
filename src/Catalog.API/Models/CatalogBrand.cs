using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models;

public class CatalogBrand
{
    public int Id { get; set; }
    [Required][MaxLength(50)] public required string Brand { get; set; }
}
