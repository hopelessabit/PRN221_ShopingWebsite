using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(PMSDbContext context, ILogger logger) : base(context, logger)
    {

    }

    // Get all accounts
    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        return await dbSet.ToListAsync();
    }

    // Get an account by ID
    public async Task<Account?> GetAccountById(int accountId)
    {
        return await _context.Set<Account>().FindAsync(accountId);
    }

    // Add a new account
    public async Task<bool> AddAccount(Account account)
    {
        await dbSet.AddAsync(account);
        return true; 
    }

    // Update an existing account
    public async Task<bool> UpdateAccount(Account account)
    {
        var existingAccount = await dbSet.FindAsync(account.AccountId);

        if (existingAccount != null)
        {
            _context.Entry(existingAccount).CurrentValues.SetValues(account);
            return true; 
        }

        return false; // Account not found
    }

    // Remove an account by ID
    public async Task<bool> RemoveAccount(Guid id)
    {
        var accountToRemove = await dbSet.FindAsync(id);

        if (accountToRemove != null)
        {
            dbSet.Remove(accountToRemove);
            return true; 
        }

        return false; // Account not found
    }

}
