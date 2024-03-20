using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderRecordController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public OrderRecordController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/<OrderRecordsController>
        [HttpGet]
        public ActionResult<IEnumerable<OrderRecord>> Get()
        {
            return _context.OrderRecords
                .Include(_ => _.order)
                .Include(_ => _.orderState)
                .Where(_ => _.order.isActive)
                .ToList();
        }

        // GET api/<OrderRecordsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRecord>> Get(string id)
        {
            var orderRecordFound = await _context.OrderRecords
                .Include(_ => _.orderState)
                .FirstOrDefaultAsync(_ => _.id == new Guid(id));

            if (orderRecordFound == null) return NotFound();

            return Ok(orderRecordFound);
        }

        // GET api/<OrderRecordsController>/5
        [HttpGet("Order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderRecord>>> GetByOrder(string orderId)
        {
            var orderRecordFound = await _context.OrderRecords
                .Include(_ => _.orderState)
                .Where(_ => _.orderId == new Guid(orderId)).ToListAsync();

            if (orderRecordFound == null) return NotFound();

            return Ok(orderRecordFound);
        }

        // GET: api/<OrderRecordsController>/All
        [HttpGet("All")]
        public ActionResult<IEnumerable<OrderRecord>> GetAll()
        {
            return _context.OrderRecords
                .Include(_ => _.orderState)
                .ToList();
        }

        // POST api/<OrderRecordsController>
        [HttpPost]
        public async Task<ActionResult<OrderRecord>> Post(OrderRecord orderRecord)
        {
            if (!OrderExists(orderRecord.orderId)) return NotFound();

            orderRecord.id = Guid.NewGuid();
            orderRecord.date = DateTime.Now;

            _context.OrderRecords.Add(orderRecord);
            await _context.SaveChangesAsync();

            return Ok(orderRecord);
        }

        // PUT api/<OrderRecordsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderRecord>> Put(string id, OrderRecord orderRecord)
        {
            if (new Guid(id) != orderRecord.id || !OrderExists(orderRecord.orderId)) return BadRequest();

            try
            {
                _context.Update(orderRecord);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderRecordExists(orderRecord.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(orderRecord);
        }

        // DELETE api/<OrderRecordsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            var orderRecordFound = await _context.OrderRecords.FindAsync(id);

            if (orderRecordFound == null) return NotFound();

            _context.OrderRecords.Remove(orderRecordFound);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        private bool OrderExists(Guid? orderId)
        {
            return _context.Orders.Any(_ => _.id == orderId && _.isActive);
        }

        private bool OrderRecordExists(Guid? id)
        {
            return _context.OrderRecords.Any(e => e.id == id);
        }
    }
}