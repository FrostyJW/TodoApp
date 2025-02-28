using Microsoft.AspNetCore.Identity;

namespace Todo.Persistence.Interfaces;
public interface IRoleRepository : IRepository<IdentityRole>
{
}
