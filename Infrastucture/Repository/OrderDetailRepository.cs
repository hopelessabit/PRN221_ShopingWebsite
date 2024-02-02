using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
    // Get all order details
    public async Task<IEnumerable<OrderDetail>> GetAllOrderDetails()
    {
        return await dbSet.ToListAsync();
    }

    // Get order detail by composite key (OrderId and ProductId)
    public async Task<OrderDetail?> GetOrderDetailById(int orderId, int productId)
    {
        return await dbSet.FindAsync(orderId, productId);
    }

    // Add a new order detail
    public async Task<bool> AddOrderDetail(OrderDetail orderDetail)
    {
        await dbSet.AddAsync(orderDetail);
        return true; 
    }

    // Update an existing order detail
    public async Task<bool> UpdateOrderDetail(OrderDetail orderDetail)
    {
        var existingOrderDetail = await dbSet.FindAsync(orderDetail.OrderId, orderDetail.ProductId);

        if (existingOrderDetail != null)
        {
            _context.Entry(existingOrderDetail).CurrentValues.SetValues(orderDetail);
            return true; 
        }

        return false; // Order detail not found
    }

    // Remove an order detail by composite key (OrderId and ProductId)
    public async Task<bool> RemoveOrderDetail(int orderId, int productId)
    {
        var orderDetailToRemove = await dbSet.FindAsync(orderId, productId);

        if (orderDetailToRemove != null)
        {
            dbSet.Remove(orderDetailToRemove);
            return true; 
        }

        return false; // Order detail not found
    }

}
