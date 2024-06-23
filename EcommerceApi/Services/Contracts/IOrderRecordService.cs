using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface IOrderHistoryService : IBaseService<OrderHistory>
    {
        public Task<IEnumerable<OrderHistory>> GetByOrder(Guid orderId);
    }
}
