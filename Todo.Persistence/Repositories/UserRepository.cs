using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Persistence.Contexts;
using Todo.Persistence.Interfaces;

namespace Todo.Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserManager<User> userManager;
    private readonly TodoDbContext context;
    public UserRepository(UserManager<User> userManager, TodoDbContext context)
    {
        this.userManager = userManager;
        this.context = context;
    }
    public async Task AddAsync(User entity)
    {
        await userManager.CreateAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is not null)
        {
            await userManager.DeleteAsync(user);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        //return null;
        return await userManager.Users.Include(p => p.Todos).ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await userManager.Users.Include(p => p.Todos).FirstOrDefaultAsync(p => p.Id == id.ToString());
    }

    public async Task Update(User entity)
    {
        var user = await userManager.Users.Include(p => p.Todos).FirstOrDefaultAsync(p => p.Id == entity.Id);
        if (user is null)
        {
            return;
        }
        user.Email = entity.Email;
        user.Todos = entity.Todos;

        await userManager.UpdateAsync(user);
    }
}
