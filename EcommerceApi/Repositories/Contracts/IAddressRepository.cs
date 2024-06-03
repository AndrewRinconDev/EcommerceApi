using EcommerceApi.Models.Database;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        public Task<IEnumerable<Address>> GetActiveAddresses();

        public Task<Address?> GetActiveAddressById(Guid id);

        public Task<IEnumerable<Address>> GetActiveAddressByCustomerId(Guid customerId);
    }
}
