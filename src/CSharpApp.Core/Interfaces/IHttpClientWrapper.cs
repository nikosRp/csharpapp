namespace CSharpApp.Core.Interfaces;

public interface IHttpClientWrapper
{
    Task<T?> GetAsync<T>(string uri);
    Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest body);
    Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest body);
    Task<bool> DeleteAsync(string uri);
}