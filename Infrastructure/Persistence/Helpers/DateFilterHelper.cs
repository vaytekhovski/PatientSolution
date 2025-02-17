using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Helpers;

public static class DateFilterHelper
{
    public static List<FilterDefinition<T>> GetFilters<T>(
        Expression<Func<T, DateOnly>> dateField, string? dateOp, DateOnly? date,
        Expression<Func<T, TimeOnly>> timeField, string? timeOp, TimeOnly? time)
        => [..new List<FilterDefinition<T>?>
        {
            date.HasValue ? CreateFilter(dateField, dateOp, date.Value) : null,
            time.HasValue ? CreateFilter(timeField, timeOp, time.Value) : null
        }
        .Where(f => f is not null)
        .Cast<FilterDefinition<T>>()];

    private static FilterDefinition<T> CreateFilter<T, TValue>(
        Expression<Func<T, TValue>> field,
        string? operation,
        TValue value) where TValue : struct
        => operation switch
        {
            "eq" => Builders<T>.Filter.Eq(field, value),
            "ne" => Builders<T>.Filter.Ne(field, value),
            "gt" => Builders<T>.Filter.Gt(field, value),
            "ge" => Builders<T>.Filter.Gte(field, value),
            "lt" => Builders<T>.Filter.Lt(field, value),
            "le" => Builders<T>.Filter.Lte(field, value),
            _ => throw new ArgumentException($"Unsupported operation: {operation}")
        };
}
