using EcommerceApi.Models.Database;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public Task<IEnumerable<Order>> GetAllActive();
        public Task<Order> GetByIdActive(Guid id);
        public Task<IEnumerable<Order>> GetByCustomer(Guid customerId);
    }
}
