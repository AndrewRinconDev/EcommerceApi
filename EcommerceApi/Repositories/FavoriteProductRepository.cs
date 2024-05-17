using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class FavoriteProductRepository : BaseRepository<FavoriteProduct>, IFavoriteProductRepository
    {
        public FavoriteProductRepository(EcommerceDbContext context) : base(context) { }

        public async Task<IEnumerable<FavoriteProduct>> GetFavoriteProducts()
        {
            return await _context.Set<FavoriteProduct>().Include(_ => _.principalProductId).ToListAsync();
        }

        public async Task<IEnumerable<FavoriteProduct>> GetByCustomer(Guid customerId)
        {
            return await _context.Set<FavoriteProduct>()
                .Where(_ => _.id == customerId).ToListAsync();
        }
        
        public async Task<IEnumerable<FavoriteProduct>> GetByCustomerProduct(Guid customerId, Guid productId)
        {
            return await _context.Set<FavoriteProduct>()
                .Where(_ => _.id == customerId && _.principalProductId == productId).ToListAsync();
        }
    }
}
