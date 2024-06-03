using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        ICategoryRepository _CategoryRepository;
        public CategoryService(ICategoryRepository repository) : base(repository) {
            _CategoryRepository = repository;
        }

        public async Task<IEnumerable<Category>> GetActiveCategories()
        {
            return await _CategoryRepository.GetActiveCategories();
        }
        
        public async Task<Category?> GetActiveCategoryById(Guid id)
        {
            return await _CategoryRepository.GetActiveCategoryById(id);
        }

        public async Task<ICollection<Category>> GetSubcategoriesByParent(Guid? parentId)
        {
            return await _CategoryRepository.GetSubcategoriesByParent(parentId);
        }

        public async Task<Category> SaveCategory(Category category)
        {
            category.isActive = true;

            return await _CategoryRepository.Save(category);
        }

        public async Task<Category?> DeleteCategory(Guid id)
        {
            var category = await GetById(id);
            if (category == null) return null;

            category.isActive = false;
            return await Update(category);
        }
    }
}
