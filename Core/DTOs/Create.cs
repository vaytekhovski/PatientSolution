using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;
public class Create
{
    [SwaggerSchema("Unique patient identifier", Format = "uuid")]
    public Guid? Id { get; set; }
    [Required]
    [SwaggerSchema("Patient given names")]
    public string[] Given { get; set; } = Array.Empty<string>();
    [Required]
    [SwaggerSchema("Patient family name")]
    public string Family { get; set; } = string.Empty;
    [Required]
    [SwaggerSchema("Date of birth", Format = "date")]
    public DateOnly BirthDate { get; set; }
    [SwaggerSchema("Time of birth", Format = "time")]
    public TimeOnly BirthTime { get; set; } = TimeOnly.MinValue;
    [SwaggerSchema("Gender")]
    public string Gender { get; set; } = Entities.Gender.Unknown.ToString();
    public bool Active { get; set; }
}