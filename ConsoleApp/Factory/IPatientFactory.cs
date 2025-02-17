using Core.DTOs;

namespace ConsoleApp.Factory;

public interface IPatientFactory
{
    Create Create();
    List<Create> Create(int count);
}