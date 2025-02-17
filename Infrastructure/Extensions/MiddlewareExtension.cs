using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Infrastructure.Middlewares;

namespace Infrastructure.Extensions;

public static class MiddlewareExtension
{
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors("AllowAll");
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}
