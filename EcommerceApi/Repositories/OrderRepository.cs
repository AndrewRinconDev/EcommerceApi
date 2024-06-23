using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(EcommerceDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetAllActive()
        {
            return await _context.Set<Order>()
                .Include(_ => _.customer)
                    .ThenInclude(_ => _.user)
                .Where(_ => _.isActive)
                .OrderByDescending(_ => _.creationOn)
                .ToListAsync();
        }
        
        public async Task<Order> GetByIdActive(Guid id)
        {
            return await _context.Set<Order>()
                .Include(_ => _.customer)
                    .ThenInclude(_ => _.user)
                .Include(_ => _.orderProducts)
                    .ThenInclude(_ => _.product)
                .Include(_ => _.orderHistorys)
                .Where(_ => _.isActive && id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetByCustomer(Guid customerId)
        {
            return await _context.Set<Order>()
                .Where(_ => _.customerId == customerId).ToListAsync();
        }
    }
}
