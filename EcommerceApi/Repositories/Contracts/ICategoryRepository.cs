using EcommerceApi.Models.Database;

namespace EcommerceApi.Repositories.Contracts
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public Task<IEnumerable<Category>> GetActiveCategories();
        public Task<Category?> GetActiveCategoryById(Guid id);
        public Task<ICollection<Category>> GetSubcategoriesByParent(Guid? parentId);
        public bool CategoryExists(Guid? id);
    }
}
