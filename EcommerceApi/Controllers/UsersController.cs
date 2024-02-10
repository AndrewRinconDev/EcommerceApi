using AutoMapper;
using EcommerceApi.Context;
using EcommerceApi.Dto;
using EcommerceApi.Helpers;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;
        private CryptographyHelper cryptographyHelper;

        public UsersController(EcommerceDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            cryptographyHelper = new CryptographyHelper(configuration);
        }

        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                return Ok(_context.Users.Include(_ => _.role).ToList());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            try
            {
                var userFound = await _context.Users.Include(_ => _.role)
                .FirstOrDefaultAsync(u => u.id == id);

                if (userFound == null) return NotFound();

                return Ok(userFound);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> Post(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.id = Guid.NewGuid().ToString();
            user.password = cryptographyHelper.Encrypt(user.password);
            user.isActive = true;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = user.id }, user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(string id, UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            if (id != user.id) return BadRequest();

            var oldUser = _context.Users.FirstOrDefault(u => u.id == id);
            if (oldUser == null) return NotFound();

            if (user.password != oldUser.password)
            {
                user.password = cryptographyHelper.Encrypt(user.password);
            }

            return await Update(user);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            user.isActive = false;
            var userCustomer = await _context.Customers.FirstOrDefaultAsync(_ => _.userId == id);
            if (userCustomer != null)
            {
                userCustomer.isActive = false;
                _context.Update(userCustomer);
            }

            return await Update(user);
        }

        private bool UserExists(string? id)
        {
            if(id == null) return false;

            return _context.Users.Any(e => e.id == id);
        }

        private async Task<ActionResult<User>> Update(User user)
        {
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!UserExists(user.id)) return NotFound();

                Console.Error.WriteLine(e);
                return BadRequest();
            }

            return Ok(user);
        }
    }
}
