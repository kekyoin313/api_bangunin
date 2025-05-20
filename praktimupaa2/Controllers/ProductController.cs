using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praktimupaa2.Models.Product;

namespace praktimupaa2.Controllers
{
    [Authorize]
    [Route("bangunin/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly string _consStr;

        public ProductController(IConfiguration configuration)
        {
            _consStr = configuration.GetConnectionString("DefaultConnection");
        }


        [HttpGet]
        public ActionResult<Product> GetProducts()
        {
            ProductContext context = new ProductContext(_consStr);
            List<Product> products = context.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            ProductContext context = new ProductContext(_consStr);
            Product product = context.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public ActionResult CreateProduct([FromBody] Product product)
        {
            ProductContext context = new ProductContext(_consStr);
            bool success = context.AddProduct(product);
            if (success)
                return Ok(product);
            return BadRequest("Failed to create product");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.id_product)
                return BadRequest("ID mismatch");

            ProductContext context = new ProductContext(_consStr);
            bool success = context.UpdateProduct(product);
            if (success)
                return Ok(new { message = $"Update product dengan id {id} berhasil" });
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            ProductContext context = new ProductContext(_consStr);
            bool success = context.DeleteProduct(id);
            if (success)
                return Ok(new { message = $"Menghapus Product dengan id {id} berhasil" });
            return NotFound();
        }
    }
}
