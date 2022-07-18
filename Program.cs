using MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices((_, services) =>
{
    services.AddRouting(typeof(IRouting).Assembly);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.Run();