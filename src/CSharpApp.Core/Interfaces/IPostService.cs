namespace CSharpApp.Core.Interfaces;

public interface IPostService
{
    Task<PostRecord?> GetPostById(int id);

}