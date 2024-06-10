using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class OrderProductService : BaseService<OrderProduct>, IOrderProductService
    {
        private readonly IOrderProductRepository _orderProductRepository;
        public OrderProductService(IOrderProductRepository repository) : base(repository)
        {
            _orderProductRepository = repository;
        }

        public async Task SaveAllProducts(Order order)
        {
            if (order.orderProducts == null || order.orderProducts.Count == 0) return;

            foreach (var orderProduct in order.orderProducts)
            {
                orderProduct.orderId = order.id;
                await _orderProductRepository.Save(orderProduct);
            }
        }
    }
}
