using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class AddressRepository
    {
        private readonly EcommerceDbContext _context;
        public AddressRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetActiveAddresses()
        {
            return await _context.Addresses.Where(_ => _.isActive == true).ToListAsync();
        }

        public async Task<Address?> GetActiveAddressById(Guid id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(_ => _.id == id && _.isActive == true);
        }
        public async Task<Address?> GetAddressById(Guid id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task<IEnumerable<Address>> GetActiveAddressByCustomerId(Guid customerId)
        {
            return await _context.Addresses.Where(_ => _.customerId == customerId && _.isActive == true).ToListAsync();
        }
        
        public async Task<Address> SaveAddress(Address address)
        {
            _context.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }
        
        public async Task<Address> UpdateAddress(Address address)
        {
            _context.Update(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public bool AddressExists(Guid? id)
        {
            return _context.Addresses.Any(e => e.id == id);
        }

    }
}
