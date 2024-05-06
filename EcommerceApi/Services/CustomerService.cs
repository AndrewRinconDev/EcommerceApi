using EcommerceApi.Helpers;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        ICustomerRepository _customerRepository;
        private CryptographyHelper _cryptographyHelper;
        public CustomerService(IBaseRepository<Customer> repository, IConfiguration configuration) : base(repository) {
            _customerRepository = (ICustomerRepository)repository;
            _cryptographyHelper = new CryptographyHelper(configuration);
        }

        public async Task<IEnumerable<Customer>> GetActiveCustomers()
        {
            return await _customerRepository.GetActiveCustomers();
        }
        
        public async Task<Customer?> GetActiveCustomerById(Guid id)
        {
            return await _customerRepository.GetActiveCustomerById(id);
        }

        public async Task<Customer> SaveCustomer(Customer customer)
        {
            customer.isActive = true;
            return await _customerRepository.Save(customer);
        }
        
        public async Task<Customer> SaveCustomerUser(Customer customer)
        {
            var user = customer.user;

            if (user.id == Guid.Empty) {
                user.isActive = true;
                user.password = _cryptographyHelper.Encrypt(user.password);
                // TODO : var userSaved = _context.Users.Add(user);
            }

            customer.userId = user.id;
            customer.isActive = true;
            return await _customerRepository.Save(customer);
        }

        public async Task<Customer?> DeleteCustomer(Guid id)
        {
            var customer = await GetById(id);
            if (customer == null) return null;

            customer.isActive = false;
            return await Update(customer);
        }

        public async Task<Customer> ActiveCustomer(Customer customer, bool isActive = false)
        {
            return await _customerRepository.ActiveCustomer(customer, isActive);
        }

        public bool CustomerUserExists(Guid? userId)
        {
            return _customerRepository.CustomerUserExists(userId);
        }

        public bool UserExists(Guid? id, Guid? userId)
        {
            return _customerRepository.UserExists(id, userId);
        }
    }
}
