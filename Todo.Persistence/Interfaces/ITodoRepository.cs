using Todo.Domain.Entities;

namespace Todo.Persistence.Interfaces;
public interface ITodoRepository : IRepository<TodoItem>
{
}
