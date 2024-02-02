using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
    // Get all suppliers
    public async Task<IEnumerable<Supplier>> GetAllSuppliers()
    {
        return await dbSet.ToListAsync();
    }

    // Get supplier by ID
    public async Task<Supplier?> GetSupplierById(int supplierId)
    {
        return await dbSet.FindAsync(supplierId);
    }

    // Add a new supplier
    public async Task<bool> AddSupplier(Supplier supplier)
    {
        await dbSet.AddAsync(supplier);
        return true; 
    }

    // Update an existing supplier
    public async Task<bool> UpdateSupplier(Supplier supplier)
    {
        var existingSupplier = await dbSet.FindAsync(supplier.SupplierId);

        if (existingSupplier != null)
        {
            _context.Entry(existingSupplier).CurrentValues.SetValues(supplier);
            return true; 
        }

        return false; // Supplier not found
    }

    // Remove a supplier by ID
    public async Task<bool> RemoveSupplier(int supplierId)
    {
        var supplierToRemove = await dbSet.FindAsync(supplierId);

        if (supplierToRemove != null)
        {
            dbSet.Remove(supplierToRemove);
            return true; 
        }

        return false; // Supplier not found
    }

}