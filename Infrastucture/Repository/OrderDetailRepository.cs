using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Logging;

public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
