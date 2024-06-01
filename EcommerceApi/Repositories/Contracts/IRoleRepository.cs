using EcommerceApi.Models.Database;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        public Task<IEnumerable<Role>> GetAllIncludePermissions();

        public Task<Role?> GetIncludePermissions(Guid id);
    }
}
