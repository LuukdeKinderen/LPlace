using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();
builder.WebHost.ConfigureAppConfiguration((context, config) =>
{
    config.SetBasePath(context.HostingEnvironment.ContentRootPath)
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
        .AddJsonFile("routes.json", false, true)
        .AddEnvironmentVariables();
});

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();