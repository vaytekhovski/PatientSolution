namespace ConsoleApp.Services;

public interface IApiChecker
{
    Task<bool> IsApiAvailableAsync();
}
