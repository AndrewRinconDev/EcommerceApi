using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(EcommerceDbContext context) : base(context) {}


        public async Task<IEnumerable<Permission>> GetByRole(Guid roleId)
        {
            return await _context.Set<Permission>()
                .Where(_ => _.roleId == roleId)
                .ToListAsync();
        }
    }
}
