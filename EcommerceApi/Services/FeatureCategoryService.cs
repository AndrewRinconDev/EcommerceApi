using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class FeatureCategoryService : BaseService<FeatureCategory>, IFeatureCategoryService
    {
        public FeatureCategoryService(IFeatureCategoryRepository repository) : base(repository) { }
    }
}
