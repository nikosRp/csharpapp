namespace CSharpApp.Core.Interfaces;

public interface IPostService
{
    Task<PostRecordResponse?> GetPostById(int id);
    Task<ReadOnlyCollection<PostRecordResponse>> GetAllPosts();
    Task<PostRecordResponse> CreateAsync(PostRecordRequest newPost);
}