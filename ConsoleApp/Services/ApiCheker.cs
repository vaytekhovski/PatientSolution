namespace ConsoleApp.Services;

public class ApiChecker : IApiChecker
{
    private readonly HttpClient HttpClient;
    private readonly string ApiUrl;

    public ApiChecker(HttpClient httpClient, string apiUrl)
    {
        HttpClient = httpClient;
        ApiUrl = apiUrl;
    }

    public async Task<bool> IsApiAvailableAsync()
    {
        try
        {
            Console.WriteLine($"Checking API availability at {ApiUrl}...");
            var response = await HttpClient.GetAsync($"{ApiUrl}/api/patient");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("API is available.");
                return true;
            }

            Console.WriteLine($"API responded with status: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API check failed: {ex.Message}");
        }

        return false;
    }
}
