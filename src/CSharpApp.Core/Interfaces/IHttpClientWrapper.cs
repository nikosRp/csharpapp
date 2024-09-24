namespace CSharpApp.Core.Interfaces;

public interface IHttpClientWrapper
{
    Task<T?> GetAsync<T>(string uri);
    Task<T?> PostAsync<T>(string uri, object data);
    Task<T?> PutAsync<T>(string uri, object data);
    Task<bool> DeleteAsync(string uri);
}