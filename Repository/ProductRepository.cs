using Microsoft.EntityFrameworkCore;
using MVCWebApplication3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApplication3.Repository
{
    public class ProductRepository : IProductRepository
    {
            private readonly ProDbContext _context;



            public ProductRepository(ProDbContext context)
            {
                _context = context;
            }

            public async Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize)
            {
                return await _context.Products
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                    .ToListAsync();
            }

            public async Task<Product?> GetProductByIdAsync(int id)
            {
                return await _context.Products
                            .Where(x => x.Id == id)
                            .FirstOrDefaultAsync(); // or use FindAsync(id);
            }

            public async Task AddProductAsync(Product product)
            {


                _context.Products.Add(product);
                await _context.SaveChangesAsync();

            }

            public async Task UpdateProductAsync(int Id, Product P)
            {
                var EProduct = await _context.Products.FindAsync(Id);


                if (EProduct == null)
                {
                    throw new ArgumentException("Category not found");
                }
                else
                {
                    // Update category properties
                    EProduct.Name = P.Name;
                    EProduct.IsActive = P.IsActive;
                    EProduct.CategoryId = P.CategoryId;
                }


                await _context.SaveChangesAsync();
            }



            public async Task DeleteProductAsync(int id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task ActivateProductAsync(int id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    product.IsActive = true;
                    await _context.SaveChangesAsync();
                }
            }

            public async Task DeactivateProductAsync(int id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    product.IsActive = false;
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<List<Product>> GetAllActivateProductAsync(int pageNumber, int pageSize)
            {
                return await _context.Products
                                     .Where(c => c.IsActive)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();
            }

            public async Task<List<Product>> GetAllDeactiveProAsync(int pageNumber, int pageSize)
            {
                return await _context.Products
                                     .Where(c => !c.IsActive)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();
            }
        }
    }






