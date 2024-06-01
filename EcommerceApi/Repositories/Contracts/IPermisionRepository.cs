using EcommerceApi.Models.Database;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IPermissionRepository : IBaseRepository<Permission>
    {
        public Task<IEnumerable<Permission>> GetByRole(Guid roleId);
    }
}
