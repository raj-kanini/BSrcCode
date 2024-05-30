using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingPortalApi.Models;

namespace ShoppingPortalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext productDbContext;
        public ProductController(ProductDbContext productDbContext)
        {
            this.productDbContext = productDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<Product>> GetAllProduct()
        {
            var products=await productDbContext.Products.ToListAsync();
            if (products == null)
            {
                return NotFound("No Product Found");
            }
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            await productDbContext.AddAsync(product);
            await productDbContext.SaveChangesAsync();
           
            return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"No Product Found with id {id}");
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await productDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"No Product Found with id {id}");
            }

            productDbContext.Products.Remove(product);
            await productDbContext.SaveChangesAsync();

            return Ok($"Product with id {id} has been deleted.");
        }


    }
}
