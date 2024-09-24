namespace CSharpApp.Application.Services;

public class PostService : IPostService
{
    private readonly ILogger<PostService> _logger;
    private readonly HttpClient _client;

    private readonly string? _baseUrl;

    public PostService(ILogger<PostService> logger, 
        IConfiguration configuration)
    {
        _logger = logger;
        _client = new HttpClient();
        _baseUrl = configuration["BaseUrl"];
    }

    public async Task<PostRecord?> GetPostById(int id)
    {
        _client.BaseAddress = new Uri(_baseUrl!);
        var response = await _client.GetFromJsonAsync<PostRecord>($"posts/{id}");

        return response;
    }

    public async Task<ReadOnlyCollection<PostRecord>> GetAllPosts()
    {
        _client.BaseAddress = new Uri(_baseUrl!);
        var response = await _client.GetFromJsonAsync<List<PostRecord>>($"posts");

        return response!.AsReadOnly();
    }
}