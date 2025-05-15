namespace Todo.Application.Interfaces;
public interface IApiService
{
    Task GetAccessToken(string email);
}
