namespace Core.Entities;
public class PatientChangeLog : BaseEntity
{
    public PatientChangeLog(Guid? id = null, DateTime? createDateTime = null, DateTime? updateDateTime = null) : base(id, createDateTime, updateDateTime) { }
    public Guid PatientId { get; set; }
    public string Field { get; set; } = null!;
    public string OldValue { get; set; } = null!;
    public string NewValue { get; set; } = null!;
    public DateTime ChangedAt { get; set; }
}
