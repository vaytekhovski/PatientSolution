namespace ConsoleApp;
public static class ApiChecker
{
    public static async Task<bool> IsApiAvailable(string apiUrl)
    {
        using var httpClient = new HttpClient();

        try
        {
            Console.WriteLine($"Checking API availability at {apiUrl}...");
            var response = await httpClient.GetAsync($"{apiUrl}/api/patient");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("API is available.");
                return true;
            }
            else
            {
                Console.WriteLine($"API responded with status: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API check failed: {ex.Message}");
        }

        return false;
    }
}
