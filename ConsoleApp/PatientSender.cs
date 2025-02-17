using Core.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace ConsoleApp;
public static class PatientSender
{
    private static readonly HttpClient HttpClient = new HttpClient();
    private const int MaxTries = 3;

    public static async Task SendPatients(string apiUrl, List<Create> patients)
    {
        Console.WriteLine($"Sending {patients.Count} patients asynchronously...");

        var tasks = patients.Select(async patient =>
        {
            for (int attempt = 1; attempt <= MaxTries; attempt++)
            {
                try
                {
                    Console.WriteLine($"Sending patient: {JsonSerializer.Serialize(patient)} (attempt {attempt}/{MaxTries})");
                    using var response = await HttpClient.PostAsJsonAsync($"{apiUrl}/api/patient", patient);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Successfully submitted!");
                        return;
                    }

                    Console.WriteLine($"Submission failed: {response.StatusCode}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API response: {errorContent}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                await Task.Delay(500);
            }

            Console.WriteLine("Failed to send patient after multiple attempts.");
        });

        await Task.WhenAll(tasks);

        Console.WriteLine("All patients processed!");
    }
}
