using Todo.Application.Dto;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Persistence.Interfaces;

namespace Todo.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IRoleRepository roleRepository;
    public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        this.userRepository = userRepository;
        this.roleRepository = roleRepository;
    }
    public async Task AddAsync(UserDto entity)
    {
        var role = await roleRepository.GetByIdAsync(Convert.ToInt32(entity.Role.Id));
        await userRepository.AddAsync(new User()
        {
            Id = entity.Id.ToString(),
            Email = entity.Email,
            Role = role,
            Todos = entity.Todos.Select(t => new TodoItem()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
                Deadline = t.Deadline,
                CreationTime = t.CreationTime
            }).ToList(),
            Projects = entity.Projects.Select(p => new Project()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Status = p.Status,
                Deadline = p.Deadline
            }).ToList()
        });  
    }

    public async Task Delete(int id)
    {
        await userRepository.Delete(id);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        IEnumerable<User> enumerable = await userRepository.GetAllAsync();
        return enumerable.Select(user => new UserDto()
        {
            Id = Convert.ToInt32(user.Id),
            Email = user.Email,
            Role = user.Role,
            Todos = user.Todos.Select(t => new TodoItemDto()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
                Deadline = t.Deadline,
                CreationTime = t.CreationTime
            }).ToList(),
            Projects = user.Projects.Select(p => new ProjectDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Status = p.Status,
                Deadline = p.Deadline
            }).ToList()
        });
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        User user = await userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return null;
        }
        else
        {
            return new UserDto()
            {
                Id = Convert.ToInt32(user.Id),
                Email = user.Email,
                Role = user.Role,
                Todos = user.Todos.Select(t => new TodoItemDto()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    Deadline = t.Deadline,
                    CreationTime = t.CreationTime
                }).ToList(),
                Projects = user.Projects.Select(p => new ProjectDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Status = p.Status,
                    Deadline = p.Deadline
                }).ToList()
            };
        }
    }
    public async Task Update(UserDto entity)
    {
        User user = await userRepository.GetByIdAsync(entity.Id);
        user.Id = entity.Id.ToString();
        user.Email = entity.Email;
        user.Role = entity.Role;
        user.Todos = entity.Todos.Select(t => new TodoItem()
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Priority = t.Priority,
            Deadline = t.Deadline,
            CreationTime = t.CreationTime
        }).ToList();
        user.Projects = entity.Projects.Select(p => new Project()
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Status = p.Status,
            Deadline = p.Deadline
        }).ToList();
        await userRepository.Update(user);
    }
}
