using Catalog.API.Data;
using Catalog.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandsController(CatalogContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<Ok<List<CatalogBrand>>> GetListAsync()
        {
            var catalogBrands = await context.Brands.OrderBy(x => x.Brand).ToListAsync();
            return TypedResults.Ok(catalogBrands);
        }
    }
}