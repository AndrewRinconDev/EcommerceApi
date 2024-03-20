using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public OrderController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await _context.Orders
                .Where(_ => _.isActive)
                .Include(_ => _.customer)
                //TODO: .ThenInclude(_ => _.user)
                //.ThenInclude(_ => _.user)
                .Include(_ => _.orderProducts)
                //.ThenInclude(_ => _.product)
                .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(string id)
        {
            var order = _context.Orders
                .Include(_ => _.address)
                .Include(_ => _.customer)
                    .ThenInclude(_ => _.user)
                .Include(_ => _.orderProducts)
                    .ThenInclude(_ => _.product)
            .FirstOrDefaultAsync(_ => _.id == new Guid(id));

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // GET: api/Orders
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return await _context.Orders
                .Include(_ => _.customer)
                    .ThenInclude(_ => _.user)
                .Include(_ => _.orderProducts)
                    .ThenInclude(_ => _.product)
                .Include(_ => _.address)
                .ToListAsync();
        }

        // GET: api/Orders
        [HttpGet("Customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetByCustomer(string customerId)
        {
            return await _context.Orders
                .Where(_ => _.customerId == new Guid(customerId) && _.isActive)
                .Include(_ => _.customer)
                    .ThenInclude(_ => _.user)
                .Include(_ => _.orderProducts)
                    .ThenInclude(_ => _.product)
                .Include(_ => _.address)
                .ToListAsync();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            order.id = Guid.NewGuid();
            order.creationDates = DateTime.Now;
            order.dateUpdate = DateTime.Now;
            order.isActive = true;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = order.id }, order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Put(string id, Order order)
        {
            if (new Guid(id) != order.id) return BadRequest();

            order.dateUpdate = DateTime.Now;
            return await UpdateOrder(order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> Delete(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return await UpdateOrderActive(order, false);
        }

        private async Task<ActionResult<Order>> UpdateOrder(Order order)
        {
            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!OrderExists(order.id)) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }

            return Ok(order);
        }

        private async Task<ActionResult<Order>> UpdateOrderActive(Order order, bool isActive)
        {
            order.isActive = isActive;
            return await UpdateOrder(order);
        }

        private bool OrderExists(Guid? id)
        {
            return _context.Orders.Any(_ => _.id == id);
        }
    }
}