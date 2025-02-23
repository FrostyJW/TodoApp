using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Persistence.Contexts;
using Todo.Persistence.Interfaces;

namespace Todo.Persistence.Repositories;
public class UserRepository(TodoDbContext context) : IUserRepository
{
    public async Task AddAsync(User entity)
    {
        await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var user = await context.Users.FirstOrDefaultAsync(p => p.Id == id);
        if(user is not null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.Include(p => p.Todos).ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await context.Users.Include(p => p.Todos).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(User entity)
    {
        var user = await context.Users.Include(p => p.Todos).FirstOrDefaultAsync(p => p.Id == entity.Id);
        if(user is null)
        {
            return;
        }
        user.PasswordHash = entity.PasswordHash;
        user.Email = entity.Email;
        user.Todos = entity.Todos;

        await context.SaveChangesAsync();
    }
}
