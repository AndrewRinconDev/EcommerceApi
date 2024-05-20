using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface IFeatureService : IBaseService<Feature>
    {
        public Task<IEnumerable<Feature>> GetByCategory(Guid featureCategoryId);
    }
}
