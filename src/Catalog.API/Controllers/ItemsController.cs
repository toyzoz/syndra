using System.Collections;
using System.ComponentModel;
using System.Net;
using Catalog.API.Data;
using Catalog.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Catalog.API.Controllers;
//https://stackoverflow.com/questions/54336578/cant-decide-between-taskiactionresult-iactionresult-and-actionresultthing
[ApiController]
[Route("[controller]")]
public class ItemsController(
    CatalogContext context,
    IWebHostEnvironment environment)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginatedItems<CatalogItem>>> GetListAsync(
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
        
        return Ok(new PaginatedItems<CatalogItem>(pageSize, pageIndex, count, catalogItems));
    }

    [ProducesResponseType((int)HttpStatusCode.Created)]
    [HttpPost]
    public async Task<ActionResult> CreateAsync(CatalogItem input)
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

        return CreatedAtRoute(nameof(GetByIdAsync), new { id = catalogItem.Id }, catalogItem);
    }

    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var catalogItem = await context.CatalogItems.FindAsync(id);
        if (catalogItem is null) return NotFound();

        context.CatalogItems.Remove(catalogItem);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [ProducesResponseType((int)HttpStatusCode.OK)]
    [HttpGet("{id:int}",Name =nameof(GetByIdAsync))]
    public async Task<ActionResult<CatalogItem>> GetByIdAsync(int id)
    {
        var catalogItem = await context.CatalogItems.FindAsync(id);
        return catalogItem is null ? NotFound() : Ok(catalogItem);
    }

    [ProducesResponseType((int)HttpStatusCode.OK)]
    [HttpGet("{ids}")]
    public async Task<ActionResult<IEnumerable<CatalogItem>>> GetByIdsAsync(int[] ids)
    {
        var catalogItems = await context.CatalogItems.Where(x => ids.Contains(x.Id)).ToListAsync();
        return Ok(catalogItems);
    }

    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, CatalogItem input)
    {
        var catalogItem = await context.CatalogItems.FindAsync(id);
        if (catalogItem is null) return NotFound();

        EntityEntry<CatalogItem>? entityEntry = context.CatalogItems.Entry(catalogItem);
        entityEntry.CurrentValues.SetValues(input);

        if (entityEntry.Property(e => e.Price).IsModified)
        {
            // todo: add price change event
        }

        await context.SaveChangesAsync();
        // https://www.josephguadagno.net/2020/07/01/no-route-matches-the-supplied-values
        return CreatedAtRoute(nameof(GetByIdAsync), new { id = catalogItem.Id }, catalogItem);
    }

    //[ProducesResponseType((int)HttpStatusCode.OK)]
    [HttpGet("{id:int}/pic")]
    public async Task<ActionResult> GetImageByIdAsync(int id)
    {
        var item = await context.CatalogItems.FindAsync(id);
        if (item is null) return NotFound();

        var fullPath = GetFullPath(environment.ContentRootPath, item.PictureFileName);
        var extension = Path.GetExtension(item.PictureFileName);
        var imageMimeType = GetImageMimeTypeFromImageFileExtension(extension);

        var bytes = await System.IO.File.ReadAllBytesAsync(fullPath);
        return File(bytes, imageMimeType);
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