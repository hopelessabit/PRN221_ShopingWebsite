using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Logging;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
