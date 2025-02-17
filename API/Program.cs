using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder.Configuration);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();
app.ConfigureMiddleware();
app.Run();
