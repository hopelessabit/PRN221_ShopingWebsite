using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
    // Get all orders
    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        return await dbSet.ToListAsync();
    }

    // Get order by ID
    public async Task<Order?> GetOrderById(int orderId)
    {
        return await dbSet.FindAsync(orderId);
    }

    // Add a new order
    public async Task<bool> AddOrder(Order order)
    {
        await dbSet.AddAsync(order);
        return true;
    }

    // Update an existing order
    public async Task<bool> UpdateOrder(Order order)
    {
        var existingOrder = await dbSet.FindAsync(order.OrderId);

        if (existingOrder != null)
        {
            _context.Entry(existingOrder).CurrentValues.SetValues(order);
            return true;
        }

        return false; // Order not found
    }

    // Remove an order by ID
    public async Task<bool> RemoveOrder(int orderId)
    {
        var orderToRemove = await dbSet.FindAsync(orderId);

        if (orderToRemove != null)
        {
            dbSet.Remove(orderToRemove);
            return true;
        }

        return false; // Order not found
    }

}
