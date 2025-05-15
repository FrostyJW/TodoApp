using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;
using Todo.Application.Configuration;
using Todo.Application.Interfaces;
using Todo.Application.Profiles;
using Todo.Application.Services;
using Todo.Domain.Entities;
using Todo.Persistence.Contexts;

namespace Todo.Application;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var c = configuration.GetSection("OAuthSettings");
        services.AddOptions<OAuthSettings>().Bind(configuration.GetSection("OAuthSettings"));
        services.AddHttpClient("ApiClient").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        });

        //services.AddIdentity<User, IdentityRole >().AddEntityFrameworkStores<TodoDbContext>();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ITodoItemService, TodoItemService>();
        services.AddScoped<IApiService,ApiService>();
        services.AddScoped<SignInManager<User>>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddRazorPages();
        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

        

        return services;
    }
}
