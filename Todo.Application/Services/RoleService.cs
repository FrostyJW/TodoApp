using Microsoft.AspNetCore.Identity;
using Todo.Application.Interfaces;
using Todo.Persistence.Interfaces;

namespace Todo.Application.Services;
public class RoleService : IRoleService
{
    private readonly IRoleRepository roleRepository;
    public RoleService(IRoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }
    public async Task AddAsync(IdentityRole entity)
    {
        await roleRepository.AddAsync(entity);
    }

    public async Task Delete(int id)
    {
        await roleRepository.Delete(id);
    }

    public async Task<IEnumerable<IdentityRole>> GetAllAsync()
    {
        return await roleRepository.GetAllAsync();
    }

    public async Task<IdentityRole> GetByIdAsync(int id)
    {
        return await roleRepository.GetByIdAsync(id);
    }

    public async Task Update(IdentityRole entity)
    {
        await roleRepository.Update(entity);
    }
}
