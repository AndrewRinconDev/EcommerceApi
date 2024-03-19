using EcommerceApi.Context;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public RolesController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/<RolesController>
        [HttpGet]
        public ActionResult<IEnumerable<Role>> Get()
        {
            return Ok(_context.Roles.ToList());
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Get(string id)
        {
            var roleFound = await _context.Roles.FindAsync(id);

            if(roleFound == null) return NotFound();

            return Ok(roleFound);
        }

        // POST api/<RolesController>
        [HttpPost]
        public async Task<ActionResult<Role>> Post(Role role)
        {
            role.id = Guid.NewGuid();
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = role.id }, role);
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> Put(string id, Role role)
        {
            if (new Guid(id) != role.id) return BadRequest();

            try
            {
                _context.Update(role);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!RoleExists(new Guid(id))) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }

            return Ok(role);
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            var role = _context.Roles.Find(id);
            if(role == null) return NotFound();

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        private bool RoleExists(Guid? id)
        {
            return _context.Roles.Any(e => e.id == id);
        }
    }
}
