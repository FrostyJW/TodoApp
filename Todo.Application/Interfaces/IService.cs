namespace Todo.Application.Interfaces;
public interface IService<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task AddAsync(TEntity entity);
    Task Delete(int id);
    Task Update(TEntity entity);
}
