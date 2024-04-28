using EcommerceApi.Context;
using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EcommerceDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetActiveCategories()
        {
            return await _context.Set<Category>()
                .Include(_ => _.subcategories!.Where(sc1 => sc1.isActive == true))
                    .ThenInclude(_ => _.subcategories!.Where(sc1 => sc1.isActive == true))
                .Where(_ => _.parentId == null && _.isActive == true)
                .ToListAsync();
        }

        public async Task<Category?> GetActiveCategoryById(Guid id)
        {
            return await _context.Set<Category>().Include(_ => _.subcategories)
                .FirstOrDefaultAsync(_ => _.id == id && _.isActive == true);
        }

        public async Task<ICollection<Category>> GetSubcategoriesByParent(Guid? parentId)
        {
            return await _context.Set<Category>()
                .Include(_ => _.subcategories!.Where(sc1 => sc1.isActive == true))
                    .ThenInclude(_ => _.subcategories!.Where(sc1 => sc1.isActive == true))
                .Where(_ => _.parentId == parentId && _.isActive == true)
                .ToListAsync();
        }

        public bool CategoryExists(Guid? id)
        {
            return _context.Set<Category>().Any(e => e.id == id);
        }
    }
}
