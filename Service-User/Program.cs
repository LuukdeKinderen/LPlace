using MassTransit;
using Microsoft.EntityFrameworkCore;
using Service_User;
using Service_User.Database;

var builder = WebApplication.CreateBuilder(args);

//Database
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(Global.DatabaseConnectionString));

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//RabbitMQ
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host($"amqp://{Global.RabbitMqUsername}:{Global.RabbitMqPassword}@rabbitmq:5672");
    });
});


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

CreateDbIfNotExists(app);

app.Run();


void CreateDbIfNotExists(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<UserContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}