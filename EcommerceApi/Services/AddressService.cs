using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class AddressService : BaseService<Address>, IAddressService
    {
        public AddressService(IBaseEntityFrameworkRepository<Address> repository) : base(repository) { }

        public async Task<IEnumerable<Address>> GetActiveAddresses()
        {
            return await ((IAddressRepository)_repository).GetActiveAddresses();
        }
        
        public async Task<Address?> GetActiveAddressById(Guid id)
        {
            return await ((IAddressRepository)_repository).GetActiveAddressById(id);
        }

        public async Task<Address?> GetAddressById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Address>> GetActiveAddressByCustomerId(Guid customerId)
        {
            return await ((IAddressRepository)_repository).GetActiveAddressByCustomerId(customerId);
        }

        public async Task<Address> SaveAddress(Address address)
        {
            address.id = Guid.NewGuid();
            address.isActive = true;

            return await _repository.Save(address);
        }
        
        public async Task<Address> UpdateAddress(Address address)
        {
            return await _repository.Update(address);
        }

        public async Task<Address?> DeleteAddress(Guid id)
        {
            var address = await _repository.GetById(id);
            if (address == null) return null;

            address.isActive = false;
            return await _repository.Update(address);
        }

        public bool AddressExists(Guid? id)
        {
            return ((IAddressRepository)_repository).AddressExists(id);
        }
    }
}
