using AutoMapper;
using EcommerceApi.Context;
using EcommerceApi.Dto;
using EcommerceApi.Helpers;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly EcommerceDbContext _context;
        private CryptographyHelper cryptographyHelper;
        private readonly IMapper _mapper;

        public CustomersController(EcommerceDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            cryptographyHelper = new CryptographyHelper(configuration);
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            try
            {
                return Ok(_context.Customers.Include(_ => _.user).ToList());
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
                var customerFound = await _context.Customers
                    .Include(_ => _.addresses)
                    .Include(_ => _.user)
                       .ThenInclude(_ => _.role)
                    .FirstOrDefaultAsync(_ => _.id == id);

                if (customerFound == null) return NotFound();

                return Ok(customerFound);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ActionResult<Customer>> Post(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            if (CustomerUserExists(customer.userId)) return BadRequest("User already exists");

            customer.id = Guid.NewGuid().ToString();
            customer.isActive = true;

            _context.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = customer.id }, customer);
        }
        // POST api/<CustomersController>/user
        [HttpPost("User")]
        public async Task<ActionResult<Customer>> PostCustomerUser(Customer customer)
        {
            var user = customer.user;
            user.id = Guid.NewGuid().ToString();
            user.isActive = true;
            user.password = cryptographyHelper.Encrypt(user.password);
            _context.Users.Add(user);

            customer.id = Guid.NewGuid().ToString();
            customer.userId = user.id;
            customer.isActive = true;
            _context.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = customer.id }, customer);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Put(string id, CustomerDto customerDto)
        {
            if (id != customerDto.id) return BadRequest();

            var userIdExist = _context.Customers.Any(_ => _.id != id && _.userId == customerDto.userId);
            if (userIdExist) return BadRequest("User already exists");

            var customer = _mapper.Map<Customer>(customerDto);

            return await Update(customer);
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(string id)
        {
            var customer = await _context.Customers.Include(_ => _.addresses)
                .FirstOrDefaultAsync(_ => _.id == id);

            if (customer == null) return NotFound();

            customer.addresses.ToList().ForEach(_ => _.isActive = false);
            customer.isActive = false;

            return await Update(customer);
        }

        private bool CustomerExists(string? id)
        {
            if (id == null) return false;

            return _context.Customers.Any(e => e.id == id);
        }
        
        private bool CustomerUserExists(string? userId)
        {
            if (userId == null) return false;

            return _context.Customers.Any(_ => _.userId == userId && _.isActive);
        }

        private async Task<ActionResult<Customer>> Update(Customer customer)
        {
            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!CustomerExists(customer.id)) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }

            return Ok(customer);
        }
    }
}
