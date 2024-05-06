using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface IFavoriteProductService : IBaseService<FavoriteProduct>
    {
        public Task<IEnumerable<FavoriteProduct>> GetFavoriteProducts();

        public Task<IEnumerable<FavoriteProduct>> GetByCustomer(Guid customerId);

        public Task<IEnumerable<FavoriteProduct>> GetByCustomerProduct(Guid customerId, Guid productId);
    }
}
