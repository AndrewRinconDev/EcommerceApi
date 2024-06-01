using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteProductController : BaseController<FavoriteProduct, FavoriteProductDto>
    {
        private readonly IFavoriteProductService _favoriteProductService;
        private readonly IMapper _mapper;

        public FavoriteProductController(IMapper mapper, IFavoriteProductService baseService) : base(mapper, baseService)
        {
            _favoriteProductService = baseService;
            _mapper = mapper;
        }

        // GET: api/<FavoriteProducts>
        [HttpGet("GetIncludeProduct")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<FavoriteProductDto>>> GetIncludeProduct()
        {
            try
            {
                IEnumerable<FavoriteProduct> favoriteProducts = await _favoriteProductService.GetFavoriteProducts();
                return Ok(_mapper.Map<IEnumerable<FavoriteProduct>, IEnumerable<FavoriteProductDto>>(favoriteProducts));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<FavoriteProducts>/customer/5
        [HttpGet("Customer/{customerId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<FavoriteProductDto>>> GetByCustomer(string customerId)
        {
            try
            {
                IEnumerable<FavoriteProduct> favoriteProducts = await _favoriteProductService.GetByCustomer(new Guid(customerId));
                return Ok(_mapper.Map<IEnumerable<FavoriteProduct>, IEnumerable<FavoriteProductDto>>(favoriteProducts));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }
        
        // GET api/<FavoriteProducts>/customer/5
        [HttpGet("Product/{customerId}/{productId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<FavoriteProductDto>>> GetByCustomerProduct(string customerId, string productId)
        {
            try
            {
                IEnumerable<FavoriteProduct> favoriteProducts = await _favoriteProductService.GetByCustomerProduct(new Guid(customerId), new Guid(productId));
                return Ok(_mapper.Map<IEnumerable<FavoriteProduct>, IEnumerable<FavoriteProductDto>>(favoriteProducts));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }
    }
}
