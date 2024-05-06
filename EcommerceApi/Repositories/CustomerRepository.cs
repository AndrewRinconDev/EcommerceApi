using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(EcommerceDbContext context) : base(context) { }

        public async Task<IEnumerable<Customer>> GetActiveCustomers()
        {
            return await _context.Set<Customer>()
                .Include(_ => _.user)
                .Where(_ => _.isActive == true)
                .ToListAsync();
        }

        public async Task<Customer?> GetActiveCustomerById(Guid id)
        {
            return await _context.Set<Customer>()
                    .Include(_ => _.addresses)
                    .Include(_ => _.user)
                       .ThenInclude(_ => _.role)
                    .FirstOrDefaultAsync(_ => _.id == id);
        }
        
        public async Task<Customer?> GetFullCustomerById(Guid id)
        {
            var customer = await _context.Set<Customer>()
                .Include(_ => _.addresses)
                .Include(_ => _.orders)
                .Include(_ => _.favoriteProducts)
                .FirstOrDefaultAsync(_ => _.id == id);

            if (customer == null) return null;

            return await ActiveCustomer(customer, false);
        }

        public async Task<Customer> ActiveCustomer(Customer customer, bool isActive)
        {
            customer.addresses.ToList().ForEach(_ => _.isActive = isActive);
            customer.isActive = isActive;

            // TODO - Order delete

            return await Update(customer);
        }

        public bool CustomerUserExists(Guid? userId)
        {
            if (userId == null) return false;

            return _context.Set<Customer>().Any(_ => _.userId == userId && _.isActive);
        }

        public bool UserExists(Guid? id, Guid? userId)
        {
            if (userId == null || id == null) return false;

            return _context.Set<Customer>().Any(_ => _.id != id && _.userId == userId);
        }
    }
}
