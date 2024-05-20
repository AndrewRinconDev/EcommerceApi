using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class FeatureRepository : BaseRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(EcommerceDbContext context) : base(context) { }

        public async Task<IEnumerable<Feature>> GetByCategory(Guid featureCategoryId)
        {
            return await _context.Set<Feature>()
                .Where(_ => _.featureCategoryId == featureCategoryId).ToListAsync();
        }
    }
}
