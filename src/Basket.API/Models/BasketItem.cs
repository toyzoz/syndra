using System.ComponentModel.DataAnnotations;

namespace Basket.API.Models;

public class BasketItem : IValidatableObject
{
    public string Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OldUnitPrice { get; set; }
    public int Quantity { get; set; }
    public string PictureUrl { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult>? results = new();

        if (Quantity < 1) results.Add(new ValidationResult("Invalid number of units", ["Quantity"]));

        return results;
    }
}