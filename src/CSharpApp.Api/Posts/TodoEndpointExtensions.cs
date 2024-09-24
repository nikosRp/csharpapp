namespace CSharpApp.Api.Posts;

public static class PostEndpointExtensions
{
    public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetPost();
        app.MapGetAllPosts();

        return app;
    }
}