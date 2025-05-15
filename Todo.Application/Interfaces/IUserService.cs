using Microsoft.AspNetCore.Identity;
using Todo.Application.Dto;
using Todo.Domain.Entities;

namespace Todo.Application.Interfaces;
public interface IUserService : IService<UserDto>
{
    Task<User> FindUserByEmail(string email);
    Task<IdentityResult> ResetUserPasswordAsync(User user, string token, string newPassword);
    Task<IEnumerable<string>> GetRolesAsync(User user);
}
