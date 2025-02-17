using Core.Common.Extensions;
using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository Repository;
    private readonly IPatientChangeLogRepository ChangesLogger;

    public PatientService(IPatientRepository patientRepository, IPatientChangeLogRepository logRepository)
    {
        Repository = patientRepository;
        ChangesLogger = logRepository;
    }

    public async Task<Response> FindAsync(Guid id)
    {
        var patient = await Repository.FindAsync(id);
        if (patient == null)
            throw new NotFoundException("Patient not found.");

        return patient.ToDto();
    }

    public async Task<List<Response>> FindAsync() => (await Repository.FindAsync()).ConvertAll(p => p.ToDto());

    public async Task<List<Response>> FindAsync(string? dateOp, DateOnly? date, string? timeOp, TimeOnly? time)
    {
        var patients = await Repository.SearchByBirthDateAsync(dateOp, date, timeOp, time);
        return patients.ConvertAll(p => p.ToDto());
    }

    public async Task<Response> CreateAsync(Create dto)
    {
        if (dto.Id.HasValue)
        {
            var existing = await Repository.FindAsync(dto.Id.Value);
            if (existing != null)
                throw new DatabaseException("A patient with this ID already exists.");
        }

        var entity = dto.ToEntity();
        await Repository.CreateAsync(entity);
        return entity.ToDto();
    }

    public async Task<Response> UpdateAsync(Update dto)
    {
        var oldValue = await Repository.FindAsync(dto.Id);
        if (oldValue == null)
            throw new NotFoundException("Patient not found.");

        var changes = oldValue.GetChanges(dto);

        if (!changes.Any())
            return oldValue.ToDto();

        if (!oldValue.IsEditable && changes.Any(c => c.Field is "BirthDate" or "Gender"))
            throw new ValidationException("You cannot update BirthDate or Gender by now");

        var newValue = dto.ToEntity(oldValue);

        await Task.WhenAll(Repository.UpdateAsync(newValue),
            Task.WhenAll(changes.ToChangeLogs(newValue.Id).Select(ChangesLogger.CreateAsync)));

        return newValue.ToDto();
    }

    public async Task DeleteAsync(Guid id)
    {
        var patient = await Repository.FindAsync(id);
        if (patient == null)
            throw new NotFoundException("Patient not found.");

        await Repository.DeleteAsync(id);
    }
}
