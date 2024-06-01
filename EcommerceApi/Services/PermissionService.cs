using EcommerceApi.Models.Contants;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class PermissionService : BaseService<Permission>, IPermissionService
    {
        IPermissionRepository _PermissionRepository;
        public PermissionService(IBaseRepository<Permission> repository) : base(repository) {
            _PermissionRepository = (IPermissionRepository)repository;
        }
        
        public async Task<IEnumerable<Permission>> GetByRole(Guid roleId)
        {
            return await _PermissionRepository.GetByRole(roleId);
        }

        public List<string> getPermissionsConst()
        {
            return PermissionsContant.names;
        }

        public async Task SaveAllPermission(Guid roleId, List<string>? newPermissions)
        {
            if (newPermissions == null) return;

            foreach (var permission in newPermissions)
            {
                var permissionEntity = new Permission
                {
                    roleId = roleId,
                    name = permission
                };
                await Save(permissionEntity);
            }
        }

        public async Task UpdateAllPermission(Guid roleId, List<string>? newPermissions)
        {
            if (newPermissions == null) return;

            IEnumerable<Permission> oldPermissions = await GetByRole(roleId);

            var oldPermissionsFiltered = oldPermissions.Except(oldPermissions.Where(_ => newPermissions.Contains(_.name)));
            foreach (var permission in oldPermissionsFiltered)
            {
                await DeleteById(permission.id);
            }

            var newPermissionsFiltered = newPermissions.Except(newPermissions.Where(_ => oldPermissions.Select(_ => _.name).Contains(_)));
            foreach (var permission in newPermissionsFiltered)
            {
                var permissionEntity = new Permission
                {
                    roleId = roleId,
                    name = permission
                };
                await Save(permissionEntity);
            }
        }
    }
}
