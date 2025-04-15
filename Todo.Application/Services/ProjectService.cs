using Todo.Application.Dto;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Persistence.Interfaces;

namespace Todo.Application.Services;
public class ProjectService : IProjectService
{
    private readonly IProjectRepository projectRepository;
    private readonly ITodoRepository todoRepository;
    public ProjectService(IProjectRepository projectRepository, ITodoRepository todoRepository)
    {
        this.projectRepository = projectRepository;    
        this.todoRepository = todoRepository;    
    }
    public async Task AddAsync(ProjectDto entity)
    {
        await projectRepository.AddAsync(new()
        {
            Name = entity.Name,
            Description = entity.Description,
            Status = entity.Status,
            Deadline = entity.Deadline,
            Todos = entity.Todos.Select(todo => new TodoItem()
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Priority = todo.Priority,
                Deadline = todo.Deadline,
                CreationTime = todo.CreationTime,
            }).ToList()
        });
    }

    public async Task Delete(int id)
    {
        await projectRepository.Delete(id);
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync()
    {
        IEnumerable<Project> enumerable = await projectRepository.GetAllAsync();
        return enumerable.Select(x => new ProjectDto()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Status = x.Status,
            Deadline = x.Deadline,
            Todos = x.Todos.Select(todo => new TodoItemDto()
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Priority = todo.Priority,
                Deadline = todo.Deadline,
                CreationTime = todo.CreationTime,
            }).ToList()
        });
    }

    public async Task<ProjectDto> GetByIdAsync(int id)
    {
        Project project = await projectRepository.GetByIdAsync(id);
        return new ProjectDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            Deadline = project.Deadline,
            Todos = project.Todos.Select(todo => new TodoItemDto()
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Priority = todo.Priority,
                Deadline = todo.Deadline,
                CreationTime = todo.CreationTime,
            }).ToList()
        };
    }

    public async Task Update(ProjectDto entity)
    {
        Project project = await projectRepository.GetByIdAsync(entity.Id);
        if (project is null)
        {
            return;
        }
        else
        {
            project.Name = entity.Name;
            project.Description = entity.Description;
            project.Status = entity.Status;
            project.Deadline = entity.Deadline;
            project.Todos = entity.Todos.Select(todo => new TodoItem()
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Priority = todo.Priority,
                Deadline = todo.Deadline,
                CreationTime = todo.CreationTime,
            }).ToList();
            await projectRepository.Update(project);
        }
    }
}
