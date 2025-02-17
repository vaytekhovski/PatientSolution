using Core.Entities;

namespace Core.Common.Extensions;
public static class ChangeLogExtensions
{
    public static IEnumerable<PatientChangeLog> ToChangeLogs(this IEnumerable<(string Field, string OldValue, string NewValue)> changes, Guid patientId)
    {
        return changes.Select(change => new PatientChangeLog
        {
            PatientId = patientId,
            Field = change.Field,
            OldValue = change.OldValue,
            NewValue = change.NewValue,
            ChangedAt = DateTime.UtcNow
        });
    }
}
