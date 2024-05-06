using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class FavoriteProductService : BaseService<FavoriteProduct>, IFavoriteProductService
    {
        IFavoriteProductRepository _FavoriteProductRepository;
        public FavoriteProductService(IBaseRepository<FavoriteProduct> repository) : base(repository) {
            _FavoriteProductRepository = (IFavoriteProductRepository)repository;
        }

        public async Task<IEnumerable<FavoriteProduct>> GetFavoriteProducts()
        {
            return await _FavoriteProductRepository.GetFavoriteProducts();
        }
        
        public async Task<IEnumerable<FavoriteProduct>> GetByCustomer(Guid customerId)
        {
            return await _FavoriteProductRepository.GetByCustomer(customerId);
        }
        
        public async Task<IEnumerable<FavoriteProduct>> GetByCustomerProduct(Guid customerId, Guid productId)
        {
            return await _FavoriteProductRepository.GetByCustomerProduct(customerId, productId);
        }

    }
}
