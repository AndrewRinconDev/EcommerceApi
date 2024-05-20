using EcommerceApi.Models.Database;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IFeatureRepository : IBaseRepository<Feature>
    {
        public Task<IEnumerable<Feature>> GetByCategory(Guid featureCategoryId);
    }
}
