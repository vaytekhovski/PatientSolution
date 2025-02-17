namespace ConsoleApp;
public static class PatientVerifier
{
    private static readonly HttpClient HttpClient = new HttpClient();

    public static async Task CheckPatientsInApi(string apiUrl)
    {
        try
        {
            using var response = await HttpClient.GetAsync($"{apiUrl}/api/patient");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error retrieving data: {response.StatusCode}");
                return;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Data in API: {responseData}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data from API: {ex.Message}");
        }
    }
}
