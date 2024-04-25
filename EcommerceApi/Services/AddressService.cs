using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class AddressService : BaseService<Address>, IAddressService
    {
        IAddressRepository _addressRepository;
        public AddressService(IBaseRepository<Address> repository) : base(repository) {
            _addressRepository = (IAddressRepository)repository;
        }

        public async Task<IEnumerable<Address>> GetActiveAddresses()
        {
            return await _addressRepository.GetActiveAddresses();
        }
        
        public async Task<Address?> GetActiveAddressById(Guid id)
        {
            return await _addressRepository.GetActiveAddressById(id);
        }

        public async Task<Address?> GetAddressById(Guid id)
        {
            return await _addressRepository.GetById(id);
        }

        public async Task<IEnumerable<Address>> GetActiveAddressByCustomerId(Guid customerId)
        {
            return await _addressRepository.GetActiveAddressByCustomerId(customerId);
        }

        public async Task<Address> SaveAddress(Address address)
        {
            address.id = Guid.NewGuid();
            address.isActive = true;

            return await _addressRepository.Save(address);
        }
        
        public async Task<Address> UpdateAddress(Address address)
        {
            return await _addressRepository.Update(address);
        }

        public async Task<Address?> DeleteAddress(Guid id)
        {
            var address = await _addressRepository.GetById(id);
            if (address == null) return null;

            address.isActive = false;
            return await _addressRepository.Update(address);
        }

        public bool AddressExists(Guid? id)
        {
            return _addressRepository.AddressExists(id);
        }
    }
}
