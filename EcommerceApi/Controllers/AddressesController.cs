using EcommerceApi.Context;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Loader;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public AddressesController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/<AddressesController>
        [HttpGet]
        public ActionResult<IEnumerable<Address>> Get()
        {
            try
            {

                return Ok(_context.Addresses.Where(_ => _.isActive == true).ToList());
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
            var addressFound = await _context.Addresses
                .FirstOrDefaultAsync(_ => _.id == id && _.isActive == true);

            if (addressFound == null) return NotFound();

            return Ok(addressFound);
        }

        // GET api/<AddressesController>/customer/5
        [HttpGet("Customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetByCustomer(string customerId)
        {
            var addressesFound = await _context.Addresses
                .Where(_ => _.customerId == customerId && _.isActive == true).ToListAsync();

            if (addressesFound == null) return NotFound();

            return Ok(addressesFound);
        }

        // POST api/<AddressesController>
        [HttpPost]
        public async Task<ActionResult<Address>> Post(Address address)
        {
            address.id = Guid.NewGuid().ToString();
            address.isActive = true;

            _context.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = address.id }, address);
        }

        // PUT api/<AddressesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> Put(string id, Address address)
        {
            if (id != address.id) return BadRequest();

            return await Update(address);
        }

        // DELETE api/<AddressesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> Delete(string id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return NotFound();

            address.isActive = false;
            return await Update(address);
        }

        private async Task<ActionResult<Address>> Update(Address address)
        {
            try
            {
                _context.Update(address);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!AddressExists(address.id)) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }

            return Ok(address);
        }

        private bool AddressExists(string id)
        {
            return _context.Addresses.Any(e => e.id == id);
        }
    }
}
