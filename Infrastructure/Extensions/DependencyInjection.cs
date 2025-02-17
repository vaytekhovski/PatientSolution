using Application.Services;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IPatientChangeLogRepository, PatientChangeLogRepository>();
        services.AddScoped<IPatientService, PatientService>();
        return services;
    }
}
