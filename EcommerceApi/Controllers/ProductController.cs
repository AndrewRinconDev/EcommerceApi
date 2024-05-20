using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public ProductController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_context.Products.Where(_ => _.isActive).ToList());
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var productFound = await _context.Products.FindAsync(id);

            if (productFound == null) return NotFound();

            return Ok(productFound);
        }

        // GET api/<ProductsController>/5
        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<PrincipalProduct>>> GetByCategory(string categoryId)
        {
            var productsFound = await _context.PrincipalProducts
                .Where(_ => _.categoryId == new Guid(categoryId) && _.isActive).ToListAsync();

            if (productsFound == null) return NotFound();

            return Ok(productsFound);
        }

        // GET: api/<ProductsController>
        [HttpGet("All")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_context.Products.ToList());
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            product.id = Guid.NewGuid();
            product.isActive = true;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = product.id }, product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(string id, Product product)
        {
            if (new Guid(id) != product.id) return BadRequest();

            return await UpdateProduct(product);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(string id)
        {
            var productFound = await _context.Products.FindAsync(id);

            if (productFound == null) return NotFound();
            productFound.isActive = false;

            return await UpdateProduct(productFound);
        }

        private async Task<ActionResult<Product>> UpdateProduct(Product product)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ProductExists(product.id)) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }

            return Ok(product);
        }

        private bool ProductExists(Guid? id)
        {
            return _context.Products.Any(_ => _.id == id);
        }
    }
}
