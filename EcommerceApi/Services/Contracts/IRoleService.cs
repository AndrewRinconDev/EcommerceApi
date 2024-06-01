using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;

namespace EcommerceApi.Services.Contracts
{
    public interface IRoleService : IBaseService<Role>
    {
        public Task<IEnumerable<Role>> GetAllIncludePermissions();

        public Task<RoleDto?> GetIncludePermissions(Guid id);

        public Task<RoleDto> SaveRole(RoleDto roleDto);

        public Task<RoleDto> UpdateRole(Guid id, RoleDto roleDto);

        public Task<Role> DeleteRole(Guid id);

        public List<string> getPermissionsConst();
    }
}
