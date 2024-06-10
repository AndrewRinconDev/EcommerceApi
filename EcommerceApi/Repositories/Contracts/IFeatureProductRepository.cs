using EcommerceApi.Models.Database;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IFeatureProductRepository : IBaseRepository<FeatureProduct>
    {
        public Task<IEnumerable<FeatureProduct>> GetByProduct(Guid productId);

        public Task<Dictionary<Guid, List<FeatureProduct>>> GetByProductGroupedByFeature(Guid productId);

        public Task<Dictionary<Guid, List<FeatureProduct>>> GetByPrincipalProductGroupedByFeature(Guid principalProductId);
    }
}
