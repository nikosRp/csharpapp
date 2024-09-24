namespace CSharpApp.Application.Services;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _client;
    private readonly ILogger<HttpClientWrapper> _logger;

    public HttpClientWrapper(HttpClient client, ILogger<HttpClientWrapper> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string uri)
    {
        try
        {
            var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<T>();
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error occurred while performing GET request to {Uri}", uri);
            throw;
        }
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest body)
    {
        try
        {
            var response = await _client.PostAsJsonAsync(uri, body);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TResponse>();
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error occurred while performing POST request to {Uri}", uri);
            throw;
        }
    }

    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest body)
    {
        try
        {
            var response = await _client.PutAsJsonAsync(uri, body);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TResponse>();
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error occurred while performing PUT request to {Uri}", uri);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(string uri)
    {
        try
        {
            var response = await _client.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error occurred while performing DELETE request to {Uri}", uri);
            throw;
        }
    }
}