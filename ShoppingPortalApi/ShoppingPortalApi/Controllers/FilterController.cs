using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingPortalApi.Models;

namespace ShoppingPortalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly ProductDbContext productDbContext;
        public FilterController(ProductDbContext productDbContext)
        {
            this.productDbContext = productDbContext;
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            var products = await productDbContext.Products.Where(p => p.Category == category).ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound($"No Products Found in category {category}");
            }

            return Ok(products);
        }

    }
}
