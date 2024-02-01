using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Logging;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
