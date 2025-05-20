using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praktimupaa2.Models.Category;


namespace praktimupaa2.Controllers
{
    [Authorize]
    [Route("bangunin/v1/[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly string _consStr;

        public CategoryController(IConfiguration configuration)
        {
            _consStr = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult<Category> GetCategory()
        {
            CategoryContext context = new CategoryContext(_consStr);
            List<Category> categories = context.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost]
        public ActionResult CreateCategory([FromBody] Category category)
        {
            CategoryContext context = new CategoryContext(_consStr);
            bool success = context.AddCategory(category);
            if (success)
                return Ok(context);
            return BadRequest("Failed to create Category");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.id_category)
                return BadRequest("ID mismatch");

            CategoryContext context = new CategoryContext(_consStr);
            bool success = context.UpdateCategory(category);
            if (success)
                return Ok(new { message = $"Update category dengan id {id} berhasil" });
            return NotFound();
        }

            
        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            CategoryContext context = new CategoryContext(_consStr);
            bool success = context.DeleteCategory(id);
            if (success)
                return Ok(new { message = $"Menghapus category dengan id {id} berhasil" });
            return NotFound();
        }

    }
    
}
