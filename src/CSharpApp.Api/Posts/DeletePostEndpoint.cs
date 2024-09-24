namespace CSharpApp.Api.Posts;

public static class DeletePostEndpoint
{
    private const string Name = "DeletePost";

    public static IEndpointRouteBuilder MapDeletePost(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/posts/{id}", async ([FromRoute] int id, IPostService postService) =>
            {
                var deleted = await postService.DeleteByIdAsync(id);
                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.Ok();
            })
            .WithName(Name)
            .WithOpenApi();
        return app;
    }
}