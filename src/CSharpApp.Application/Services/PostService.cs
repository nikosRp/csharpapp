namespace CSharpApp.Application.Services;

public class PostService : IPostService
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string? _baseUrl;

    public PostService(IHttpClientWrapper httpClientWrapper, IOptions<ApiSettings> options)
    {
        _httpClientWrapper = httpClientWrapper;
        _baseUrl = options.Value.BaseUrl;
    }


    public async Task<PostRecordResponse?> GetPostById(int id)
    {
        var uri = $"{_baseUrl}/posts/{id}";
        return await _httpClientWrapper.GetAsync<PostRecordResponse>(uri);
    }

    public async Task<ReadOnlyCollection<PostRecordResponse>> GetAllPosts()
    {
        var uri = $"{_baseUrl}/posts";
        var result = await _httpClientWrapper.GetAsync<List<PostRecordResponse>>(uri);
        return result!.AsReadOnly();
    }

    public async Task<PostRecordResponse?> CreateAsync(PostRecordRequest newPost)
    {
        var createdPost = await _httpClientWrapper.PostAsync<PostRecordResponse>("posts", newPost);
        return createdPost;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        return await _httpClientWrapper.DeleteAsync($"posts/{id}");
    }
}
