using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Persistence.Contexts;
using Todo.Persistence.Interfaces;

namespace Todo.Persistence.Repositories;
public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly TodoDbContext context;
    public RoleRepository(RoleManager<IdentityRole> roleManager, TodoDbContext context)
    {
        this.roleManager = roleManager;
        this.context = context;
    }
    public async Task AddAsync(IdentityRole entity)
    {
        await roleManager.CreateAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());
        if(role is not null)
        {
            await roleManager.DeleteAsync(role);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<IdentityRole>> GetAllAsync()
    {
        return await roleManager.Roles.ToListAsync();
    }

    public async Task<IdentityRole> GetByIdAsync(int id)
    {
        return await roleManager.FindByIdAsync(id.ToString());
    }

    public async Task Update(IdentityRole entity)
    {
        var role = await roleManager.Roles.FirstOrDefaultAsync(p => p.Id == entity.Id);
        if (role is null)
        {
            return;
        }
        role.Name = entity.Name;
        role.NormalizedName = entity.NormalizedName;
        role.ConcurrencyStamp = entity.ConcurrencyStamp;
        
        await roleManager.UpdateAsync(role);
    }
}
