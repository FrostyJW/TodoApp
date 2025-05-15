using Microsoft.AspNetCore.Identity;
using Todo.Domain.Entities;

namespace Todo.Persistence.Interfaces;
public interface IUserRepository : IRepository<User>
{
    Task AddAsync(User entity, string password);
    Task<User> FindUserByEmail(string email);
    Task<User> GetByIdAsync(string id);
    Task<IdentityResult> ResetUserPasswordAsync(User user, string token, string newPassword);
    Task<IEnumerable<string>> GetRolesAsync(User user);
    public Task GetToken(User user);
}
