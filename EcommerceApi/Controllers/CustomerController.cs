using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(IMapper mapper, ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            try
            {
                return Ok(await _customerService.GetActiveCustomers());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            try
            {
                var customerFound = await _customerService.GetActiveCustomerById(new Guid(id));

                if (customerFound == null) return NotFound();

                return Ok(customerFound);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ActionResult<Customer>> Post(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            if (_customerService.CustomerUserExists(customer.userId)) return BadRequest("Customer by this User already exists");

            return Ok(await _customerService.SaveCustomerUser(customer));
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Put(string id, CustomerDto customerDto)
        {
            Guid userId = new Guid(id);
            if (userId != customerDto.id) return BadRequest();

            if (_customerService.UserExists(userId, customerDto.userId)) return BadRequest("User already is a customer");

            var customer = _mapper.Map<Customer>(customerDto);

            return await _customerService.Update(customer);
        }

        // PUT api/<CustomersController>/Reactive/5
        [HttpPut("Reactive/{id}")]
        public async Task<ActionResult<Customer>> Reactive(string id, Customer customer)
        {
            if (_customerService.UserExists(new Guid(id), customer.userId)) return BadRequest("User already is a customer");

            return await _customerService.ActiveCustomer(customer, true);
        }

        //DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(string id)
        {
            var customer = await _customerService.DeleteCustomer(new Guid(id));

            if (customer == null) return NotFound();
            
            return Ok(customer);
        }
    }
}
