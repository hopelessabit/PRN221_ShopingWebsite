using System.Linq.Expressions;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetById(Guid id);
    Task<IEnumerable<T>> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    Task<bool> Add(T entity);
    Task<bool> Remove(int id);
    Task<bool> Upsert(T entity);
}