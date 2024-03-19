using EcommerceApi.Context;
using EcommerceApi.Dto;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteProductsController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public FavoriteProductsController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/<FavoriteProducts>
        [HttpGet]
        public ActionResult<IEnumerable<FavoriteProduct>> Get()
        {
            try
            {
                return Ok(_context.FavoriteProducts.Include(_ => _.product).ToList());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<FavoriteProducts>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteProduct>> Get(string id)
        {
            try
            {
                var favoriteProductFound = await _context.FavoriteProducts.Include(_ => _.product)
                .FirstOrDefaultAsync(_ => _.id == new Guid(id) && _.isActive == true);

                if (favoriteProductFound == null) return NotFound();

                return Ok(favoriteProductFound);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<FavoriteProducts>/customer/5
        [HttpGet("Customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<FavoriteProduct>>> GetByCustomer(string customerId)
        {
            var favoriteProductFound = await _context.FavoriteProducts
                .Where(_ => _.id == new Guid(customerId) && _.isActive == true).ToListAsync();

            if (favoriteProductFound == null) return NotFound();

            return Ok(favoriteProductFound);
        }

        // POST api/<FavoriteProducts>
        [HttpPost]
        public async Task<ActionResult<FavoriteProduct>> Post(FavoriteProduct favoriteProduct)
        {
            favoriteProduct.id = Guid.NewGuid();
            favoriteProduct.isActive = true;

            _context.Add(favoriteProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = favoriteProduct.id }, favoriteProduct);
        }

        // PUT api/<FavoriteProducts>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<FavoriteProduct>> Put(string id, FavoriteProduct favoriteProduct)
        {
            if (new Guid(id) != favoriteProduct.id) return BadRequest();

            try
            {
                _context.Update(favoriteProduct);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!FavoriteProductExists(new Guid(id))) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }

            return Ok(favoriteProduct);
        }

        // DELETE api/<FavoriteProducts>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            var favoriteProduct = await _context.FavoriteProducts.FindAsync(id);
            if (favoriteProduct == null) return NotFound();

            _context.FavoriteProducts.Remove(favoriteProduct);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        private bool FavoriteProductExists(Guid? id)
        {
            return _context.FavoriteProducts.Any(e => e.id == id);
        }
    }
}
