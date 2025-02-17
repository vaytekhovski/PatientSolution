using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities;

public class Name
{
    public Name(string? family = null, List<string>? given = null)
    {
        Family = family ?? string.Empty;
        Given = given ?? [];
    }

    [BsonElement("family")]
    public string Family { get; set; }

    [BsonElement("given")]
    public List<string> Given { get; set; }
}