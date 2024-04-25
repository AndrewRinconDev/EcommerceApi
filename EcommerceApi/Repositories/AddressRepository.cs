﻿using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class AddressRepository : BaseEntityFrameworkRepository<Address>, IAddressRepository
    {
        public AddressRepository(EcommerceDbContext context) : base(context) { }

        public async Task<IEnumerable<Address>> GetActiveAddresses()
        {
            return await _context.Set<Address>().Where(_ => _.isActive == true).ToListAsync();
        }

        public async Task<Address?> GetActiveAddressById(Guid id)
        {
            return await _context.Set<Address>().FirstOrDefaultAsync(_ => _.id == id && _.isActive == true);
        }

        public async Task<IEnumerable<Address>> GetActiveAddressByCustomerId(Guid customerId)
        {
            return await _context.Set<Address>().Where(_ => _.customerId == customerId && _.isActive == true).ToListAsync();
        }

        public bool AddressExists(Guid? id)
        {
            return _context.Set<Address>().Any(e => e.id == id);
        }
    }
}
