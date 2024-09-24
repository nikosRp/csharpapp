namespace CSharpApp.Api.Todos;

public static class TodoEndpointExtensions
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetTodo();
        app.MapGetAllTodos();

        return app;
    }
}