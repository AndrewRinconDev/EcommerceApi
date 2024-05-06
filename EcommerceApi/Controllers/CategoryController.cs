using EcommerceApi.Models.Database;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            try
            {
               return Ok(await _categoryService.GetActiveCategories());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // GET: api/<CategoryController>/All
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            try
            {
               return Ok(await _categoryService.GetAll());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Category>>> Get(string id)
        {
            var categoryFound = await _categoryService.GetActiveCategoryById(new Guid(id));

            if (categoryFound == null) return NotFound();

            return Ok(categoryFound);
        }
        
        // GET: api/<CategoryController>/Subcategories
        [HttpGet("GetSubcategoriesByParent/{parentId}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetSubcategoriesByParent(string parentId)
        {
            try
            {
                var categories = await _categoryService.GetSubcategoriesByParent(new Guid(parentId));

                return Ok(categories);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            try
            {
                return Ok(await _categoryService.SaveCategory(category));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Put(string id, Category category)
        {
            try
            {
                if (new Guid(id) != category.id) return BadRequest();
    
                return Ok(await _categoryService.Update(category));
            }
            catch (Exception e)
            {
                if (! await _categoryService.Exist(category.id)) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(string id)
        {
            try
            {
                var category = await _categoryService.DeleteCategory(new Guid(id));
                if (category == null) return NotFound();

                return Ok(category);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e);
            }
        }
    }
}
