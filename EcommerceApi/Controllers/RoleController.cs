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
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IMapper mapper, IRoleService roleService)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        // GET api/<Roles>/GetAllIncludePermissions
        [HttpGet("GetAllIncludePermissions")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllIncludePermissions()
        {
            try
            {
                IEnumerable<Role> roles = await _roleService.GetAllIncludePermissions();
                return Ok(_mapper.Map<IEnumerable<Role>, IEnumerable<RoleDto>>(roles));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // GET api/<Roles>/GetAllIncludePermissions
        [HttpGet("GetIncludePermissions/{id}")]
        [Authorize("BasicRead")]
        public async Task<ActionResult<RoleDto?>> GetIncludePermissions(string id)
        {
            try
            {
            return Ok(await _roleService.GetIncludePermissions(new Guid(id)));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // POST api/<Roles>
        [HttpPost]
        [Authorize("SuperAdmin")]
        public async Task<ActionResult<RoleDto>> Post(RoleDto roleDto)
        {
            try
            {
                return Ok(await _roleService.SaveRole(roleDto));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // PUT api/<Roles>/5
        [HttpPut("{id}")]
        [Authorize("SuperAdmin")]
        public async Task<ActionResult<RoleDto>> Put(string id, RoleDto roleDto)
        {
            try
            {
                if (new Guid(id) != roleDto.id) return BadRequest();

                return Ok(await _roleService.UpdateRole(new Guid(id), roleDto));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE api/<Roles>/5
        [HttpDelete("{id}")]
        [Authorize("SuperAdmin")]
        public async Task<ActionResult<RoleDto>> Delete(string id)
        {
            try
            {
                Role role = await _roleService.DeleteRole(new Guid(id));
                return Ok(_mapper.Map<Role, RoleDto>(role));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return BadRequest();
            }
        }

        // GET api/<Roles>/GetPermissionsConst
        [HttpGet("GetPermissionsConst")]
        [Authorize("SuperAdmin")]
        public ActionResult<List<string>> GetPermissionsConst()
        {
            return Ok(_roleService.getPermissionsConst());
        }
    }
}
