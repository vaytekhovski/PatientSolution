namespace ConsoleApp;
public class Program
{
    public static async Task Main()
    {
        string apiUrl = Environment.GetEnvironmentVariable("PATIENT_API_BASE_URL") ?? "http://localhost:5000";
        int patientCount = 100;

        Console.WriteLine($"Checking API availability at {apiUrl}...");
        if (!await ApiChecker.IsApiAvailable(apiUrl))
        {
            Console.WriteLine("API is not available. Exiting.");
            return;
        }

        Console.WriteLine("API is available! Generating patient data...");
        var patients = PatientGenerator.GeneratePatients(patientCount);

        Console.WriteLine($"Sending {patients.Count} patients to the API...");
        await PatientSender.SendPatients(apiUrl, patients);

        Console.WriteLine("Verifying data after submission...");
        await PatientVerifier.CheckPatientsInApi(apiUrl);
    }
}
