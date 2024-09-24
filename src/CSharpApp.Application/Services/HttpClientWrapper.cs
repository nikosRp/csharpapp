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
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while GET request to {uri}");
            throw;
        }
    }
    
    public async Task<T?> PostAsync<T>(string uri, object data)
    {
        try
        {
            var response = await _client.PostAsJsonAsync(uri, data); 
            response.EnsureSuccessStatusCode(); 
            var result = await response.Content.ReadFromJsonAsync<T>(); 
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while POST request to {uri}");
            throw;
        }
    }
    
    public async Task<T?> PutAsync<T>(string uri, object data)
    {
        try
        {
            var response = await _client.PutAsJsonAsync(uri, data); 
            response.EnsureSuccessStatusCode(); 
            var result = await response.Content.ReadFromJsonAsync<T>(); 
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while PUT request to {uri}");
            throw;
        }
    }
    
    public async Task<bool> DeleteAsync(string uri)
    {
        try
        {
            var response = await _client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while DELETE request to {uri}");
            throw;
        }
    }
}