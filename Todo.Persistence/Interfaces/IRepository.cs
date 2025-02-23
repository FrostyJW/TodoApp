using Todo.Domain.Entities;

namespace Todo.Persistence.Interfaces;
public interface IRepository<TEntity> where TEntity : BaseEntity<int>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task AddAsync(TEntity entity);
    Task Delete(int id);
    Task Update(TEntity entity);
}
