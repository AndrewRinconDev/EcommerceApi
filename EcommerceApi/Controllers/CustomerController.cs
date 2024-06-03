using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            try
            {
                var customers = await _customerService.GetActiveCustomers();
                return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<CustomerDto>> Get(string id)
        {
            try
            {
                var customerFound = await _customerService.GetActiveCustomerById(new Guid(id));

                return Ok(_mapper.Map<CustomerDto>(customerFound));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // POST api/<CustomersController>
        [HttpPost]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<CustomerDto>> Post(CustomerDto customerDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                var customerSaved = await _customerService.SaveCustomerUser(customer);
                return Ok(_mapper.Map<CustomerDto>(customerSaved));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<CustomerDto>> Put(string id, CustomerDto customerDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                var cutomerUpdated = await _customerService.UpdateCustomer(new Guid(id), customer);
                return Ok(_mapper.Map<CustomerDto>(cutomerUpdated));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // PUT api/<CustomersController>/Reactive/5
        [HttpPut("Reactive/{id}")]
        [Authorize("AdminWrite")]
        public async Task<ActionResult<CustomerDto>> Reactive(string id, Customer customer)
        {
            try
            {
                var cutomerActived = await _customerService.ActiveCustomer(new Guid(id), customer, true);
                return Ok(_mapper.Map<CustomerDto>(cutomerActived));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        //DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        [Authorize("AdminWrite")]
        public async Task<ActionResult<CustomerDto>> Delete(string id)
        {
            try
            {
                var customer = await _customerService.DeleteCustomer(new Guid(id));

                return Ok(_mapper.Map<CustomerDto>(customer));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}
