
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected PMSDbContext _context;
    protected DbSet<T> dbSet;
    protected readonly ILogger _logger;
    public GenericRepository(
        PMSDbContext context,
        ILogger logger)
    {
        _context = context;
        _logger = logger;
        dbSet = _context.Set<T>();
    }

    public async Task<bool> Add(T entity)
    {
        await dbSet.AddAsync(entity);
        return true;
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return dbSet.Where(expression);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await dbSet.ToListAsync();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task<bool> Remove(int id)
    {
        var t = await dbSet.FindAsync(id);

        if (t != null)
        {
            dbSet.Remove(t);
            return true;
        }
        else
            return false;
    }

    public async Task<bool> Upsert(T entity, int id)
    {
        try
        {
            var existingEntity = await dbSet.FindAsync(id);

            if (existingEntity != null)
            {
                // Entity exists, update it
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                // Entity does not exist, add it
                await dbSet.AddAsync(entity);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while upserting entity.");
            return false;
        }

    }
}
