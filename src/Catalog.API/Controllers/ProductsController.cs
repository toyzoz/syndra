using Catalog.API.Data;
using Catalog.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(CatalogContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<Ok<List<Product>>> GetListAsync()
        {
            List<Product> products = await context.Products.ToListAsync();

            return TypedResults.Ok(products);
        }

        [HttpPost]
        public async Task CreateAsync(Product input)
        {
            Product product = new()
            {
                Name = input.Name,
                Description = input.Description
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }
    }
}