using Core.Common.Extensions;
using Core.DTOs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;
public class Patient : BaseEntity
{
    public Patient() : base() { }
    public Patient(Guid? id = null, DateTime? createDateTime = null, DateTime? updateDateTime = null) : base(id, createDateTime, updateDateTime) { }

    [BsonElement("name")]
    public Name Name { get; set; } = new Name();

    [BsonElement("gender")]
    public Gender Gender { get; set; } = Gender.Unknown;

    [BsonElement("birthDate")]
    public DateOnly BirthDate { get; set; }
    [BsonElement("birthTime")]
    public TimeOnly BirthTime { get; set; } = TimeOnly.MinValue;

    [BsonElement("active")]
    public bool Active { get; set; }

    public bool IsEditable => (DateTime.UtcNow - CreateDateTime).TotalDays < 30;

    public List<(string Field, string OldValue, string NewValue)> GetChanges(Update dto)
    {
        var changes = new List<(string, string, string)>();

        if (dto.BirthDate.HasValue && dto.BirthDate != BirthDate)
            changes.Add(("BirthDate", BirthDate.ToString(), dto.BirthDate.ToString() ?? string.Empty));

        if (dto.BirthTime.HasValue && dto.BirthTime != BirthTime)
            changes.Add(("BirthTime", BirthTime.ToString(), dto.BirthTime.ToString() ?? string.Empty));

        if (!string.IsNullOrEmpty(dto.Gender) && !dto.Gender.ToGender().Equals(Gender))
            changes.Add(("Gender", Gender.ToString(), dto.Gender));

        if (!string.IsNullOrEmpty(dto.Family) && dto.Family != Name.Family)
            changes.Add(("Family", Name.Family, dto.Family));

        if (dto.Given is not null && !dto.Given.SequenceEqual(Name.Given))
            changes.Add(("Given", string.Join(',', Name.Given), string.Join(',', dto.Given)));

        if (dto.Active.HasValue && dto.Active != Active)
            changes.Add(("Active", Active.ToString(), dto.Active.Value.ToString()));

        return changes;
    }
}
