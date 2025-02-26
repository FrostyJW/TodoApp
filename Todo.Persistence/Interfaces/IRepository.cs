using Microsoft.AspNetCore.Identity;
using Todo.Domain.Entities;

namespace Todo.Persistence.Interfaces;
public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task AddAsync(TEntity entity);
    Task Delete(int id);
    Task Update(TEntity entity);
}
