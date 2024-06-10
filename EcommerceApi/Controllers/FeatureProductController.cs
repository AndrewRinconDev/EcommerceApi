using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeatureProductController : BaseController<FeatureProduct, FeatureProduct>
    {
        private readonly IFeatureProductService _featureProductService;

        public FeatureProductController(IMapper mapper, IFeatureProductService FeatureProductService) : base(mapper, FeatureProductService)
        {
            _featureProductService = FeatureProductService;
        }

        // GET api/<FeatureProducts>/Product/5
        [HttpGet("Product/{productId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<FeatureProduct>>> GetByProduct(string productId)
        {
            try
            {
                return Ok(await _featureProductService.GetByProduct(new Guid(productId)));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // GET api/<FeatureProducts>/ProductGroupedByFeature/5
        [HttpGet("ProductGroupedByFeature/{productId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<Dictionary<Guid, List<FeatureProduct>>>> GetByProductGroupedByFeature(string productId)
        {
            try
            {
                return Ok(await _featureProductService.GetByProductGroupedByFeature(new Guid(productId)));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // GET api/<FeatureProducts>/PrincipalProductGroupedByFeature/5
        [HttpGet("PrincipalProductGroupedByFeature/{principalProductId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<Dictionary<Guid, List<FeatureProduct>>>> GetByPrincipalProductGroupedByFeature(string principalProductId)
        {
            try
            {
                return Ok(await _featureProductService.GetByPrincipalProductGroupedByFeature(new Guid(principalProductId)));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}
