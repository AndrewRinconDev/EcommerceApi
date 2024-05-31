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
    public class FeatureCategoryController : BaseController<FeatureCategory, FeatureCategory>
    {
        public FeatureCategoryController(IMapper mapper, IBaseService<FeatureCategory> baseService) : base(mapper, baseService) { }
    }
}
