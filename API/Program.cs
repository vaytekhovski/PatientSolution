using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationLayer()
    .AddSwagger()
    .AddMongoDb(builder.Configuration)
    .ConfigureCors(builder.Configuration);

var app = builder.Build();

app.ConfigureMiddleware();
app.Run();
