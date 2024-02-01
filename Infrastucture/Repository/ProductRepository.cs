using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Logging;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
