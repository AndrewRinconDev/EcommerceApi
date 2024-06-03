using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        IOrderRepository _orderRepository;
        public OrderService(IOrderRepository repository) : base(repository) {
            _orderRepository = repository;
        }

        public async Task<IEnumerable<Order>> GetAllActive()
        {
            return await _orderRepository.GetAllActive();
        }
        public async Task<Order> GetByIdActive(Guid id)
        {
            return await _orderRepository.GetByIdActive(id);
        }
        public async Task<IEnumerable<Order>> GetByCustomer(Guid customerId)
        {
            return await _orderRepository.GetByCustomer(customerId);
        }

        public async Task<Order> DeleteOrder(Guid Id)
        {
            var order = await _orderRepository.GetById(Id);
            if (order != null) throw new Exception("Order not found");

            order.isActive = false;
            return await _orderRepository.Update(order);
        }
    }
}
