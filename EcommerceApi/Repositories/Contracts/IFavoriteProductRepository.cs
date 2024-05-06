using EcommerceApi.Models.Database;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IFavoriteProductRepository : IBaseRepository<FavoriteProduct>
    {
        public Task<IEnumerable<FavoriteProduct>> GetFavoriteProducts();
        
        public Task<IEnumerable<FavoriteProduct>> GetByCustomer(Guid customerId);

        public Task<IEnumerable<FavoriteProduct>> GetByCustomerProduct(Guid customerId, Guid productId);
    }
}
