using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
    

    // Get all customers
    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await dbSet.ToListAsync();
    }

    // Get a customer by ID
    public async Task<Customer?> GetCustomerById(Guid id)
    {
        return await dbSet.FindAsync(id);
    }

    // Add a new customer
    public async Task<bool> AddCustomer(Customer customer)
    {
        await dbSet.AddAsync(customer);
        return true; 
    }

    // Update an existing customer
    public async Task<bool> UpdateCustomer(Customer customer)
    {
        var existingCustomer = await dbSet.FindAsync(customer.CustomerId);

        if (existingCustomer != null)
        {
            _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
            return true; 
        }

        return false; // Customer not found
    }

    // Remove a customer by ID
    public async Task<bool> RemoveCustomer(Guid id)
    {
        var customerToRemove = await dbSet.FindAsync(id);

        if (customerToRemove != null)
        {
            dbSet.Remove(customerToRemove);
            return true; 
        }

        return false; // Customer not found
    }

    // You may add other specific methods for the Customer entity if needed.

}
