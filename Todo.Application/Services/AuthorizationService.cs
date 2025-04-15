using Microsoft.AspNetCore.Identity;
using Todo.Application.Interfaces;

namespace Todo.Application.Services;
public class AuthorizationService : IAuthorizationService
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    public AuthorizationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }
    public async Task<SignInResult> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
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
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return false;
        var result = await userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }
}
