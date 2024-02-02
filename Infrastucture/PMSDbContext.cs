
using Core.Entities;
using Microsoft.EntityFrameworkCore;

public class PMSDbContext : DbContext
{
    public PMSDbContext(DbContextOptions<PMSDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Projects { get; set; }
}
