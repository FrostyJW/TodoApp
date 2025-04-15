using Todo.Application.Dto;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Infrastructure.Enums;
using Todo.Persistence.Interfaces;

namespace Todo.Application.Services;
public class TodoItemService : ITodoItemService
{
    private readonly ITodoRepository todoRepository;
    public TodoItemService(ITodoRepository todoRepository)
    {
        this.todoRepository = todoRepository;
    }
    public async Task AddAsync(TodoItemDto entity)
    {
        await todoRepository.AddAsync(new()
        {
            Title = entity.Title,
            Description = entity.Description,
            Priority = entity.Priority,
            Deadline = entity.Deadline,
            CreationTime = entity.CreationTime
        }); 
    }

    public async Task Delete(int id)
    {
        await todoRepository.Delete(id);
    }

    public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
    {
        IEnumerable<TodoItem> todoItems = await todoRepository.GetAllAsync();
        return todoItems.Select(todoItem => new TodoItemDto
        {
            Id = todoItem.Id,
            Title = todoItem.Title,
            Description = todoItem.Description,
            Priority = todoItem.Priority,
            Deadline = todoItem.Deadline,
            CreationTime = todoItem.CreationTime
        });
    }

    public async Task<IEnumerable<TodoItemDto>> GetAllByCategory(string category)
    {
        IEnumerable<TodoItem> todoItems = await todoRepository.GetAllAsync();
        return todoItems.Where(todoitem => todoitem.Category.Title == category).Select(todoItem => new TodoItemDto()
        {
            Id = todoItem.Id,
            Title = todoItem.Title,
            Description = todoItem.Description,
            Priority = todoItem.Priority,
            Deadline = todoItem.Deadline,
            CreationTime = todoItem.CreationTime
        });
    }

    public async Task<IEnumerable<TodoItemDto>> GetAllByPriority(Priority priority)
    {
        IEnumerable<TodoItem> todoItems = await todoRepository.GetAllAsync();
        return todoItems.Where(todoitem => todoitem.Priority == priority).Select(todoItem => new TodoItemDto()
        {
            Id = todoItem.Id,
            Title = todoItem.Title,
            Description = todoItem.Description,
            Priority = todoItem.Priority,
            Deadline = todoItem.Deadline,
            CreationTime = todoItem.CreationTime
        });
    }

        public async Task<TodoItemDto> GetByIdAsync(int id)
    {
        TodoItem todo = await todoRepository.GetByIdAsync(id);
        if (todo == null)
        {
            return null;
        }
        else
        {
            return new TodoItemDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Priority = todo.Priority,
                Deadline = todo.Deadline,
                CreationTime = todo.CreationTime
            };
        }
    }

    public async Task Update(TodoItemDto entity)
    {
        TodoItem todo = await todoRepository.GetByIdAsync(entity.Id);
        todo.Title = entity.Title;
        todo.Description = entity.Description;
        todo.Priority = entity.Priority;
        todo.Deadline = entity.Deadline;
        todo.CreationTime = entity.CreationTime;
        await todoRepository.Update(todo);
    }
}
