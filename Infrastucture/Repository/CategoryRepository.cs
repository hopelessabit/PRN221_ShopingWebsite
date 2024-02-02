using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
    // Get all categories
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await dbSet.ToListAsync();
    }

    // Get a category by ID
    public async Task<Category?> GetCategoryById(Guid id)
    {
        return await dbSet.FindAsync(id);
    }

    // Add a new category
    public async Task<bool> AddCategory(Category category)
    {
        await dbSet.AddAsync(category);
        return true; 
    }

    // Update an existing category
    public async Task<bool> UpdateCategory(Category category)
    {
        var existingCategory = await dbSet.FindAsync(category.CategoryId);

        if (existingCategory != null)
        {
            _context.Entry(existingCategory).CurrentValues.SetValues(category);
            return true; 
        }

        return false; // Category not found
    }

    // Remove a category by ID
    public async Task<bool> RemoveCategory(Guid id)
    {
        var categoryToRemove = await dbSet.FindAsync(id);

        if (categoryToRemove != null)
        {
            dbSet.Remove(categoryToRemove);
            return true; 
        }

        return false; // Category not found
    }
}
