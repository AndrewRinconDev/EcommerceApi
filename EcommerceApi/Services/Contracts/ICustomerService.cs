using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface ICustomerService : IBaseService<Customer>
    {
        public Task<IEnumerable<Customer>> GetActiveCustomers();

        public Task<Customer?> GetActiveCustomerById(Guid id);

        public Task<Customer> SaveCustomer(Customer customer);

        public Task<Customer> SaveCustomerUser(Customer customer);

        public Task<Customer?> DeleteCustomer(Guid id);

        public Task<Customer> ActiveCustomer(Customer customer, bool isActive = false);

        public bool CustomerUserExists(Guid? userId);

        public bool UserExists(Guid? id, Guid? userId);
    }
}
