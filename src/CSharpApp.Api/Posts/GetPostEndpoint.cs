namespace CSharpApp.Api.Posts;

public static class GetPostEndpoint
{
    public const string Name = "GetPostById";

    public static IEndpointRouteBuilder MapGetPost(this IEndpointRouteBuilder app)
    {
        app.MapGet("/posts/{id}", async ([FromRoute] int id, IPostService postService) =>
            {
                var post = await postService.GetPostById(id);
                return post;
            })
            .WithName(Name)
            .Produces<PostRecordResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        return app;
    }
}