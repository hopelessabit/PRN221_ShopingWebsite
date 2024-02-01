using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Logging;

public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
