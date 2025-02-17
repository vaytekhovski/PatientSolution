using ConsoleApp;
using ConsoleApp.Factory;
using ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = ServiceConfigurator.ConfigureServices();

var apiChecker = serviceProvider.GetRequiredService<IApiChecker>();
var patientFactory = serviceProvider.GetRequiredService<IPatientFactory>();
var patientSender = serviceProvider.GetRequiredService<IPatientSender>();

if (!await apiChecker.IsApiAvailableAsync())
{
    Console.WriteLine("API is not available. Exiting...");
    return;
}

var patients = patientFactory.Create(100);
await patientSender.SendPatientsAsync(patients);

Console.WriteLine("Process completed.");
