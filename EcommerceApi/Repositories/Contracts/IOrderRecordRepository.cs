using EcommerceApi.Models.Database;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IOrderHistoryRepository : IBaseRepository<OrderHistory>
    {
        public Task<IEnumerable<OrderHistory>> GetByOrder(Guid orderId);
    }
}
