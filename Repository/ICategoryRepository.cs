using MVCWebApplication3.Models;

namespace MVCWebApplication3.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync(int pageNumber, int pageSize);

        Task<Category> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(Category cat);
        Task UpdateCategoryAsync(int Id, Category category);
        Task DeleteCategoryAsync(int id);
        Task ActivateCategoryAsync(int id);
        Task DeactivateCategoryAsync(int id);
        Task<List<Category>> GetActiveCategoriesAsync(int pageNumber, int pageSize);
        Task<List<Category>> GetDeactivatedCategoriesAsync(int pageNumber, int pageSize);
    }
}
