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
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            try
            {
                IEnumerable<User> users = await _userService.GetAll();
                return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users));
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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByRole(string roleId)
        {
            try
            {
                IEnumerable<User> users = await _userService.GetByRole(new Guid(roleId));
                return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users));
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
        public async Task<ActionResult<UserDto>> Get(string id)
        {
            try
            {
                var userFound = await _userService.GetById(new Guid(id));

                if (userFound == null) return NotFound();

                return Ok(_mapper.Map<User, UserDto>(userFound));
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
                User userSaved = await _userService.SaveUser(userDto);
                return Ok(_mapper.Map<User, UserDto>(userSaved));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Email test and@email.com
        /// Password test Test123*
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        // POST api/<UsersController>/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserLoggedDto?>> Login(UserLoginDto userLoginDto)
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

        [HttpPost("ChangePassword")]
        [Authorize("BasicWrite")]
        public async Task<ActionResult<UserDto>> ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            try
            {
                User userUpdated = await _userService.ChangePassword(userChangePasswordDto);
                return Ok(_mapper.Map<User, UserDto>(userUpdated));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        [Authorize("AdminWrite")]
        public async Task<ActionResult<UserDto>> Put(string id, UserDto userDto)
        {
            try
            {
                if (new Guid(id) != userDto.id) return BadRequest();

                User userUpdated = await _userService.UpdateUser(userDto);
                return Ok(_mapper.Map<User, UserDto>(userUpdated));
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
        public async Task<ActionResult<UserDto?>> Delete(string id)
        {
            try
            {
                User? userDeleted = await _userService.DeleteUser(new Guid(id));
                return Ok(_mapper.Map<User, UserDto>(userDeleted));
            }
            catch (Exception e)
            {
                if (!await _userService.Exist(new Guid(id))) return NotFound();

                return BadRequest(e.Message);
            }
        }
    }
}
