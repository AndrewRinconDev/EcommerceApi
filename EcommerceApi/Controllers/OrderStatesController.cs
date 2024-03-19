using EcommerceApi.Context;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatesController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public OrderStatesController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/<OrderStatesController>
        [HttpGet]
        public ActionResult<IEnumerable<OrderState>> Get()
        {
            return Ok(_context.OrderStates.ToList());
        }

        // GET api/<OrderStatesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderState>> Get(string id)
        {
            var orderStateFound = await _context.OrderStates.FindAsync(id);

            if (orderStateFound == null) return NotFound();

            return Ok(orderStateFound);
        }

        // POST api/<OrderStatesController>
        [HttpPost]
        public async Task<ActionResult<OrderState>> Post(OrderState orderState)
        {
            orderState.id = Guid.NewGuid();

            _context.OrderStates.Add(orderState);
            await _context.SaveChangesAsync();

            return Ok(orderState);
        }

        // PUT api/<OrderStatesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderState>> Put(string id, OrderState orderState)
        {
            if (new Guid(id) != orderState.id) return BadRequest();

            return await UpdateOrderState(orderState);
        }

        // DELETE api/<OrderStatesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderState>> Delete(string id)
        {
            var orderStateFound = await _context.OrderStates.FindAsync(id);

            if (orderStateFound == null) return NotFound();

            return await UpdateOrderState(orderStateFound);
        }

        private bool OrderStateExists(Guid? id)
        {
            return _context.OrderStates.Any(e => e.id == id);
        }

        private async Task<ActionResult<OrderState>> UpdateOrderState(OrderState orderState)
        {
            try
            {
                _context.Update(orderState);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!OrderStateExists(orderState.id)) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }

            return Ok(orderState);
        }
    }
}