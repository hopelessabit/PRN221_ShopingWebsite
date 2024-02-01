using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Logging;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
