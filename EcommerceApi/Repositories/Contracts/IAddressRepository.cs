using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        public Task<IEnumerable<Address>> GetActiveAddresses();

        public Task<Address?> GetActiveAddressById(Guid id);

        public Task<IEnumerable<Address>> GetActiveAddressByCustomerId(Guid customerId);

        public bool AddressExists(Guid? id);
    }
}
