using Microsoft.AspNetCore.Identity;
using Todo.Domain.Entities;

namespace Todo.Persistence.Interfaces;
public interface IUserRepository : IRepository<User>
{
}
