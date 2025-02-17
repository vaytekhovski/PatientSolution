using Core.Entities;

namespace Core.Common.Extensions;
public static class GenderExtensions
{
    public static Gender ToGender(this string? gender) =>
        Enum.TryParse<Gender>(gender, true, out var result) ? result : Gender.Unknown;
}
