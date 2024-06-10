using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class FeatureProductRepository : BaseRepository<FeatureProduct>, IFeatureProductRepository
    {
        public FeatureProductRepository(EcommerceDbContext context) : base(context) { }

        public async Task<IEnumerable<FeatureProduct>> GetByProduct(Guid productId)
        {
            return await _context.Set<FeatureProduct>()
                .Include(_ => _.feature)
                    .ThenInclude(_ => _.featureCategory)
                .Where(_ => _.productId == productId)
                .ToListAsync();
        }

        public async Task<Dictionary<Guid, List<FeatureProduct>>> GetByProductGroupedByFeature(Guid productId)
        {
            return _context.Set<FeatureProduct>()
                .Include(_ => _.feature)
                    .ThenInclude(_ => _.featureCategory)
                .Where(_ => _.productId == productId)
                .ToList()
                .GroupBy(_ => _.featureId)
                .ToDictionary(d => d.Key, d => d.Select(d => d).ToList());
        }

        public async Task<Dictionary<Guid, List<FeatureProduct>>> GetByPrincipalProductGroupedByFeature(Guid principalProductId)
        {
            return _context.Set<FeatureProduct>()
                .Include(_ => _.feature)
                    .ThenInclude(_ => _.featureCategory)
                .Where(_ => _.product.principalProductId == principalProductId)
                .ToList()
                .GroupBy(_ => _.featureId)
                .ToDictionary(d => d.Key, d => d.Select(d => d).ToList());
        }
    }
}
