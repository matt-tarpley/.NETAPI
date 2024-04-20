using APIBasic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBasic.Controllers
{
    [Route("api/[controller]")] //api/products
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext context;

        public ProductsController(ShopContext shopContext) 
        {
            context = shopContext;
            context.Database.EnsureCreated(); //since using built in db....ensure the db is created
        }


        //get all
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            return Ok(await context.Products.ToArrayAsync());
        }

        //get by id
        [HttpGet("{id}")] //since controller level route exists you only need final part of url
        public async Task<ActionResult> GetProduct(int id) 
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            } 

            return Ok(product);
        }

        //post 
        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();

            //returns the created item
            return CreatedAtAction(
                "GetProduct",
                new { id = product.Id },
                product);
        }

        //update existing product | put
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, [FromBody] Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }

            context.Entry(product).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            { 
                if(!context.Products.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            { 
                return NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return product;
        }

        //delete multiple
        [HttpPost("{id}")]
        [Route("Delete")]
        public async Task<ActionResult<Product>> DeleteMultiple([FromQuery] int[] ids)
        {
            var products = new List<Product>();

            //if one of the provided id's does not return a product, cancel transaction
            foreach (var id in ids)
            {
                var product = await context.Products.FindAsync(id);

                if(product == null)
                {
                    return NotFound();
                }

                products.Add(product);
            }


            context.Products.RemoveRange(products);
            await context.SaveChangesAsync();

            return Ok(products);
        }
    }
}
