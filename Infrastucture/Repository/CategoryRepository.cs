using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Logging;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
