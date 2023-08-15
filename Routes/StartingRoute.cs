namespace MinimalApi.Routes;

public class StartingRoute : IRouteEndpoint
{
    public void DefineEndpoints(WebApplication app)
    {
        app.Map("api/start", Greeting);
    }

    internal IResult Greeting()
    {
        return Results.Ok("Hello, World!");
    }
}
