using System.Text.Json.Serialization;

namespace Core.DTOs;
public class Update
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string[]? Given { get; set; }
    public string? Family { get; set; }
    public DateOnly? BirthDate { get; set; }
    public TimeOnly? BirthTime { get; set; }
    public string? Gender { get; set; }
    public bool? Active { get; set; }
}