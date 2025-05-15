using Microsoft.AspNetCore.Identity;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Application.Services;
public class AuthorizationService : IAuthorizationService
{
    private readonly IUserService userService;
    private readonly SignInManager<User> signInManager;
    public AuthorizationService(IUserService userService, SignInManager<User> signInManager)
    {
        this.signInManager = signInManager;    
        this.userService = userService;
    }
    public async Task<SignInResult> Login(string email, string password)
    {
        var user = await userService.FindUserByEmail(email);
        if(user is null)
        {
            return SignInResult.Failed;
        }
        return await signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
    }

    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<bool> ResetPassword(string email, string token, string newPassword)
    {
        var user = await userService.FindUserByEmail(email);
        if (user is null)
            return false;
        var result = await userService.ResetUserPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }
}
