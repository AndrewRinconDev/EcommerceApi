using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;

        public RoleService(IBaseRepository<Role> repository, IPermissionService permissionService, IMapper mapper) : base(repository) {
            _roleRepository = (IRoleRepository)repository;
            _permissionService = permissionService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Role>> GetAllIncludePermissions()
        {
            return await _roleRepository.GetAllIncludePermissions();
        }

        public async Task<RoleDto?> GetIncludePermissions(Guid id)
        {
            var role = await _roleRepository.GetIncludePermissions(id);
            if (role == null) return null;

            RoleDto roleDto = _mapper.Map<Role, RoleDto>(role);

            roleDto.permissionList = role.permissions.Select(_ => _.name).ToList();
            return roleDto;
        }

        public async Task<RoleDto> SaveRole(RoleDto roleDto)
        {
            Role role = _mapper.Map<Role>(roleDto);

            Role roleSaved = await _roleRepository.Save(role);
            await _permissionService.SaveAllPermission(roleSaved.id, roleDto.permissionList);
            
            roleDto.id = roleSaved.id;
            return roleDto;
        }
        
        public async Task<RoleDto> UpdateRole(Guid id, RoleDto roleDto)
        {
            Role role = _mapper.Map<Role>(roleDto);
            await _roleRepository.Update(role);
            await _permissionService.UpdateAllPermission(id, roleDto.permissionList);
            return roleDto;
        }

        public async Task<Role> DeleteRole(Guid id)
        {
            Role? role = await _roleRepository.GetById(id);

            if (role == null) throw new Exception("Role not found");

            role.isActive = false;
            return await _roleRepository.Update(role);
        }

        public List<string> getPermissionsConst() {
            return _permissionService.getPermissionsConst();
        }
    }
}
