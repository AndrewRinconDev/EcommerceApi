using EcommerceApi.Models.Database;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/<AddressController>
        [HttpGet]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<Address>>> Get()
        {
            try
            {
                return Ok(await _addressService.GetActiveAddresses());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<Address>> Get(string id)
        {
            var addressFound = await _addressService.GetActiveAddressById(new Guid(id));

            if (addressFound == null) return NotFound();

            return Ok(addressFound);
        }

        // GET api/<AddressController>/customer/5
        [HttpGet("Customer/{customerId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<Address>>> GetByCustomer(string customerId)
        {
            var addressesFound = await _addressService.GetActiveAddressByCustomerId(new Guid(customerId));

            if (addressesFound == null) return NotFound();

            return Ok(addressesFound);
        }

        // POST api/<AddressController>
        [HttpPost]
        [Authorize("BasicWrite")]
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

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<Address>> Put(string id, Address address)
        {
            try
            {
                if (new Guid(id) != address.id) return BadRequest();

                return Ok(await _addressService.Update(address));
            }
            catch (Exception e)
            {
                if (!await _addressService.Exist(address.id)) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        [Authorize("BasicWrite")]
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
