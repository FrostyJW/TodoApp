using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;
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
    public async Task<IEnumerable<string>> GetRolesAsync(User user)
    {
        return await userRepository.GetRolesAsync(user);
    }
    public async Task<User> FindUserByEmail(string email)
    {
        User user = await userRepository.FindUserByEmail(email);
        if (user is null)
        {
            return null;
        }
        return user;
    }
    public async Task<IdentityResult> ResetUserPasswordAsync(User user, string token, string newPassword)
    {
        var userEntity = await userRepository.GetByIdAsync(user.Id);
        if (userEntity is null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }
        return await userRepository.ResetUserPasswordAsync(userEntity, token, newPassword);
    }
    public async Task AddAsync(UserDto entity)
    {
        var role = await roleRepository.GetById("3f2504e0-4f89-11d3-9a0c-0305e82c3301");
        await userRepository.AddAsync(new User()
        {
            UserName = entity.Username,
            Email = entity.Email,
            Role = role.Name,
            //Todos = entity.Todos.Select(t => new TodoItem()
            //{
            //    Id = t.Id,
            //    Title = t.Title,
            //    Description = t.Description,
            //    Priority = t.Priority,
            //    Deadline = t.Deadline,
            //    CreationTime = t.CreationTime
            //}).ToList(),
            //Projects = entity.Projects.Select(p => new Project()
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Description = p.Description,
            //    Status = p.Status,
            //    Deadline = p.Deadline
            //}).ToList()
        }, entity.Password);  

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
            Id = user.Id,
            Email = user.Email,
            //Role = user.Role,
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

    public async Task<UserDto> GetByIdAsync(string id)
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
                Id = user.Id,
                Email = user.Email,
                //Role = user.Role,
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
        user.Email = entity.Email;
        //user.Role = entity.Role;
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

    public Task<UserDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
