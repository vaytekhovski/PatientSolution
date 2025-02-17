using Core.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace ConsoleApp.Services;
public class SendPatientCommand
{
    private readonly HttpClient HttpClient;
    private readonly string ApiUrl;
    private readonly int MaxTries = 3;

    public SendPatientCommand(HttpClient httpClient, string apiUrl)
    {
        HttpClient = httpClient;
        ApiUrl = apiUrl;
    }

    public async Task<bool> ExecuteAsync(Create patient)
    {
        for (int attempt = 1; attempt <= MaxTries; attempt++)
        {
            try
            {
                Console.WriteLine($"Sending patient: {JsonSerializer.Serialize(patient)} (attempt {attempt}/{MaxTries})");
                using var response = await HttpClient.PostAsJsonAsync($"{ApiUrl}/api/patient", patient);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Successfully submitted!");
                    return true;
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
        return false;
    }
}