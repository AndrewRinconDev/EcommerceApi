using EcommerceApi.Models.Database;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteProductController : ControllerBase
    {
        private readonly IFavoriteProductService _favoriteProductService;

        public FavoriteProductController(IFavoriteProductService favoriteProductService)
        {
            _favoriteProductService = favoriteProductService;
        }

        // GET: api/<FavoriteProducts>
        [HttpGet]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<FavoriteProduct>>> Get()
        {
            try
            {
                return Ok(await _favoriteProductService.GetFavoriteProducts());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<FavoriteProducts>/5
        [HttpGet("{id}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<FavoriteProduct>> GetById(string id)
        {
            try
            {
                var favoriteProductFound = await _favoriteProductService.GetById(new Guid(id));

                if (favoriteProductFound == null) return NotFound();

                return Ok(favoriteProductFound);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // GET api/<FavoriteProducts>/customer/5
        [HttpGet("Customer/{customerId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<FavoriteProduct>>> GetByCustomer(string customerId)
        {
            return Ok(await _favoriteProductService.GetByCustomer(new Guid(customerId)));
        }
        
        // GET api/<FavoriteProducts>/customer/5
        [HttpGet("Product/{customerId}/{productId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<FavoriteProduct>>> GetByCustomerProduct(string customerId, string productId)
        {
            return Ok(await _favoriteProductService.GetByCustomerProduct(new Guid(customerId), new Guid(productId)));
        }

        // POST api/<FavoriteProducts>
        [HttpPost]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<FavoriteProduct>> Post(FavoriteProduct favoriteProduct)
        {
            return Ok(await _favoriteProductService.Save(favoriteProduct));
        }

        // PUT api/<FavoriteProducts>/5
        [HttpPut("{id}")]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<FavoriteProduct>> Put(string id, FavoriteProduct favoriteProduct)
        {
            if (new Guid(id) != favoriteProduct.id) return BadRequest();

            try
            {
                return Ok(await _favoriteProductService.Update(favoriteProduct));
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!await _favoriteProductService.Exist(new Guid(id))) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE api/<FavoriteProducts>/5
        [HttpDelete("{id}")]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            return Ok(await _favoriteProductService.DeleteById(new Guid(id)));
        }
    }
}
