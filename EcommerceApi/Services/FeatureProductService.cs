using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class FeatureProductService : BaseService<FeatureProduct>, IFeatureProductService
    {
        private readonly IFeatureProductRepository _FeatureProductRepository;

        public FeatureProductService(IFeatureProductRepository repository) : base(repository) {
            _FeatureProductRepository = repository;
        }

        public async Task<Dictionary<Guid, List<FeatureProduct>>> GetByPrincipalProductGroupedByFeature(Guid principalProductId)
        {
            return await _FeatureProductRepository.GetByPrincipalProductGroupedByFeature(principalProductId);
        }

        public async Task<IEnumerable<FeatureProduct>> GetByProduct(Guid productId)
        {
            return await _FeatureProductRepository.GetByProduct(productId);
        }

        public async Task<Dictionary<Guid, List<FeatureProduct>>> GetByProductGroupedByFeature(Guid productId)
        {
            return await _FeatureProductRepository.GetByProductGroupedByFeature(productId);
        }
    }
}
