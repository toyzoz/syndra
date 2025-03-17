using System.Collections;
using System.ComponentModel;
using Catalog.API.Data;
using Catalog.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Catalog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController(
    CatalogContext context,
    IWebHostEnvironment environment)
    : ControllerBase
{
    [HttpGet]
    public async Task<Ok<PaginatedItems<CatalogItem>>> GetListAsync(
        [FromQuery] PaginationRequest request,
        [Description("Filter by name.")] string? name,
        [Description("Filter by type.")] int? type,
        [Description("Filter by brand.")] int? brand)
    {
        IQueryable<CatalogItem> root = context.CatalogItems;
        if (name is not null) root = context.CatalogItems.Where(x => x.Name.StartsWith(name));

        if (type is not null) root = root.Where(x => x.CatalogTypeId == type);

        if (brand is not null) root = root.Where(x => x.CatalogBrandId == brand);

        var count = await root.LongCountAsync();
        var pageSize = request.PageSize;
        var pageIndex = request.PageIndex;
        IEnumerable catalogItems = await root
            .OrderBy(i => i.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return TypedResults.Ok(new PaginatedItems<CatalogItem>(pageSize, pageIndex, count, catalogItems));
    }


    [HttpPost]
    public async Task<CreatedAtRoute> CreateAsync(CatalogItem input)
    {
        CatalogItem catalogItem = new()
        {
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            PictureFileName = input.PictureFileName,
            CatalogBrandId = input.CatalogBrandId,
            CatalogTypeId = input.CatalogTypeId,
            AvailableStock = input.AvailableStock,
            RestockThreshold = input.RestockThreshold,
            MaxStockThreshold = input.MaxStockThreshold
        };
        context.CatalogItems.Add(catalogItem);
        await context.SaveChangesAsync();

        return TypedResults.CreatedAtRoute(nameof(GetByIdAsync), new { id = catalogItem.Id });
    }

    [HttpDelete("{id:int}")]
    public async Task<Results<NotFound, NoContent>> DeleteAsync(int id)
    {
        var catalogItem = await context.CatalogItems.FindAsync(id);
        if (catalogItem is null) return TypedResults.NotFound();

        context.CatalogItems.Remove(catalogItem);
        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    [HttpGet("{id:int}", Name = nameof(GetByIdAsync))]
    public async Task<Results<Ok<CatalogItem>, NotFound>> GetByIdAsync(int id)
    {
        var catalogItem = await context.CatalogItems.FindAsync(id);
        return catalogItem is null ? TypedResults.NotFound() : TypedResults.Ok(catalogItem);
    }

    [HttpGet("{ids}")]
    public async Task<Ok<List<CatalogItem>>> GetByIdsAsync(int[] ids)
    {
        var catalogItems = await context.CatalogItems.Where(x => ids.Contains(x.Id)).ToListAsync();
        return TypedResults.Ok(catalogItems);
    }

    [HttpPut("{id:int}")]
    public async Task<Results<CreatedAtRoute, NotFound>> UpdateAsync(int id, CatalogItem input)
    {
        var catalogItem = await context.CatalogItems.FindAsync(id);
        if (catalogItem is null) return TypedResults.NotFound();

        EntityEntry<CatalogItem>? entityEntry = context.CatalogItems.Entry(catalogItem);
        entityEntry.CurrentValues.SetValues(input);

        if (entityEntry.Property(e => e.Price).IsModified)
        {
            // todo: add price change event
        }

        await context.SaveChangesAsync();
        return TypedResults.CreatedAtRoute(nameof(GetByIdAsync), new { id = catalogItem.Id });
    }

    [HttpGet("{id:int}/pic")]
    public async Task<Results<PhysicalFileHttpResult, NotFound>> GetImageByIdAsync(int id)
    {
        var item = await context.CatalogItems.FindAsync(id);
        if (item is null) return TypedResults.NotFound();

        var fullPath = GetFullPath(environment.ContentRootPath, item.PictureFileName);
        var extension = Path.GetExtension(item.PictureFileName);
        var imageMimeType = GetImageMimeTypeFromImageFileExtension(extension);

        return TypedResults.PhysicalFile(fullPath, imageMimeType);
    }


    private static string GetFullPath(string environmentContentRootPath, string itemPictureFileName)
    {
        return Path.Combine(environmentContentRootPath, "Pics", itemPictureFileName);
    }

    private static string GetImageMimeTypeFromImageFileExtension(string extension)
    {
        return extension switch
        {
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".bmp" => "image/bmp",
            ".tiff" => "image/tiff",
            ".wmf" => "image/wmf",
            ".jp2" => "image/jp2",
            ".svg" => "image/svg+xml",
            ".webp" => "image/webp",
            _ => "application/octet-stream"
        };
    }
}