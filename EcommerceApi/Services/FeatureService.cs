using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class FeatureService : BaseService<Feature>, IFeatureService
    {
        private readonly IFeatureRepository _FeatureRepository;

        public FeatureService(IFeatureRepository repository) : base(repository) {
            _FeatureRepository = repository;
        }
        
        public async Task<IEnumerable<Feature>> GetByCategory(Guid featureCategoryId)
        {
            return await _FeatureRepository.GetByCategory(featureCategoryId);
        }
    }
}
