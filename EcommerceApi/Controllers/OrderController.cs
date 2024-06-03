using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IMapper mapper, IOrderService orderService)
        { 
            _orderService = orderService;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            try
            {
                var orders = await _orderService.GetAllActive();
                return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<Order>> GetByIdActive(string id)
        {
            try
            {
                var order = await _orderService.GetByIdActive(new Guid(id));
                if (order == null) return NotFound();

                return Ok(_mapper.Map<OrderDto>(order));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // GET: api/Orders
        [HttpGet("Customer/{customerId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<Order>>> GetByCustomer(string customerId)
        {
            try
            {
                var orders = await _orderService.GetByCustomer(new Guid(customerId));
                return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // POST: api/Orders
        [HttpPost]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<Order>> Post([FromBody] OrderDto orderDto)
        {
            try
            {
                var order = _mapper.Map<Order>(orderDto);
                var orderSaved = await _orderService.Save(order);
                return Ok(_mapper.Map<OrderDto>(order));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<Order>> Put(string id, [FromBody] OrderDto orderDto)
        {
            try
            {
                if (new Guid(id) != orderDto.id) return BadRequest("Id does not match");

                var order = _mapper.Map<Order>(orderDto);
                var orderUpdated = await _orderService.Update(order);
                return Ok(_mapper.Map<OrderDto>(orderUpdated));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<Order>> Delete(string id)
        {
            try
            {
                var order = await _orderService.DeleteOrder(new Guid(id));
                return Ok(_mapper.Map<OrderDto>(order));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}