using Core.DTOs;

namespace Core.Interfaces.Services;
public interface IPatientService
{
    Task<Response> FindAsync(Guid id);
    Task<List<Response>> FindAsync();
    Task<List<Response>> FindAsync(string? dateOp, DateOnly? date, string? timeOp, TimeOnly? time);
    Task<Response> CreateAsync(Create dto);
    Task<Response> UpdateAsync(Update dto);
    Task DeleteAsync(Guid id);
}