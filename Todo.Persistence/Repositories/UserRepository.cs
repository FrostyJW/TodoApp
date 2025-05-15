using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public async Task GetToken(User user)
    {
        if(user is null)
        {
            return;
        }
        var token = await userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, user.Id.ToString());
        await userManager.SetAuthenticationTokenAsync(user,TokenOptions.DefaultProvider, user.Id.ToString(), token);
        //return token;
        //userManager.GenerateUserTokenAsync();
        //userManager.SetAuthenticationTokenAsync();
        //userManager.GetAuthenticationTokenAsync();
    }
    public async Task<IEnumerable<string>> GetRolesAsync(User user)
    {
        return await userManager.GetRolesAsync(user);
    }
    public async Task<User> FindUserByEmail(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }
    public async Task<IdentityResult> ResetUserPasswordAsync(User user, string token, string newPassword)
    {
        return await userManager.ResetPasswordAsync(user, token, newPassword);
    }
    public async Task AddAsync(User entity,string password)
    {
        try
        {
            var creationResult = await userManager.CreateAsync(entity, password);
            if (!creationResult.Succeeded)
            {
                var errors = string.Join(", ", creationResult.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }
            var roleResult = await userManager.AddToRoleAsync(entity, "ADMIN");
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                throw new Exception($"Adding to role failed: {errors}");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            }
        }
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

    public async Task<User> GetByIdAsync(string id)
    {
        return await userManager.Users.Include(p => p.Todos).FirstOrDefaultAsync(p => p.Id == id);
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

    public Task AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
