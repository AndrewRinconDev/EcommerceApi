using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;

namespace EcommerceApi.Repositories
{
    public class FeatureCategoryRepository : BaseRepository<FeatureCategory>, IFeatureCategoryRepository
    {
        public FeatureCategoryRepository(EcommerceDbContext context) : base(context) { }
    }
}
