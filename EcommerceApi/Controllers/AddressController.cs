using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly EcommerceDbContext _context;
        private readonly AddressService _addressService;

        public AddressController(EcommerceDbContext context, AddressService addressService)
        {
            _context = context;
            _addressService = addressService;
        }

        // GET: api/<AddressesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> Get()
        {
            try
            {
                return Ok(await _addressService.GetActiveAddresses());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<AddressesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> Get(string id)
        {
            var addressFound = await _addressService.GetActiveAddressById(new Guid(id));

            if (addressFound == null) return NotFound();

            return Ok(addressFound);
        }

        // GET api/<AddressesController>/customer/5
        [HttpGet("Customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetByCustomer(string customerId)
        {
            var addressesFound = await _addressService.GetActiveAddressByCustomerId(new Guid(customerId));

            if (addressesFound == null) return NotFound();

            return Ok(addressesFound);
        }

        // POST api/<AddressesController>
        [HttpPost]
        public async Task<ActionResult<Address>> Post(Address address)
        {
            try
            {
                return Ok(await _addressService.SaveAddress(address));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // PUT api/<AddressesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> Put(string id, Address address)
        {
            try
            {
                if (new Guid(id) != address.id) return BadRequest();

                return Ok(await _addressService.UpdateAddress(address));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE api/<AddressesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> Delete(string id)
        {
            try
            {
                var address = await _addressService.DeleteAddress(new Guid(id));
                if (address == null) return NotFound();

                return Ok(address);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }
    }
}
