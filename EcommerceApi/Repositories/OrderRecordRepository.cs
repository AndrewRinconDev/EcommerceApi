using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class OrderRecordRepository : BaseRepository<OrderRecord>, IOrderRecordRepository
    {
        public OrderRecordRepository(EcommerceDbContext context) : base(context) { }

        public async Task<IEnumerable<OrderRecord>> GetByOrder(Guid orderId)
        {
            return await _context.Set<OrderRecord>()
                .Where(_ => _.orderId == orderId).ToListAsync();
        }
    }
}
