using Core.Entities;

namespace Core.Interfaces.Repositories;
public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T?> FindAsync(Guid id);
    Task<List<T>> FindAsync();
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}