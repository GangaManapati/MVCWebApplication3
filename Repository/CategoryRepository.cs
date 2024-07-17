using Microsoft.EntityFrameworkCore;
using MVCWebApplication3.Models;

namespace MVCWebApplication3.Repository
{
        public class CategoryRepository : ICategoryRepository
        {
            private readonly ProDbContext _context;

            public CategoryRepository(ProDbContext context)
            {
                _context = context;
            }

            public async Task<List<Category>> GetCategoriesAsync(int pageNumber, int pageSize)
            {
                return await _context.Categories
                    .Include(c => c.Products) // Eager loading products ,for getting the Related data also
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            public async Task<Category?> GetCategoryByIdAsync(int id)
            {
                return await _context.Categories
                            .Include(c => c.Products)
                            .Where(x => x.Id == id)
                            .FirstOrDefaultAsync();// .FirstOrDefaultAsync() param empty ite where condition vadali .Where(x=>x.Id==id)
            }

            public async Task CreateCategoryAsync(Category cat)
            {
                _context.Categories.Add(cat);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateCategoryAsync(int Id, Category category)
            {
                var existingCategory = await _context.Categories.FindAsync(Id);


                if (existingCategory == null)
                {
                    throw new ArgumentException("Category not found");
                }
                else
                {
                    // Update category properties
                    existingCategory.Name = category.Name;
                    existingCategory.IsActive = category.IsActive;
                }


                await _context.SaveChangesAsync();
            }

            public async Task DeleteCategoryAsync(int id)
            {
                var category = await _context.Categories.FindAsync(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task ActivateCategoryAsync(int id)
            {
                var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
                if (category != null)
                {
                    category.IsActive = true;
                    foreach (var product in category.Products)
                    {
                        product.IsActive = true;
                    }
                    await _context.SaveChangesAsync();
                }
            }

            public async Task DeactivateCategoryAsync(int id)
            {
                var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
                if (category != null)
                {
                    category.IsActive = false;
                    foreach (var product in category.Products)
                    {
                        product.IsActive = false;
                    }
                    await _context.SaveChangesAsync();
                }
            }
            public async Task<List<Category>> GetActiveCategoriesAsync(int pageNumber, int pageSize)
            {
                return await _context.Categories
                                     .Include(c => c.Products)
                                     .Where(x => x.IsActive)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();
            }

            public async Task<List<Category>> GetDeactivatedCategoriesAsync(int pageNumber, int pageSize)
            {
                return await _context.Categories
                                     .Include(c => c.Products)
                                     .Where(c => !c.IsActive)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();
            }
        }
    }
