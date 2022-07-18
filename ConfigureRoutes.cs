using System.Reflection;

namespace MinimalApi;

internal static class ConfigureRoutes
{
    internal static IServiceCollection AddRouting(this IServiceCollection services, params Assembly[] assemblies)
    {
        var routes = new List<IRouting>();

        foreach (var assembly in assemblies)
        {
            routes.AddRange(assembly.ExportedTypes
                .Where(x => typeof(IRouting).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IRouting>()
            );
        }

        services.AddSingleton(routes as IReadOnlyCollection<IRouting>);

        return services;
    }

    internal static WebApplication UseRouting(this WebApplication app)
    {
        var routes = app.Services.GetRequiredService<IReadOnlyCollection<IRouting>>();

        foreach(var route in routes)
        {
            route.MapRoutes(app);
        }

        return app;
    }
}