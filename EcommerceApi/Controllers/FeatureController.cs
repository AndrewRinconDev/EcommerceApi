using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : BaseController<Feature, Feature>
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IMapper mapper, IFeatureService featureService) : base(mapper, featureService)
        {
            _featureService = featureService;
        }

        // GET api/<Features>/customer/5
        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Feature>>> GetByCategory(string categoryId)
        {
            return Ok(await _featureService.GetByCategory(new Guid(categoryId)));
        }
    }
}
