namespace Core.DTOs;
public class Response
{
    public Guid Id { get; set; }
    public string[] Given { get; set; } = Array.Empty<string>();
    public string? Family { get; set; }
    public DateOnly BirthDate { get; set; }
    public TimeOnly BirthTime { get; set; }
    public string Gender { get; set; } = Entities.Gender.Unknown.ToString();
    public bool Active { get; set; }
}