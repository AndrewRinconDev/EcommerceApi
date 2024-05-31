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
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                return Ok(await _userService.GetAll());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // GET: api/<UsersController>
        [HttpGet("Role/{roleId}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<User>>> GetByRole(string roleId)
        {
            try
            {
                return Ok(await _userService.GetByRole(new Guid(roleId)));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<User>> Get(string id)
        {
            try
            {
                var userFound = await _userService.GetById(new Guid(id));

                if (userFound == null) return NotFound();

                return Ok(userFound);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<User>> Post(UserDto userDto)
        {
            try
            {
                return await _userService.SaveUser(userDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<UsersController>/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto?>> Login(UserLoginDto userLoginDto)
        {
            try
            {
                return await _userService.Login(userLoginDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<UsersController>/ChangePassword
        /// <summary>
        /// Password test Test123*
        /// </summary>
        /// <param name="userChangePasswordDto"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<User>> ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            try
            {
                return await _userService.ChangePassword(userChangePasswordDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        [Authorize("AdminWrite")]
        public async Task<ActionResult<User>> Put(string id, UserDto userDto)
        {
            try
            {
                if (new Guid(id) != userDto.id) return BadRequest();

                return await _userService.UpdateUser(userDto);
            }
            catch (Exception e)
            {
                if (!await _userService.Exist(new Guid(id))) return NotFound();

                return BadRequest(e.Message);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        [Authorize("AdminWrite")]
        public async Task<ActionResult<User?>> Delete(string id)
        {
            try
            {
                return await _userService.DeleteUser(new Guid(id));
            }
            catch (Exception e)
            {
                if (!await _userService.Exist(new Guid(id))) return NotFound();

                return BadRequest(e.Message);
            }
        }
    }
}
