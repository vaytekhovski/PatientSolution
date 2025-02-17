using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Extensions;

public static class MiddlewareExtensions
{
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors("AllowAll");
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.MapControllers();
        return app;
    }
}
