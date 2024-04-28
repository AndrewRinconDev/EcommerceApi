using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface ICategoryService : IBaseService<Category>
    {
        public Task<IEnumerable<Category>> GetActiveCategories();

        public Task<Category?> GetActiveCategoryById(Guid id);

        public Task<ICollection<Category>> GetSubcategoriesByParent(Guid? parentId);

        public Task<Category> SaveCategory(Category Category);

        public Task<Category?> DeleteCategory(Guid id);
    }
}
