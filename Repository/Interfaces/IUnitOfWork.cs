public interface IUnitOfWork : IDisposable
{
    IAccountRepository Account { get; }
    ICategoryRepository Category { get; }
    ICustomerRepository Customer { get; }
    IOrderRepository Order { get; }
    IOrderDetailRepository OrderDetail { get; }
    IProductRepository Product { get; }
    ISupplierRepository Supplier { get; }
    Task<int> CompletedAsync();
}
