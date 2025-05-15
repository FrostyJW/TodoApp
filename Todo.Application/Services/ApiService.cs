using Microsoft.Extensions.Options;
using Todo.Application.Configuration;
using Todo.Application.Interfaces;
using Todo.Persistence.Interfaces;

namespace Todo.Application.Services;
public class ApiService : IApiService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly OAuthSettings oAuthSettings;
    private readonly IUserRepository userRepository;
    public ApiService(IUserRepository userRepository,IHttpClientFactory httpClientFactory, IOptionsMonitor<OAuthSettings> oAuthSettings)
    {
        this.httpClientFactory = httpClientFactory;
        this.oAuthSettings = oAuthSettings.CurrentValue;
        this.userRepository = userRepository;
    }

    public async Task GetAccessToken(string email)
    {
        if(email is null)
        {
            return;
        }
        var user = await userRepository.FindUserByEmail(email);
        await userRepository.GetToken(user);
        

        #region Token Generation with HTTP Client Factory
        ////Create the HTTP Client
        //var client = httpClientFactory.CreateClient("ApiClient");
        //client.BaseAddress = new Uri(oAuthSettings.BaseAddress);

        ////Set the Authorization header with Basic Authentication
        //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{oAuthSettings.ClientId}:{oAuthSettings.ClientSecret}"));
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHeaderValue);

        ////Prepare form Data, User Credentials (Claims)
        //var formData = new Dictionary<string, string>
        //{
        //    { "grant_type", "client_credentials" }
        //};

        ////Sends the POST request to the token endpoint with form data + Basic Authentication 
        //var tokenResponse = await client.PostAsync(oAuthSettings.TokenEndpoint, new FormUrlEncodedContent(formData));

        ////Read and Parse the response
        //var rawResponse = await tokenResponse.Content.ReadAsStringAsync();
        //var tokenData = JsonSerializer.Deserialize<OAuthTokenResponse>(rawResponse);

        ////Return the access token, If successful - Returns token as a String, Otherwise returns null
        //return tokenData?.AccessToken;
        #endregion
    }
}
