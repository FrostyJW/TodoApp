using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Entities;
using Todo.Persistence.Contexts;

namespace Todo.Persistence;
public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TodoDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
             .AddEntityFrameworkStores<TodoDbContext>();
        
        return services;
    }
}
