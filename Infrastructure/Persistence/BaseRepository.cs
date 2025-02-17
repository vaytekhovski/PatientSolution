using System.Linq.Expressions;
using Core.Entities;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public abstract class BaseRepository<T> where T : BaseEntity
{
    protected readonly IMongoCollection<T> Collection;

    protected BaseRepository(IMongoDatabase database, string collectionName)
    {
        Collection = database.GetCollection<T>(collectionName);
    }

    public async Task<T?> FindAsync(Guid id) =>
        await Collection.Find(e => e.Id == id).FirstOrDefaultAsync();

    public async Task<List<T>> FindAsync() =>
        await Collection.Find(_ => true).ToListAsync();

    public async Task<List<T>> FindAsync(FilterDefinition<T> filter) =>
        await Collection.Find(filter).ToListAsync();

    public async Task CreateAsync(T entity) =>
        await Collection.InsertOneAsync(entity);

    public async Task UpdateAsync(T entity) =>
        await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);

    public async Task DeleteAsync(Guid id) =>
        await Collection.DeleteOneAsync(e => e.Id == id);

    protected FilterDefinition<T> BuildFilter<TField>(Expression<Func<T, TField>> field, string operation, TField value)
    {
        var builder = Builders<T>.Filter;
        return operation switch
        {
            "eq" => builder.Eq(field, value),
            "ne" => builder.Ne(field, value),
            "gt" => builder.Gt(field, value),
            "lt" => builder.Lt(field, value),
            "ge" => builder.Gte(field, value),
            "le" => builder.Lte(field, value),
            _ => builder.Empty
        };
    }
}
