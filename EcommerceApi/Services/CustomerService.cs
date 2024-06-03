using EcommerceApi.Helpers;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly CryptographyHelper _cryptographyHelper;
        public CustomerService(ICustomerRepository repository, IUserRepository userRepository, IConfiguration configuration) : base(repository) {
            _customerRepository = repository;
            _userRepository = userRepository;
            _cryptographyHelper = new CryptographyHelper(configuration);
        }

        public async Task<IEnumerable<Customer>> GetActiveCustomers()
        {
            return await _customerRepository.GetActiveCustomers();
        }
        
        public async Task<Customer?> GetActiveCustomerById(Guid id)
        {
            var customer = await _customerRepository.GetActiveCustomerById(id);
            if (customer == null) throw new Exception("Customer not found");
            return customer;
        }

        public async Task<Customer> SaveCustomer(Customer customer)
        {
            if (CustomerUserExists(customer.userId))
            {
                throw new Exception("Customer by this User already exists");
            }
            customer.isActive = true;
            return await _customerRepository.Save(customer);
        }
        
        public async Task<Customer> SaveCustomerUser(Customer customer)
        {
            var user = customer.user;

            if (user.id == Guid.Empty || customer.userId == null) {
                user.isActive = true;
                user.password = _cryptographyHelper.Encrypt(user.password);
                customer.userId = (await _userRepository.Save(user)).id;
            }
            else if (CustomerUserExists(customer.userId))
            {
                throw new Exception("Customer by this User already exists");
            }

            customer.isActive = true;
            return await _customerRepository.Save(customer);
        }

        public async Task<Customer> UpdateCustomer(Guid id, Customer customer)
        {
            if (id != customer.id) throw new Exception("Id does not match");

            if (UserExists(id, customer.userId)) throw new Exception("User already exists");

            return await _customerRepository.Update(customer);
        }

        public async Task<Customer?> DeleteCustomer(Guid id)
        {
            var customer = await GetById(id);
            if (customer == null) throw new Exception("Customer not found");

            customer.isActive = false;
            return await Update(customer);
        }

        public async Task<Customer> ActiveCustomer(Guid id, Customer customer, bool isActive = false)
        {
            if (id != customer.id) throw new Exception("Id does not match");

            if (UserExists(customer.id, customer.userId)) throw new Exception("User already exists");

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
