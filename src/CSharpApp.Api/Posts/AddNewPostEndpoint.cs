namespace CSharpApp.Api.Posts;

public static class AddNewPostEndpoint
{
    private const string Name = "AddNewPost";

    public static IEndpointRouteBuilder MapAddNewPost(this IEndpointRouteBuilder app)
    {
        app.MapPost("/posts", async (PostRecordRequest newPost, IPostService postService) =>
            {
                var result = await postService.CreateAsync(newPost);
                return TypedResults.CreatedAtRoute(result, GetPostEndpoint.Name, new { id = result.Id });
            })
            .WithName(Name)
            .WithOpenApi();
        return app;
    }
}