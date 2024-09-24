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

    public async Task<PostRecordResponse?> GetPostById(int id)
    {
        _client.BaseAddress = new Uri(_baseUrl!);
        var response = await _client.GetFromJsonAsync<PostRecordResponse>($"posts/{id}");

        return response;
    }

    public async Task<ReadOnlyCollection<PostRecordResponse>> GetAllPosts()
    {
        _client.BaseAddress = new Uri(_baseUrl!);
        var response = await _client.GetFromJsonAsync<List<PostRecordResponse>>($"posts");

        return response!.AsReadOnly();
    }

    public async Task<PostRecordResponse> CreateAsync(PostRecordRequest newPost)
    {
        _client.BaseAddress = new Uri(_baseUrl!);
        var response = await _client.PostAsJsonAsync("posts", newPost);
        response.EnsureSuccessStatusCode();
        var createdPost = await response.Content.ReadFromJsonAsync<PostRecordResponse>();
        return createdPost;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        _client.BaseAddress = new Uri(_baseUrl!);
        
        var response = await _client.DeleteAsync($"posts/{id}");
        
        return response.IsSuccessStatusCode;
    }
}