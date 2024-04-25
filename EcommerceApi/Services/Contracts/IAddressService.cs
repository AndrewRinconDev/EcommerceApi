using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface IAddressService
    {
        public Task<IEnumerable<Address>> GetActiveAddresses();

        public Task<Address?> GetActiveAddressById(Guid id);

        public Task<Address?> GetAddressById(Guid id);

        public Task<IEnumerable<Address>> GetActiveAddressByCustomerId(Guid customerId);

        public Task<Address> SaveAddress(Address address);

        public Task<Address> UpdateAddress(Address address);

        public Task<Address?> DeleteAddress(Guid id);

        public bool AddressExists(Guid? id);
    }
}
