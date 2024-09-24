namespace CSharpApp.Api.Posts;

public static class GetAllPostsEndpoint
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
            .Produces<ReadOnlyCollection<PostRecordResponse>>(StatusCodes.Status200OK)
            .WithOpenApi();

        return app;
    }
}