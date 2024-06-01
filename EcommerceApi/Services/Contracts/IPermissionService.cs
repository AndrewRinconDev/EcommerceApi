using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface IPermissionService : IBaseService<Permission>
    {
        public Task<IEnumerable<Permission>> GetByRole(Guid roleId);

        public Task SaveAllPermission(Guid roleId, List<string>? newPermissions);

        public Task UpdateAllPermission(Guid roleId, List<string>? newPermissions);

        public List<string> getPermissionsConst();
    }
}
