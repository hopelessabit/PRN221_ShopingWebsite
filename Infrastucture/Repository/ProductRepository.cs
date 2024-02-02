using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
    // Get all products
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await dbSet.ToListAsync();
    }

    // Get product by ID
    public async Task<Product?> GetProductById(int productId)
    {
        return await dbSet.FindAsync(productId);
    }

    // Add a new product
    public async Task<bool> AddProduct(Product product)
    {
        await dbSet.AddAsync(product);
        return true; 
    }

    // Update an existing product
    public async Task<bool> UpdateProduct(Product product)
    {
        var existingProduct = await dbSet.FindAsync(product.ProductId);

        if (existingProduct != null)
        {
            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            return true; 
        }

        return false; // Product not found
    }

    // Remove a product by ID
    public async Task<bool> RemoveProduct(int productId)
    {
        var productToRemove = await dbSet.FindAsync(productId);

        if (productToRemove != null)
        {
            dbSet.Remove(productToRemove);
            return true; 
        }

        return false; // Product not found
    }
}
