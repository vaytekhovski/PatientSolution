using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.ConfigureSwagger();

        services.AddMongoDb(configuration);
        services.AddRepositories();
        services.AddApplicationServices();

        var apiBaseUrl = configuration["PATIENT_API_BASE_URL"] ?? "http://localhost:5000";
        services.AddSingleton(new ApiSettings { BaseUrl = apiBaseUrl });

        var corsSettings = configuration.GetSection("CorsPolicy");
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.WithOrigins(corsSettings.GetSection("AllowedOrigins").Get<string[]>())
                      .WithMethods(corsSettings.GetSection("AllowedMethods").Get<string[]>())
                      .WithHeaders(corsSettings.GetSection("AllowedHeaders").Get<string[]>());
            });
        });

        return services;
    }
}
