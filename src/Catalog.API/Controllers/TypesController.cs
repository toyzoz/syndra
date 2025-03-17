

using Catalog.API.Data;
using Catalog.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TypesController(CatalogContext context) : ControllerBase
{
    [HttpGet]
    public async Task<Ok<List<CatalogType>>> GetListAsync()
    {
        List<CatalogType>? types = await context.Types.OrderBy(x => x.Type).ToListAsync();
        return TypedResults.Ok(types);
    }
}
