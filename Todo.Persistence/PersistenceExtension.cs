using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Entities;
using Todo.Persistence.Contexts;
using Todo.Persistence.Interfaces;
using Todo.Persistence.Repositories;

namespace Todo.Persistence;
public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TodoDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        //services.AddAuthorization();
        //services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthentication(IdentityConstants.ApplicationScheme).AddCookie(IdentityConstants.ApplicationScheme);
        services.AddAuthorization();

        services.AddIdentityCore<User>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedEmail = false;
        })
            .AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<TodoDbContext>()
             .AddDefaultTokenProviders();

        //services.AddIdentityApiEndpoints<User>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
    //public static WebApplication AddPersistance(this WebApplication services)
    //{
    //    services.MapIdentityApi<User>();

    //    return services;
    //}
}
