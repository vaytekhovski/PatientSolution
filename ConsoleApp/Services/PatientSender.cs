using Core.DTOs;

namespace ConsoleApp.Services;

public class PatientSender : IPatientSender
{
    private readonly SendPatientCommand SendPatientCommand;
    private const int MaxDegreeOfParallelism = 5;

    public PatientSender(SendPatientCommand sendPatientCommand)
    {
        SendPatientCommand = sendPatientCommand;
    }

    public async Task SendPatientsAsync(List<Create> patients)
    {
        Console.WriteLine($"Sending {patients.Count} patients asynchronously...");

        await Parallel.ForEachAsync(patients, 
            new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism }, 
            async (patient, _) =>
            {
                await SendPatientCommand.ExecuteAsync(patient);
            });

        Console.WriteLine("All patients processed!");
    }
}
