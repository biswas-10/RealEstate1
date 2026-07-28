
using System.Net.Http.Headers;

namespace RealEstate.Frontend.Services;

public class ApiHandler
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiHandler(
        HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor)
    {
        if (httpClient is null)
        {
            throw new ArgumentNullException(nameof(httpClient));
        }

        if (httpContextAccessor is null)
        {
            throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    private void AddAuthorizationHeader()
    {
        var user =
            _httpContextAccessor.HttpContext?.User;

        var token =
            user?.FindFirst("access_token")?.Value;

        _httpClient.DefaultRequestHeaders.Authorization =
            null;

        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);
        }
    }
    
    // GET
    public async Task<TResponse?> GetAsync<TResponse>(
        string endpoint,
        bool authenticated = true)
    {
        if (authenticated)
        {
            AddAuthorizationHeader();
        }

        return await _httpClient
            .GetFromJsonAsync<TResponse>(endpoint);
    }
    
    //POST
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        bool authenticated = true)
    {
        if (authenticated)
        {
            AddAuthorizationHeader();
        }

        var response =
            await _httpClient.PostAsJsonAsync(
                endpoint,
                request);

        // response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<TResponse>();
    }
    
    //PUT
    public async Task<TResponse?> PutAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        bool authenticated = true)
    {
        if (authenticated)
        {
            AddAuthorizationHeader();
        }

        var response =
            await _httpClient.PutAsJsonAsync(
                endpoint,
                request);
        
        // response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<TResponse>();
    }
    
    //DELETE
    public async Task<TResponse?> DeleteAsync<TResponse>(
        string endpoint,
        bool authenticated = true)
    {
        if (authenticated)
        {
            AddAuthorizationHeader();
        }

        var response =
            await _httpClient.DeleteAsync(endpoint);
        
        // response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<TResponse>();
    }
}