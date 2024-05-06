using EcommerceApi.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories.Contracts
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        public Task<IEnumerable<Customer>> GetActiveCustomers();

        public Task<Customer?> GetActiveCustomerById(Guid id);

        public Task<Customer?> GetFullCustomerById(Guid id);

        public Task<Customer> ActiveCustomer(Customer customer, bool isActive);

        public bool CustomerUserExists(Guid? userId);

        public bool UserExists(Guid? id, Guid? userId);
    }
}
