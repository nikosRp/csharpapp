namespace CSharpApp.Api.Todos;

public static class GetAllTodosEndpoint
{
    private const string Name = "GetTodos";

    public static IEndpointRouteBuilder MapGetAllTodos(this IEndpointRouteBuilder app)
    {
        app.MapGet("/todos", async (ITodoService todoService) =>
            {
                var todos = await todoService.GetAllTodos();
                return todos;
            })
            .WithName(Name)
            .Produces<ReadOnlyCollection<TodoRecordResponse>>(StatusCodes.Status200OK)
            .WithOpenApi();

        return app;
    }
}