using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface IFeatureProductService : IBaseService<FeatureProduct>
    {
        public Task<IEnumerable<FeatureProduct>> GetByProduct(Guid productId);

        public Task<Dictionary<Guid, List<FeatureProduct>>> GetByProductGroupedByFeature(Guid productId);

        public Task<Dictionary<Guid, List<FeatureProduct>>> GetByPrincipalProductGroupedByFeature(Guid principalProductId);
    }
}
