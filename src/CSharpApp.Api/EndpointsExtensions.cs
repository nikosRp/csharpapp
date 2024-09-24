namespace CSharpApp.Api;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapTodoEndpoints();
        app.MapPostEndpoints();
        return app;
    }
}