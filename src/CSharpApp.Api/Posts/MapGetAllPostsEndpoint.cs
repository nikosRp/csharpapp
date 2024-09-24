namespace CSharpApp.Api.Posts;

public static class MapGetAllPostsEndpoint
{
    private const string Name = "GetPosts";

    public static IEndpointRouteBuilder MapGetAllPosts(this IEndpointRouteBuilder app)
    {
        app.MapGet("/posts", async (IPostService postService) =>
            {
                var posts = await postService.GetAllPosts();
                return posts;
            })
            .WithName(Name)
            .WithOpenApi();

        return app;
    }
}