using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public OrderProductController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> Get()
        {
            return await _context.OrderProducts
                .Include(_ => _.product)
                .Include(_ => _.order)
                .Where(_ => _.order.isActive)
                .ToListAsync();
        }

        // GET: api/OrderProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProduct>> Get(string id)
        {
            var orderProduct = await _context.OrderProducts
                .Include(_ => _.product)
                .FirstOrDefaultAsync(_ => _.id == new Guid(id));

            if (orderProduct == null)
            {
                return NotFound();
            }

            return orderProduct;
        }

        // GET: api/OrderProducts/5
        [HttpGet("Order/{id}")]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetByOrder(string orderId)
        {
            var orderProduct = await _context.OrderProducts
                .Where(_ => _.orderId == new Guid(orderId))
                .Include(_ => _.product)
                .ToListAsync();

            if (orderProduct == null)
            {
                return NotFound();
            }

            return Ok(orderProduct);
        }

        // GET: api/OrderProducts
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetAll()
        {
            return await _context.OrderProducts
                .Include(_ => _.product)
                .ToListAsync();
        }

        // POST: api/OrderProducts
        [HttpPost]
        public async Task<ActionResult<OrderProduct>> Post(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderProduct", new { id = orderProduct.id }, orderProduct);
        }

        // PUT: api/OrderProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderProduct orderProduct)
        {
            if (new Guid(id) != orderProduct.id) return BadRequest();

            try
            {
                _context.Update(orderProduct);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!OrderProductExists(new Guid(id)))
                {
                    return NotFound();
                }

                Console.Error.WriteLine(e);
                return BadRequest();
            }

            return Ok(orderProduct);
        }

        // DELETE: api/OrderProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderProduct>> Delete(string id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        private bool OrderProductExists(Guid id)
        {
            return _context.OrderProducts.Any(_ => _.id == id);
        }
    }
}