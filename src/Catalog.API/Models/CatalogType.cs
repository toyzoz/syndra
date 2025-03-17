using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models;

public class CatalogType
{
    public int Id { get; set; }
    [Required] [MaxLength(50)] public required string Type { get; set; }
}