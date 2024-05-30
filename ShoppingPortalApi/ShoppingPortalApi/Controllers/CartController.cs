using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingPortalApi.Models;

namespace ShoppingPortalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ProductDbContext productDbContext;
        public CartController(ProductDbContext productDbContext)
        {
            this.productDbContext = productDbContext;
        }
        [HttpPost("addToCart")]
        public async Task<ActionResult<Product>> AddToCart(CartDetail c)
        {
            var user = await productDbContext.Users.FindAsync(c.Userid);
            var product = await productDbContext.Products.FindAsync(c.Poductid);

            if (user != null && product != null)
            {
                if (user.Cart == null)
                {
                    user.Cart = new List<Product>();
                }

                user.Cart.Add(product);
                await productDbContext.SaveChangesAsync(); 
                return Ok(user);
            }

            return BadRequest("Invalid user or product.");
        }
        [HttpGet("GetProduct/{productId}")]
        public async Task<ActionResult<Product>> GetProductById( int productId)
        {
            var product= await productDbContext.Products.FindAsync(productId);
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest("Not Found");
        }
        [HttpGet("GetUser/{userId}")]
        public async Task<ActionResult<Product>> GetUserById(int userId)
        {
            var user =  productDbContext.Users.Include(x=>x.Cart).Where(x=>x.Id==userId);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("Not Found");
        }

        [HttpDelete("RemoveFromCart/uid/{uid}/pid/{pid}")]
        public async Task<IActionResult> RemoveFromCart(int uid, int pid)
        {
            var user = await productDbContext.Users.Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == uid);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var product = user.Cart.FirstOrDefault(p => p.Id == pid);
            if (product == null)
            {
                return NotFound("Product not found in the user's cart");
            }

            user.Cart.Remove(product);
            await productDbContext.SaveChangesAsync();

            return Ok("Product removed from the cart");
        }


        }
}
