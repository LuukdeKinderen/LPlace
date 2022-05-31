using MassTransit;
using Microsoft.EntityFrameworkCore;
using Service_Advertisement;
using Service_Advertisement.Consumers;
using Service_Advertisement.Database;
using Service_Advertisement.Database.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Database
builder.Services.AddTransient<IAdvertisementContext, AdvertisementContext>();
builder.Services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AdvertisementContext>(options => options.UseSqlServer(connection));

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//RabbitMQ
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<UserConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://guest:guest@rabbitmq:5672");

        cfg.ConfigureEndpoints(ctx);
        //cfg.ReceiveEndpoint("user-queue", c =>
        //{
        //    c.ConfigureConsumer<UserConsumer>(ctx);
        //}); 
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
            var context = services.GetRequiredService<AdvertisementContext>();
            DbInitializer.Initialize(context);
            //context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}