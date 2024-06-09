using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface IOrderService : IBaseService<Order>
    {
        public Task<IEnumerable<Order>> GetAllActive();
        public Task<Order> GetByIdActive(Guid id);
        public Task<IEnumerable<Order>> GetByCustomer(Guid customerId);
        public Task<Order> SaveOrder(Order order);
        public Task<Order> UpdateOrder(Order order);
        public Task<Order> DeleteOrder(Guid Id);
    }
}
