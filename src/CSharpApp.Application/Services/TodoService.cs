namespace CSharpApp.Application.Services;

public class TodoService : ITodoService
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string? _baseUrl;

    public TodoService(IHttpClientWrapper httpClientWrapper, IOptions<ApiSettings> options)
    {
        _httpClientWrapper = httpClientWrapper;
        _baseUrl = options.Value.BaseUrl;
    }

    public async Task<TodoRecordResponse?> GetTodoById(int id)
    {
        var uri = $"{_baseUrl}/todos/{id}";
        return await _httpClientWrapper.GetAsync<TodoRecordResponse>(uri);
    }

    public async Task<ReadOnlyCollection<TodoRecordResponse>> GetAllTodos()
    {
        var uri = $"{_baseUrl}/todos";
        var result = await _httpClientWrapper.GetAsync<List<TodoRecordResponse>>(uri);
        return result!.AsReadOnly();
    }
}