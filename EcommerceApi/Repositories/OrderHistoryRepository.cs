using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class OrderHistoryRepository : BaseRepository<OrderHistory>, IOrderHistoryRepository
    {
        public OrderHistoryRepository(EcommerceDbContext context) : base(context) { }

        public async Task<IEnumerable<OrderHistory>> GetByOrder(Guid orderId)
        {
            return await _context.Set<OrderHistory>()
                .Where(_ => _.orderId == orderId).ToListAsync();
        }
    }
}
