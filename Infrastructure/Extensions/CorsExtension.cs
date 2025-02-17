using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class CorsExtension
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var corsSettings = configuration.GetSection("CorsPolicy").Get<CorsOptions>() ?? new CorsOptions();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.WithOrigins(corsSettings.AllowedOrigins)
                      .WithMethods(corsSettings.AllowedMethods)
                      .WithHeaders(corsSettings.AllowedHeaders);
            });
        });

        return services;
    }
}

public class CorsOptions
{
    public string[] AllowedOrigins { get; set; } = new[] { "*" };
    public string[] AllowedMethods { get; set; } = new[] { "GET", "POST", "PUT", "DELETE" };
    public string[] AllowedHeaders { get; set; } = new[] { "Content-Type", "Authorization" };
}
