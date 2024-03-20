using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories;

namespace EcommerceApi.Services
{
    public class AddressService
    {
        private readonly EcommerceDbContext _context;
        private readonly AddressRepository _addressRepository;

        public AddressService(EcommerceDbContext context, AddressRepository addressRepository)
        {
            _context = context;
            _addressRepository = addressRepository;
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
            return await _addressRepository.GetAddressById(id);
        }

        public async Task<IEnumerable<Address>> GetActiveAddressByCustomerId(Guid customerId)
        {
            return await _addressRepository.GetActiveAddressByCustomerId(customerId);
        }

        public async Task<Address> SaveAddress(Address address)
        {
            address.id = Guid.NewGuid();
            address.isActive = true;

            return await _addressRepository.SaveAddress(address);
        }
        
        public async Task<Address> UpdateAddress(Address address)
        {
            return await _addressRepository.UpdateAddress(address);
        }

        public async Task<Address?> DeleteAddress(Guid id)
        {
            var address = await _addressRepository.GetAddressById(id);
            if (address == null) return null;

            address.isActive = false;
            return await _addressRepository.UpdateAddress(address);
        }

        public bool AddressExists(Guid? id)
        {
            return _addressRepository.AddressExists(id);
        }

    }
}
