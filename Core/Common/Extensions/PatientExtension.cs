using Core.DTOs;
using Core.Entities;

namespace Core.Common.Extensions;

public static class PatientExtensions
{
    public static Response ToDto(this Patient patient) => new Response
    {
        Id = patient.Id,
        Family = patient.Name.Family,
        Given = [.. patient.Name.Given],
        BirthDate = patient.BirthDate,
        BirthTime = patient.BirthTime,
        Gender = patient.Gender.ToString(),
        Active = patient.Active
    };

    public static Patient ToEntity(this Create dto) => new Patient(dto.Id, DateTime.UtcNow)
    {
        Name = new Name
        {
            Family = dto.Family,
            Given = [.. dto.Given]
        },
        BirthDate = dto.BirthDate,
        BirthTime = dto.BirthTime,
        Gender = dto.Gender.ToGender(),
        Active = dto.Active
    };

    public static Patient ToEntity(this Update dto, Patient patient)
    {
        patient.UpdateDateTime = DateTime.UtcNow;

        if (dto.Given?.Length > 0)
            patient.Name.Given = [..dto.Given];

        if (!string.IsNullOrWhiteSpace(dto.Family))
            patient.Name.Family = dto.Family;

        if (dto.BirthDate.HasValue)
            patient.BirthDate = dto.BirthDate.Value;

        if (dto.BirthTime.HasValue)
            patient.BirthTime = dto.BirthTime.Value;

        if (dto.Active.HasValue)
            patient.Active = dto.Active.Value;

        if (!string.IsNullOrWhiteSpace(dto.Gender))
            patient.Gender = dto.Gender.ToGender();

        return patient;
    }
}