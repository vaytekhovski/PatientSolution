using ConsoleApp.Factory;
using ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp;

public static class ServiceConfigurator
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        var apiUrl = Environment.GetEnvironmentVariable("PATIENT_API_BASE_URL") ?? "http://localhost:5000";

        services.AddSingleton<HttpClient>();
        services.AddSingleton<IPatientFactory, PatientFactory>();
        services.AddSingleton<IPatientSender, PatientSender>();
        services.AddSingleton<IApiChecker, ApiChecker>(sp => new ApiChecker(sp.GetRequiredService<HttpClient>(), apiUrl));
        services.AddSingleton<SendPatientCommand>(sp => new SendPatientCommand(sp.GetRequiredService<HttpClient>(), apiUrl));

        return services.BuildServiceProvider();
    }
}
