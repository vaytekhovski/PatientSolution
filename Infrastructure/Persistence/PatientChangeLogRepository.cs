using Core.Entities;
using Core.Interfaces.Repositories;
using MongoDB.Driver;

namespace Infrastructure.Persistence;
public class PatientChangeLogRepository : BaseRepository<PatientChangeLog>, IPatientChangeLogRepository
{
    public PatientChangeLogRepository(IMongoDatabase database)
        : base(database, "PatientChangeLog") { }

}