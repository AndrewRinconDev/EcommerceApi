using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface IOrderProductService : IBaseService<OrderProduct>
    {
        public Task SaveAllProducts(Order order);
    }
}
