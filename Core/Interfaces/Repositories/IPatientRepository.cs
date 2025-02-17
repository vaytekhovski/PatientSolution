using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<List<Patient>> SearchByBirthDateAsync(string? dateOp, DateOnly? date, string? timeOp, TimeOnly? time);
}