using Microsoft.AspNetCore.Identity;

namespace Todo.Application.Interfaces;
public interface IAuthorizationService
{
    Task<SignInResult> Login(string email, string password);
    Task Logout();
    Task<bool> ResetPassword(string email, string token, string newPassword);
}
