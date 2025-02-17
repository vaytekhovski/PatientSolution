using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;
public abstract class BaseEntity
{
    public BaseEntity() { }
    public BaseEntity(Guid? id, DateTime? createDateTime = null, DateTime? updateDateTime = null)
    {
        Id = id ?? Guid.NewGuid();
        CreateDateTime = createDateTime ?? DateTime.UtcNow;
        UpdateDateTime = updateDateTime ?? CreateDateTime;
    }

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime UpdateDateTime { get; set; }
    public bool ShouldSerializeUpdateDateTime() => UpdateDateTime != CreateDateTime;
}