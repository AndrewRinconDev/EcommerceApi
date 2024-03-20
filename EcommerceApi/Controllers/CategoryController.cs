using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public CategoryController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
               return Ok(_context.Categories.Where(_ => _.isActive == true).ToList());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Category>>> Get(string id)
        {
            var categoryFound = await _context.Categories.Include(_ => _.subcategories)
                .FirstOrDefaultAsync(_ => _.id == new Guid(id) && _.isActive == true);

            if (categoryFound == null) return NotFound();

            return Ok(categoryFound);
        }

        // GET: api/<CategoriesController>/Subcategories
        [HttpGet("Subcategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetSubcategories()
        {
            try
            {
                var categories = await _context.Categories
                    .Include(_ => _.subcategories)
                    .Where(_ => _.parentId == null && _.isActive == true)
                    .ToListAsync();

                if (categories == null) return NotFound();

                foreach (Category category in categories)
                {
                    category.subcategories = await GetSubcategories(category.id);
                }

                return Ok(categories);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }


        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            category.id = Guid.NewGuid();
            category.isActive = true;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = category.id }, category);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Put(string id, Category category)
        {
            if (new Guid(id) != category.id) return BadRequest();

            return await Update(category);
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(string id)
        {
            var category = await _context.Categories
                .Include(_ => _.subcategories)
                .FirstOrDefaultAsync(_ => _.id == new Guid(id) && _.isActive == true);

            if (category == null) return NotFound();

            category.isActive = false;
            category.subcategories = await GetSubcategories(category.id, true);

            return await Update(category);
        }
        private bool CategoryExists(Guid? id)
        {
            return _context.Categories.Any(e => e.id == id);
        }

        private async Task<ActionResult<Category>> Update(Category category)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!CategoryExists(category.id)) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }

            return Ok(category);
        }

        private async Task<ICollection<Category>> GetSubcategories(Guid? parentId, bool inisActive = false)
        {
            var subcategories = await _context.Categories
                   .Include(_ => _.subcategories)
                   .Where(_ => _.parentId == parentId && _.isActive == true)
                   .ToListAsync();

            foreach (Category subcategory in subcategories)
            {
                if (subcategory.subcategories != null)
                {
                    subcategory.subcategories = await GetSubcategories(subcategory.id);
                    if (inisActive)
                    {
                        subcategory.isActive = false;
                    }
                }
            }
            
            return subcategories;
        }
    }
}
