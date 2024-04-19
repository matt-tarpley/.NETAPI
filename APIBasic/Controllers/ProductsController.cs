using APIBasic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public ActionResult GetAllProducts()
        {
            return Ok(context.Products.ToArray());
        }
    }
}
