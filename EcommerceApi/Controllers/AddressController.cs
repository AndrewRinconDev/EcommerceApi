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
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        // GET: api/<AddressController>
        [HttpGet]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> Get()
        {
            try
            {
                var addresses = await _addressService.GetActiveAddresses();
                return Ok(_mapper.Map<IEnumerable<AddressDto>>(addresses));
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
        public async Task<ActionResult<AddressDto>> Get(string id)
        {
            try
            {
                var address = await _addressService.GetActiveAddressById(new Guid(id));

                return Ok(_mapper.Map<AddressDto>(address));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // GET api/<AddressController>/customer/5
        [HttpGet("Customer/{customerId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetByCustomer(string customerId)
        {
            try
            {
                var addressesFound = await _addressService.GetActiveAddressByCustomerId(new Guid(customerId));

                return Ok(_mapper.Map<IEnumerable<AddressDto>>(addressesFound));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // POST api/<AddressController>
        [HttpPost]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<AddressDto>> Post(AddressDto addressDto)
        {
            try
            {
                var address = _mapper.Map<Address>(addressDto);
                var addressSaved = await _addressService.SaveAddress(address);
                return Ok(_mapper.Map<AddressDto>(addressSaved));
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
        public async Task<ActionResult<AddressDto>> Put(string id, AddressDto addressDto)
        {
            try
            {
                if (new Guid(id) != addressDto.id) return BadRequest();

                var address = _mapper.Map<Address>(addressDto);
                var addressUpdated = await _addressService.Update(address);
                return Ok(_mapper.Map<AddressDto>(addressUpdated));
            }
            catch (Exception e)
            {
                if (!await _addressService.Exist(new Guid(id))) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<AddressDto>> Delete(string id)
        {
            try
            {
                var addressDeleted = await _addressService.DeleteAddress(new Guid(id));

                return Ok(_mapper.Map<AddressDto>(addressDeleted));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }
    }
}
