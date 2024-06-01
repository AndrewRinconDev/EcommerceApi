using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(EcommerceDbContext context) : base(context) {}

        public async Task<IEnumerable<Role>> GetAllIncludePermissions()
        {
            return await _context.Set<Role>()
                .Where(_ => _.isActive == true)
                .Include(_ => _.permissions)
                .ToListAsync();
        }

        public async Task<Role?> GetIncludePermissions(Guid id)
        {
            return await _context.Set<Role>()
                .Where(_ => _.id == id)
                .Include(_ => _.permissions)
                .FirstOrDefaultAsync();
        }
    }
}
