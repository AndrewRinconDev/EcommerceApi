using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class OrderProductService : BaseService<OrderProduct>, IOrderProductService
    {
        public OrderProductService(IOrderProductRepository repository) : base(repository) { }

        public async Task SaveAllProducts(Order order)
        {
            if (order.orderProducts == null || order.orderProducts.Count == 0) return;

            foreach (var orderProduct in order.orderProducts)
            {
                orderProduct.orderId = order.id;
                await _repository.Save(orderProduct);
            }
        }
    }
}
