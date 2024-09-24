namespace CSharpApp.Api.Todos;

public static class GetTodoEndpoint
{
    private const string Name = "GetTodosById";

    public static IEndpointRouteBuilder MapGetTodo(this IEndpointRouteBuilder app)
    {
        app.MapGet("/todos/{id}", async ([FromRoute] int id, ITodoService todoService) =>
            {
                var todos = await todoService.GetTodoById(id);
                return todos;
            })
            .WithName(Name)
            .WithOpenApi();
        return app;
    }
}