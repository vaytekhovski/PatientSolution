using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Persistence.Helpers;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    public PatientRepository(IMongoDatabase database)
        : base(database, "Patient") { }

    public async Task<List<Patient>> SearchByBirthDateAsync(string? dateOp, DateOnly? date, string? timeOp, TimeOnly? time)
    {
        var filters = DateFilterHelper.GetFilters<Patient>(
            p => p.BirthDate, dateOp, date,
            p => p.BirthTime, timeOp, time
        );

        var combinedFilter = filters.Any() ? Builders<Patient>.Filter.And(filters) : Builders<Patient>.Filter.Empty;

        return await FindAsync(combinedFilter);
    }
}

