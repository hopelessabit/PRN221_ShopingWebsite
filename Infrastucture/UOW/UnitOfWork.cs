
using Core.Repositories;
using Microsoft.Extensions.Logging;

public class UnitOfWork : IUnitOfWork
{
    private readonly PMSDbContext _context;
    private readonly ILogger _logger;

    public IAccountRepository Account { get; private set; }

    public ICategoryRepository Category { get; private set; }

    public ICustomerRepository Customer { get; private set; }

    public IOrderRepository Order { get; private set; }

    public IOrderDetailRepository OrderDetail { get; private set; }

    public IProductRepository Product { get; private set; }

    public ISupplierRepository Supplier { get; private set; }

    public UnitOfWork(
        PMSDbContext context,
        ILoggerFactory logger
        )
    {
        _context = context;
        _logger = logger.CreateLogger("logs");

        Account = new AccountRepository(_context, _logger);
        Category = new CategoryRepository(_context, _logger);
        Customer = new CustomerRepository(_context, _logger);
        Order = new OrderRepository(_context, _logger);
        OrderDetail = new OrderDetailRepository(_context, _logger);
        Product = new ProductRepository(_context, _logger);
        Supplier = new SupplierRepository(_context, _logger);
    }

    public async Task<int> CompletedAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

