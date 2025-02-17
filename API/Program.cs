using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddApplicationLayer()
    .AddSwagger()
    .AddMongoDb(builder.Configuration)
    .ConfigureCors(builder.Configuration);

var app = builder.Build();

app.ConfigureMiddleware();
app.Run();
