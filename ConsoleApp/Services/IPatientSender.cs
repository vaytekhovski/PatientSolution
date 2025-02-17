using Core.DTOs;

namespace ConsoleApp.Services;
public interface IPatientSender
{
    Task SendPatientsAsync(List<Create> patients);
}