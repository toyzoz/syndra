using Catalog.API.Exceptions;

namespace Catalog.API.Models;

public class CatalogItem
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string PictureFileName { get; set; } = null!;
    public int CatalogTypeId { get; set; }
    public CatalogType? CatalogType { get; set; }
    public int CatalogBrandId { get; set; }
    public CatalogBrand? CatalogBrand { get; set; }

    // 可用库存
    public int AvailableStock { get; set; }

    // 库存阈值
    public int RestockThreshold { get; set; }

    // 最大库存
    public int MaxStockThreshold { get; set; }

    public int RemoveStock(int quantityDesired)
    {
        if (quantityDesired < 0)
        {
            throw new CatalogDomainException($"Invalid quantity {quantityDesired}");
        }

        if (AvailableStock == 0)
        {
            throw new CatalogDomainException("Empty stock, product is sold out");
        }

        int removeStock = Math.Min(AvailableStock, quantityDesired);
        AvailableStock -= removeStock;
        return removeStock;
    }

    public int AddStock(int quantity)
    {
        if (quantity < 0)
        {
            throw new CatalogDomainException(" Invalid quantity");
        }

        int original = AvailableStock;

        // 如果库存超过最大库存阈值
        if (AvailableStock + quantity > MaxStockThreshold)
        {
            int maxStockThreshold = MaxStockThreshold - AvailableStock;
            AvailableStock = maxStockThreshold;
            return maxStockThreshold;
        }

        AvailableStock += quantity;
        return quantity;
    }
}
