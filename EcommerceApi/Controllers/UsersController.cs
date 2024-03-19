using AutoMapper;
using EcommerceApi.Context;
using EcommerceApi.Dto;
using EcommerceApi.Helpers;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        private readonly IConfiguration _configuration;
        private CryptographyHelper cryptographyHelper;

        public UsersController(EcommerceDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
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
                .FirstOrDefaultAsync(u => u.id == new Guid(id));

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
            user.id = Guid.NewGuid();
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
            if (new Guid(id) != user.id) return BadRequest();

            var oldUser = _context.Users.FirstOrDefault(u => u.id == new Guid(id));
            if (oldUser == null) return NotFound();

            if (user.password != oldUser.password)
            {
                user.password = cryptographyHelper.Encrypt(user.password);
            }

            return await Update(user);
        }
        // PUT api/<UsersController>/Reactive/5
        [HttpPut("Reactive/{id}")]
        public async Task<ActionResult<User>> Reactive(string id, User user)
        {
            if (new Guid(id) != user.id) return BadRequest();

            return await UpdateUserActive(user, true);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return await UpdateUserActive(user, false);
        }

        private bool UserExists(Guid? id)
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

        private async Task<ActionResult<User>> UpdateUserActive(User user, bool isActive = false)
        {
            user.isActive = isActive;
            var userCustomer = await _context.Customers
                .Include(_ => _.addresses)
                .Include(_ => _.favoriteProducts)
                .FirstOrDefaultAsync(_ => _.userId == user.id);

            if (userCustomer != null)
            {
                //CustomersController customersController = new CustomersController(_context, _mapper, _configuration);
                //await customersController.UpdateCustomerActive(userCustomer, isActive);
            }

            return await Update(user);
        }
    }
}
