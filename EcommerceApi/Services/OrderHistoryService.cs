using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class OrderHistoryService : BaseService<OrderHistory>, IOrderHistoryService
    {
        IOrderHistoryRepository _orderHistoryRepository;

        public OrderHistoryService(IOrderHistoryRepository repository) : base(repository) {
            _orderHistoryRepository = repository;
        }

        public async Task<IEnumerable<OrderHistory>> GetByOrder(Guid orderId)
        {
            return await _orderHistoryRepository.GetByOrder(orderId);
        }
    }
}
