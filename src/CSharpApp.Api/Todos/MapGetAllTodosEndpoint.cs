namespace CSharpApp.Api.Todos;

public static class MapGetAllTodosEndpoint
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
            .WithOpenApi();

        return app;
    }
}